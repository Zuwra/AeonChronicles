﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//this class extends RTSObject through the Unit class
public class UnitManager : Unit,IOrderable{

	//AbilityList<Ability> is in the Unit Class
	public string UnitName;

	public int PlayerOwner; // 1 = active player, 2 = enemies, 3 = nuetral



	private float chaseRange;  // how far an enemy can come into vision before I chase after him.
	public IMover cMover;      // Pathing Interface. Classes to use here : AirMover(Flying Units), cMover (ground, uses Global Astar) , RVOMover(ground, Uses Astar and unit collisions, still in testing)
	public List<IWeapon> myWeapon;   // IWeapon is not actually an interface but a base class with required parameters for all weapons.
	public bool MultiWeaponAttack;
	public UnitStats myStats; // Contains Unit health, regen, armor, supply, cost, etc

	public Iinteract interactor; // Passes commands to this to determine how to interact (Right click on a friendly could be a follow command or a cast spell command, based on the Unit/)

	public float visionRange;
	SphereCollider visionSphere; // Trigger Collider that respresents vision radius. Size is set in the start function based on visionRange
	// When Enemies enter the visionsphere, it puts them into one of these categories. They are removed when they move away or die.
	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> allies = new List<GameObject>();
	public List<GameObject> neutrals = new List<GameObject> ();


	private Queue<UnitState> queuedStates = new Queue<UnitState> (); // Used to queue commands to be executed in succession.

	private UnitState myState;     // used for StateMachine

	private List<Object> stunSources = new List<Object> ();     // Used to keep track of stun lengths and duration, to ensure the strongest one is always applied.
	private List<Object> silenceSources = new List<Object> ();

	public voiceResponse myVoices;

	[System.Serializable]
	public struct voiceResponse
	{
		public List<AudioClip> moving;
		public List<AudioClip> attacking;
	}

	private bool isStunned;
	private bool isSilenced;


	new void Awake()
	{

		if(interactor == null)
		{
			interactor = (Iinteract)gameObject.GetComponent(typeof(Iinteract));

		}

		if (visionSphere == null) {
			visionSphere = this.gameObject.GetComponent<SphereCollider>();}



		if (cMover == null) {
			cMover = gameObject.GetComponent<customMover>();
			if(cMover == null)
				cMover = gameObject.GetComponent<airmover>();
		}


		if (myWeapon.Count == 0) {
			IWeapon tempWeap = gameObject.GetComponent<IWeapon>();
			if (tempWeap) {
				myWeapon.Add (tempWeap);}
			
		}

		if (myStats == null) {
			myStats = gameObject.GetComponent<UnitStats>();
		}

	
			GameManager man = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ();
			if (PlayerOwner == man.playerNumber) {
				this.gameObject.tag = "Player";
			} 

			man.initialize (); // Initializes data, if this is the first unit to wake up.
			myStats.Initialize();
			man.playerList [PlayerOwner - 1].addUnit (this.gameObject);

		if (gameObject.GetComponent<CharacterController> () && visionSphere != null) {
			float distance = visionRange + gameObject.GetComponent<CharacterController> ().radius;
			visionSphere.radius = distance;

			if (GetComponent<FogOfWarUnit> ()) {
				GetComponent<FogOfWarUnit> ().radius = distance +3;
			}
		} else {
			visionSphere.radius = visionRange;
			if (GetComponent<FogOfWarUnit> ()) {
				GetComponent<FogOfWarUnit> ().radius = visionRange +3;
			}
		}

	
		if (cMover != null) {
			changeState (new DefaultState ());
		} else if (myStats.isUnitType (UnitTypes.UnitTypeTag.Turret)
		           && this.gameObject.gameObject.GetComponent<UnitManager> ().UnitName == "Zephyr"
		           && ((StandardInteract)this.gameObject.gameObject.GetComponent<UnitManager> ().interactor).attackWhileMoving) {

			changeState (new turretState (this));
		} else if( myStats.isUnitType (UnitTypes.UnitTypeTag.Static_Defense) ){
			changeState (new turretState (this));
		}


	
			chaseRange = visionRange;

	}

	//Elsewhere this command is called on the RTSObject class, which is not a monobehavior, and cannot access its gameobject.
	public new GameObject getObject()
	{return this.gameObject;}



	
	// Update is called once per frame
	new void Update () {
		if (myState != null) {

			myState.Update ();
		} 
	}





