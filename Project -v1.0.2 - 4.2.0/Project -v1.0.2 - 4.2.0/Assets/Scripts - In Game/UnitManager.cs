using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//this class extends RTSObject through the Unit class
public class UnitManager : Unit,IOrderable{

	//AbilityList<Ability> is in the Unit Class
	public string UnitName;

	public int PlayerOwner; // 1 = active player, 2 = enemies, 3 = nuetral
	public int formationOrder;
	public Animator myAnim;

	private float chaseRange;  // how far an enemy can come into vision before I chase after him.
	public IMover cMover;      // Pathing Interface. Classes to use here : AirMover(Flying Units), cMover (ground, uses Global Astar) , RVOMover(ground, Uses Astar and unit collisions, still in testing)
	public List<IWeapon> myWeapon;   // IWeapon is not actually an interface but a base class with required parameters for all weapons.
	public bool MultiWeaponAttack;
	public UnitStats myStats; // Contains Unit health, regen, armor, supply, cost, etc

	public Iinteract interactor; // Passes commands to this to determine how to interact (Right click on a friendly could be a follow command or a cast spell command, based on the Unit/)

	public float visionRange;
	SphereCollider visionSphere; // Trigger Collider that respresents vision radius. Size is set in the start function based on visionRange
	// When Enemies enter the visionsphere, it puts them into one of these categories. They are removed when they move away or die.
	public List<UnitManager> enemies = new List<UnitManager>();
	public List<UnitManager> allies = new List<UnitManager>();
	public List<GameObject> neutrals = new List<GameObject> ();


	private LinkedList<UnitState> queuedStates = new LinkedList<UnitState> (); // Used to queue commands to be executed in succession.

	private UnitState myState;     // used for StateMachine

	private List<Object> stunSources = new List<Object> ();     // Used to keep track of stun lengths and duration, to ensure the strongest one is always applied.
	private List<Object> silenceSources = new List<Object> ();

	public voiceResponse myVoices;

