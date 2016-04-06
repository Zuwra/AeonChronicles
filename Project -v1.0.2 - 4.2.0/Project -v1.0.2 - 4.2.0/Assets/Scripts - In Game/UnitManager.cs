using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//this class extends RTSObject through the Unit class
public class UnitManager : Unit,IOrderable{

	public string UnitName;


	public int PlayerOwner;


	public float visionRange;
	private float chaseRange;
	public IMover cMover;
	public IWeapon myWeapon;
	public UnitStats myStats;

	public Iinteract interactor;

	SphereCollider visionSphere;

	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> allies = new List<GameObject>();
	public List<GameObject> neutrals = new List<GameObject> ();

	private Queue<UnitState> queuedStates = new Queue<UnitState> ();

	private UnitState myState;
	private List<Object> stunSources = new List<Object> ();
	private List<Object> silenceSources = new List<Object> ();

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


		if (myWeapon == null) {
			myWeapon = gameObject.GetComponent<IWeapon>();
		}

		if (myStats == null) {
			myStats = gameObject.GetComponent<UnitStats>();
		}

	
			GameManager man = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ();
			if (PlayerOwner != man.playerNumber) {
				this.gameObject.tag = "Enemy";
			} else {
				this.gameObject.tag = "Player";
			}

			man.initialize ();
			man.playerList [PlayerOwner - 1].addUnit (this.gameObject);

		//	man.playerList [PlayerOwner - 1].UnitCreated (myStats.supply);

		if (gameObject.GetComponent<CharacterController> ()  && visionSphere != null) {
			visionSphere.radius = visionRange + gameObject.GetComponent<CharacterController> ().radius;
		}

	
		if (cMover != null) {
			changeState (new DefaultState ());
		} else if (myStats.isUnitType (UnitTypes.UnitTypeTag.Turret) 
			&& this.gameObject.gameObject.GetComponent<UnitManager>().UnitName == "Coyote"
			&& ((StandardInteract)this.gameObject.gameObject.GetComponent<UnitManager>().interactor).attackWhileMoving) {

			changeState (new turretState (this, this.cMover, this.myWeapon));
		}


		if (myWeapon != null) {
			chaseRange = visionRange - ((visionRange - myWeapon.range) / 2);
		} else {
			chaseRange = visionRange;
		}

	}

	// Use this for initialization
	new void Start () {
		
	}



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
				order = abilityList [n].canActivate ();

				if (order.canCast) {

					changeState (new CastAbilityState (abilityList [n]));

				}

			}
			return order.nextUnitCast;
		}
		return true;
	}


	override
	public bool UseTargetAbility(GameObject obj, Vector3 loc, int n)
	{continueOrder order = new continueOrder();
		if (!isStunned && !isSilenced) {
			if (abilityList [n] != null) {
	
				order = abilityList [n].canActivate ();
		
				if (order.canCast) {
					if (abilityList [n] is TargetAbility) {
						changeState (new AbilityFollowState (obj, loc, (TargetAbility)abilityList [n]));
					} else if (abilityList [n] is Morph) {
						changeState (new PlaceBuildingState (loc, abilityList [n]));
					}

				}

			}
		} 
		return order.nextUnitCast;
	}



	override
	public void autoCast(int n)
	{
		if (abilityList [n] != null) {
			
				abilityList [n].setAutoCast();

		}
	}


	public void setInteractor()
	{Start ();
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



	public GameObject findClosestEnemy()
	{

		enemies.RemoveAll(item => item == null);
		GameObject best = null;
		
		
		float distance = 1000000;
	
		
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
	
	public GameObject findBestEnemy()
	{enemies.RemoveAll(item => item == null);
		GameObject best = null;


		float distance = 1000000;
		float bestPriority = -1;

		for (int i = 0; i < enemies.Count; i ++) {
			if (enemies[i] != null) {
				if (!myWeapon.isValidTarget (enemies [i])) {
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


	public void nextState()
	{
		if (myState is CastAbilityState) {
			if (queuedStates.Count > 0) {
				myState = queuedStates.Dequeue ();
				if (myState != null) {
					myState.myManager = this;
					myState.myWeapon = myWeapon;
					myState.myMover = cMover;
					myState.initialize ();
				}
			} else {
				changeState (new DefaultState ());
			}
		} 
	}

	public void changeState(UnitState nextState)
	{
		//Debug.Log ("Changing to " + nextState);
		if (Input.GetKey (KeyCode.LeftShift) && (!(nextState is DefaultState) && (queuedStates.Count > 0  || !(myState is DefaultState)))) {
				queuedStates.Enqueue (nextState);

			return;
		
		} 

		else if (nextState is DefaultState) {
			if (queuedStates.Count > 0) {

				myState = queuedStates.Dequeue ();
				if (myState == null) {
					return;
				}
				myState.myManager = this;
				myState.myWeapon = myWeapon;
				myState.myMover = cMover;
				myState.initialize ();
				return;

			}  
		}

		else if (myState is ChannelState) {
			
				queuedStates.Enqueue(nextState);
			return;
			}

	
		else if (nextState is AttackMoveState) {
			((AttackMoveState)nextState).setHome (this.gameObject.transform.position);
		}



			nextState.myManager = this;
			nextState.myWeapon = myWeapon;
			nextState.myMover = cMover;
			queuedStates.Clear ();


		if (nextState is CastAbilityState && ((CastAbilityState)nextState).myAbility.continueMoving) {

			queuedStates.Enqueue (myState);
		}


			myState = nextState;
			myState.initialize ();

			
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
		myWeapon = weap;
		if (myState != null) {
			myState.myWeapon = weap;
		}
	}

	public bool isIdle()
		{
		if(myState.GetType() == typeof(DefaultState))
		{return true;	}
		return false;
	}

	public void Attacked(GameObject src)
	{if(myState!=null)
		myState.attackResponse (src);}



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