	override
	public bool UseAbility(int n)
	{
		if (!isStunned && !isSilenced) {

			continueOrder order = null;
			if (abilityList [n] != null) {
				order = abilityList [n].canActivate (true);

				if (order.canCast) {

					changeState (new CastAbilityState (abilityList [n]));

				}

			}
			return order.nextUnitCast;
		}
		return true;
	}


	override
	public bool UseTargetAbility(GameObject obj, Vector3 loc, int n) // Either the obj - target or the location can be null.
	{continueOrder order = new continueOrder();
		if (!isStunned && !isSilenced) {
			if (abilityList [n] != null) {
	
				order = abilityList [n].canActivate (true);
		
				if (order.canCast) {
					if (abilityList [n] is TargetAbility) {
						changeState (new AbilityFollowState (obj, loc, (TargetAbility)abilityList [n]));
					} else if (abilityList [n] is Morph || abilityList [n] is BuildStructure) {
						changeState (new PlaceBuildingState (loc, abilityList [n]));
					}

				}

			}
		} 
		return order.nextUnitCast;
	}



	override
	public void autoCast(int n) // Program in how it is autocast in a custom UnitState, which should be accessed/created from the interactor class
	{
		if (abilityList [n] != null) {
			
				abilityList [n].setAutoCast();

		}
	}


	public void setInteractor()
	{Start (); // in the parent class
	}


	public new void GiveOrder (Order order)
	{
		if (myState is  ChannelState) {
			return;
		}


		if (interactor != null) {
			interactor.computeInteractions (order);

		}
	}


	public void removeAbility(Ability abil)
	{abilityList.Remove (abil);
		abilityList.RemoveAll(item => item == null);}