	//List of weapons modifiers that need to be applied to weapons as they are put on this guy
	private List<Notify> potentialNotify = new List<Notify>();

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
			foreach (IWeapon w in GetComponents<IWeapon>()) {
				if (!myWeapon.Contains (w)) {
					myWeapon.Add (w);
				}
			}
		}

		if (myStats == null) {
			myStats = gameObject.GetComponent<UnitStats>();
		}

	
		GameManager man = GameObject.FindObjectOfType<GameManager> ();
			if (PlayerOwner == man.playerNumber) {
				this.gameObject.tag = "Player";
			} 

			man.initialize (); // Initializes data, if this is the first unit to wake up.
			myStats.Initialize();

		if (!Clock.main || Clock.main.getTotalSecond () < 1 || !myStats.isUnitType (UnitTypes.UnitTypeTag.Structure) || UnitName== "Augmentor") {
		//	Debug.Log (" manager " + man.playerList.Length + "   " + (PlayerOwner - 1) +"  " + this.gameObject);
			man.playerList [PlayerOwner - 1].addUnit (this.gameObject);
		}

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
		if (myState != null && !isStunned) {

			myState.Update ();
		} 
	}



	public void addNotify(Notify toAdd)
	{
		if (!potentialNotify.Contains (toAdd)) {
			potentialNotify.Add (toAdd);
		}

		foreach (IWeapon weap in myWeapon) {
		
			if (!weap.triggers.Contains (toAdd)) {
				weap.triggers.Add (toAdd);
			}
		}
	}



	override
	public bool UseAbility(int n, bool queue)
	{
		if (!isStunned && !isSilenced) {

			continueOrder order = null;
			if (abilityList [n] != null) {
				order = abilityList [n].canActivate (true);

				if (order.canCast) {

					changeState (new CastAbilityState (abilityList [n]),false,queue);

				}

			}
			return order.nextUnitCast;
		}
		return true;
	}


	override
	public bool UseTargetAbility(GameObject obj, Vector3 loc, int n, bool queue) // Either the obj - target or the location can be null.
	{continueOrder order = new continueOrder();
		if (!isStunned && !isSilenced) {
			if (abilityList [n] != null) {
	
				order = abilityList [n].canActivate (true);
		
				if (order.canCast) {
					if (abilityList [n] is TargetAbility) {
						
						changeState (new AbilityFollowState (obj, loc, (TargetAbility)abilityList [n]), false, queue);
					} else if (abilityList [n] is Morph || abilityList [n] is BuildStructure) {
						changeState (new PlaceBuildingState (obj,loc, abilityList [n]), false, queue);
					}

				}

			}
		} 
		return order.nextUnitCast;
	}



	override
	public void autoCast(int n, bool offOn) // Program in how it is autocast in a custom UnitState, which should be accessed/created from the interactor class
	{
		if (abilityList [n] != null) {
			
			abilityList [n].setAutoCast(offOn);

		}
	}


	public void setInteractor()
	{Start (); // in the parent class
		

	}


	public new void GiveOrder (Order order)
	{
		
		if (myState is  ChannelState) {
			//order.queued = true;
			foreach (UnitState s in queuedStates) {

				if (s is PlaceBuildingState) {
					//Debug.Log ("Cenceling");
					((PlaceBuildingState)s).cancel ();
				}
			}
			if (!order.queued) {
				queuedStates.Clear ();
			}
			//return;
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
					{enemies.Add (manage);}}

			else{
					allies.Add(manage);
					//Debug.Log ( this.gameObject +"  Adding " + other.gameObject);
				}}

		}
	}

	// Other units have left the vision
	void OnTriggerExit(Collider other)
	{
		if (other.isTrigger) {
			return;
		}

		if (neutrals.Contains (other.gameObject)) {
			neutrals.Remove (other.gameObject);
		}

		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

		if (manage) {
			if (enemies.Contains (manage)) {
				enemies.Remove (manage);
			} else if (allies.Contains (manage)) {
				allies.Remove (manage);
				//Debug.Log ( this.gameObject + "  Removing " + other.gameObject);
			} 
		}
	
	}


	// Called from some of the states (ie, DefaultState, AttackMoveState)
	public UnitManager findClosestEnemy()
	{

		enemies.RemoveAll(item => item == null);
		UnitManager best = null;

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
	
	public UnitManager findBestEnemy() // Similar to above method but takes into account attack priority (enemy soldiers should be attacked before buildings)
	{enemies.RemoveAll(item => item == null);
		UnitManager best = null;


		float distance = float.MaxValue;
		float bestPriority = -1;

		for (int i = 0; i < enemies.Count; i ++) {
			if (enemies[i] != null) {
				if (!isValidTarget (enemies [i])) {
					continue;
				}
				if (enemies[i].myStats.attackPriority < bestPriority) {
						
					continue;
					}
				else if (enemies[i].myStats.attackPriority > bestPriority)
					{best = enemies[i];
					bestPriority = enemies[i].myStats.attackPriority;
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



	private UnitState popLastState()
	{
		
		UnitState us = queuedStates.Last.Value;
		queuedStates.RemoveLast ();
		return us;
	}

	public UnitState popFirstState()
	{UnitState us = queuedStates.First.Value;
		queuedStates.RemoveFirst ();
		return us;
	}

	public void nextState() // Used when executing queued commands
	{
		if (myState is CastAbilityState) {
			if (queuedStates.Count > 0) {
				myState = popFirstState();
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
	{changeState (nextState, false, false);
	}


	// make sure that Queue front and queueback are never both true
	public void changeState(UnitState nextState, bool Queuefront, bool QueueBack)
	{//Debug.Log ("Next state is " + nextState + "    " + queuedStates.Count);
		if (myState is  ChannelState && !(nextState is DefaultState)) {
			queuedStates.AddLast (nextState);
			//Debug.Log ("Queuing " + nextState + "   " + queuedStates.Count);
			return;
			}

		//Debug.Log ("# of states  " +QueueBack + "    " + queuedStates.Count + "    " + nextState + "    " + myState);
		if (Queuefront && (!(nextState is DefaultState) && (queuedStates.Count > 0 || !(myState is DefaultState)))) {

			//Debug.Log ("Queing + " +nextState);
			queuedStates.AddFirst (myState);
			myState =interactor.computeState (nextState);
			myState.initialize ();


			return;
		
		}
		else if (QueueBack && (!(nextState is DefaultState) && (queuedStates.Count > 0 || !(myState is DefaultState)))){

			queuedStates.AddLast (nextState);

			return;

		}

		else if (nextState is DefaultState) {
			if (queuedStates.Count > 0) {
				if (myState != null) {
					myState.endState ();
				}
			
				myState = interactor.computeState(popFirstState());
			
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

	
		else if (nextState is AttackMoveState ) {
			((AttackMoveState)nextState).setHome (this.gameObject.transform.position);
		}
			

		nextState.myManager = this;
	
		foreach (UnitState s in queuedStates) {

			if (s is PlaceBuildingState) {
				//Debug.Log ("Cenceling");
				((PlaceBuildingState)s).cancel ();
			}
		}
			queuedStates.Clear ();


		if (nextState is CastAbilityState && ((CastAbilityState)nextState).myAbility.continueMoving) {

			queuedStates.AddLast (myState);
		}
		if (myState != null) {
			myState.endState ();
		}



		myState =interactor.computeState (nextState);
		//Debug.Log ("Setting state to " + myState);
		myState.initialize ();
	
		checkIdleWorker ();

	}


	public void cancel()
	{
		//Debug.Log ("Here somehow");
		foreach (UnitState s in queuedStates) {

			if (s is PlaceBuildingState) {

				((PlaceBuildingState)s).cancel ();
			}
		}
		queuedStates.Clear ();
	}


	public void animMove()
	{if (myAnim) {
			myAnim.SetInteger ("State", 2);
		}
	}

	public void animAttack()
	{if (myAnim) {
			myAnim.SetInteger ("State", 3);
		}
	}

	public void animStop()
	{if (myAnim) {
			myAnim.SetInteger ("State", 1);
		}
	}

	public override UnitStats getUnitStats ()
	{
		return myStats;
	}

	public override UnitManager getUnitManager ()
	{
		return this;
	}


	public void checkIdleWorker()
	{
		if (myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
			FButtonManager.main.changeWorkers ();
		}
	}


	// return -1 if it is not in range, else pass back the index of the weapon that is in range
	public IWeapon inRange(UnitManager obj)
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


	public IWeapon isValidTarget(UnitManager obj)
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

	public IWeapon canAttack(UnitManager obj)
	{IWeapon best = null;
		float min= 100000000;
		foreach (IWeapon weap in myWeapon) {
		//	Debug.Log ("Checking " + weap.Title);
			if(weap.canAttack(obj)){
				//Debug.Log (weap.Title + " can attack");
				if (weap.range < min) {
				//	Debug.Log (weap.Title + " best range");
					best = weap;
					min = weap.range;
				}
			}
		}
		return best;
	}




	public void enQueueState(UnitState nextState)
	{queuedStates.AddLast (nextState);
	
	}


	public void cleanEnemy()
	{
		enemies.RemoveAll(item => item == null);
	
	}


	public void setStun(bool StunOrNot, Object source)
	{

		Debug.Log ("getting stunned " + this.gameObject);
		if (StunOrNot) {
			stunSources.Add (source);
		} else {
			if (stunSources.Contains (source)) {
			
				stunSources.Remove (source);
			} else {stunSources.RemoveAll (item => item == null);
			}
		}

			isStunned = (stunSources.Count > 0);

		Debug.Log ("Is stunned ");
		if (isStunned && StunRun == null) {
			StunRun = StartCoroutine (stunnedIcon());
		}
		
	}

	Coroutine StunRun;

	IEnumerator stunnedIcon()
	{
		Debug.Log ("Starting stun");
		GameObject icon =  PopUpMaker.CreateStunIcon (this.gameObject);
		while (isStunned) {
		
			yield return null;
		}

		Destroy (icon);
	}

	public void StunForTime(Object source, float duration)
	{
		StartCoroutine (stunOverTime (source, duration));
		if (isStunned && StunRun == null) {
			StunRun = StartCoroutine (stunnedIcon());
		}

	}

	IEnumerator stunOverTime(Object source, float duration)
	{

		stunSources.Add (source);
		isStunned = (stunSources.Count > 0);

		yield return new WaitForSeconds (duration);
		if (stunSources.Contains (source)) {
			stunSources.Remove (source);
		} else {
			stunSources.RemoveAll (item => item == null);
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
	{if (weap) {
			if (!myWeapon.Contains (weap)) {
				myWeapon.Add (weap);
				foreach (Notify not in potentialNotify) {
					if (!weap.triggers.Contains (not)) {
						weap.triggers.Add (not);
					}
				}
			}
		}
	}

	public void removeWeapon(IWeapon weap)
	{if (!weap) {
			return;}
		
		if (myWeapon.Contains (weap)) {
			myWeapon.Remove(weap);
		}
		foreach (Notify not in potentialNotify) {

			if (weap.triggers.Contains (not)) {
				weap.triggers.Remove (not);
			}
		}


	}



	public bool isIdle()
		{ // will need to be refactored if units require a custom default state
		if(myState.GetType() == typeof(DefaultState))
		{return true;	}
		return false;
	}

	public void Attacked(UnitManager src) //I have been attacked, what do i do?
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
			return queuedStates.First.Value;
		} else {
		return null;}
	}

	public float getChaseRange()
	{return chaseRange;}

}