	//Other Units have entered vision
	void OnTriggerEnter(Collider other)
	{
		//need to set up calls to listener components
		//this will need to be refactored for team games

		if (!other.isTrigger) {

			if (other.gameObject.layer == 13) {
				neutrals.Add (other.gameObject);
				return;
			}

			UnitManager manage = other.gameObject.GetComponent<UnitManager>();

			if(manage){
				if (manage.PlayerOwner == 3) {
					neutrals.Add (other.gameObject);
					return;
				}

			if(manage.PlayerOwner != PlayerOwner)
				{if(!other.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Invisible))
					{enemies.Add (other.gameObject);}}

			else{
				allies.Add(other.gameObject);
				}}

		}
	}

	// Other units have left the vision
	void OnTriggerExit(Collider other)
	{
		
		if (enemies.Contains (other.gameObject)) {
			enemies.Remove (other.gameObject);
		} 
		else if (allies.Contains (other.gameObject)) {
			allies.Remove (other.gameObject);
		} 
		else if (neutrals.Contains (other.gameObject)) {
			neutrals.Remove (other.gameObject);
		}
	
	}


	// Called from some of the states (ie, DefaultState, AttackMoveState)
	public GameObject findClosestEnemy()
	{

		enemies.RemoveAll(item => item == null);
		GameObject best = null;

		float distance = float.MaxValue;
	
		
		for (int i = 0; i < enemies.Count; i ++) {
			if (enemies[i] != null) {
				
				float currDistance = Vector3.Distance(enemies[i].transform.position, this.gameObject.transform.position);
				if(currDistance < distance)
				{best = enemies[i];
					
					distance = currDistance;
				}
				
			}
		}
		return best;

	}
	
	public GameObject findBestEnemy() // Similar to above method but takes into account attack priority (enemy soldiers should be attacked before buildings)
	{enemies.RemoveAll(item => item == null);
		GameObject best = null;


		float distance = float.MaxValue;
		float bestPriority = -1;

		for (int i = 0; i < enemies.Count; i ++) {
			if (enemies[i] != null) {
				if (!isValidTarget (enemies [i])) {
					continue;
				}
				if (enemies[i].GetComponent<UnitStats> ().attackPriority < bestPriority) {
						
					continue;
					}
				else if (enemies[i].GetComponent<UnitStats> ().attackPriority > bestPriority)
					{best = enemies[i];
					bestPriority = enemies[i].GetComponent<UnitStats> ().attackPriority;
					}
			
				
				float currDistance = Vector3.Distance(enemies[i].transform.position, this.gameObject.transform.position);
				if(currDistance < distance)
				      {best = enemies[i];

					distance = currDistance;
				}

			}
		}

		return best;
	}

	public void setInteractor(Iinteract inter)
	{interactor = inter;
	}


	public void nextState() // Used when executing queued commands
	{
		if (myState is CastAbilityState) {
			if (queuedStates.Count > 0) {
				myState = queuedStates.Dequeue ();
				if (myState != null) {
					myState.myManager = this;
					myState.initialize ();
				}
			} else {
				changeState (new DefaultState ());
			}
		} 
	}


	public void changeState(UnitState nextState)
	{
		
		if (Input.GetKey (KeyCode.LeftShift) && (!(nextState is DefaultState) && (queuedStates.Count > 0 || !(myState is DefaultState)))) {
			queuedStates.Enqueue (nextState);

			return;
		
		} else if (nextState is DefaultState) {
			if (queuedStates.Count > 0) {
				if (myState != null) {
					myState.endState ();
				}
				myState = queuedStates.Dequeue ();
				if (myState == null) {
					return;
				}
				myState.myManager = this;
				myState.initialize ();
				checkIdleWorker ();
				return;

			}  
		} else if (myState is PlaceBuildingState) {
			((PlaceBuildingState)myState).cancel ();
		}

		else if (myState is ChannelState) {
			
				queuedStates.Enqueue(nextState);
			return;
			}

	
		else if (nextState is AttackMoveState) {
			((AttackMoveState)nextState).setHome (this.gameObject.transform.position);
		}
			
			nextState.myManager = this;
			queuedStates.Clear ();


		if (nextState is CastAbilityState && ((CastAbilityState)nextState).myAbility.continueMoving) {

			queuedStates.Enqueue (myState);
		}
		if (myState != null) {
			myState.endState ();
		}
			myState = nextState;
			myState.initialize ();
	
		checkIdleWorker ();

	}

	public void checkIdleWorker()
	{
		if (myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
			FButtonManager.main.changeWorkers ();
		}
	}


	// return -1 if it is not in range, else pass back the index of the weapon that is in range
	public IWeapon inRange(GameObject obj)
	{
		float min= 100000000;
		IWeapon best = null;
		foreach (IWeapon weap in myWeapon) {
			if (weap.inRange (obj)) {
				if (weap.range < min) {
					best = weap;
					min = weap.range;
				}
			}

		}
		return best;

	}


	public IWeapon isValidTarget(GameObject obj)
	{IWeapon best = null;
		float min= 100000000;
		foreach (IWeapon weap in myWeapon) {
			if(weap.isValidTarget(obj)){
				if (weap.range < min) {
					best = weap;
					min = weap.range;
				}
			}

		}
		return best;
	}

	public IWeapon canAttack(GameObject obj)
	{IWeapon best = null;
		float min= 100000000;
		foreach (IWeapon weap in myWeapon) {
			if(weap.canAttack(obj)){
				if (weap.range < min) {
					best = weap;
					min = weap.range;
				}
			}
		}
		return best;
	}




	public void enQueueState(UnitState nextState)
	{queuedStates.Enqueue (nextState);
	
	}


	public void cleanEnemy()
	{
		enemies.RemoveAll(item => item == null);
	
	}


	public void setStun(bool input, Object source)
	{
		if (input) {
			stunSources.Add (source);
		} else {
			if (stunSources.Contains (source)) {
			
				stunSources.Remove (source);}
		}

			isStunned = (stunSources.Count > 0);
		
	}
	public void setSilence(bool input, Object source)
	{
		if (input) {
			silenceSources.Add (source);
		} else {
			if (silenceSources.Contains (source)) {

				silenceSources.Remove (source);}
		}

		isSilenced= (silenceSources.Count > 0);
	}


	public bool Silenced()
	{return isSilenced;
	}

	public bool Stunned()
	{return isStunned;
	}


	public void setWeapon(IWeapon weap)
		{
		if (!myWeapon.Contains (weap)) {
			myWeapon.Add (weap);
		}

	}

	public bool isIdle()
		{ // will need to be refactored if units require a custom default state
		if(myState.GetType() == typeof(DefaultState))
		{return true;	}
		return false;
	}

	public void Attacked(GameObject src) //I have been attacked, what do i do?
	{
		
		if (myState != null) {

			myState.attackResponse (src, 5);
		}
	}



	public UnitState getState()
	{return myState;}

	public int getStateCount()
	{return queuedStates.Count;
	}

	public UnitState checkNextState()
	{if (queuedStates.Count > 0) {
			return queuedStates.Peek ();
		} else {
		return null;}
	}

	public float getChaseRange()
	{return chaseRange;}

}