using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//this class extends RTSObject through the Unit class
public class UnitManager : Unit,IOrderable{


	public string UnitName;
	public bool isAStructure;

	public int PlayerOwner;
	public IMover cMover;
	public IWeapon myWeapon;
	public bool attackWhileMoving = false;
	public UnitStats myStats;

	public VisionSphere vision;
	public float visionRange;
	//public VisionComponent myVision;

	public Ability QAbility;
	public Ability WAbility;
	public Ability EAbility;
	public Ability RAbility;


	public AbstractCost myCost;

	SphereCollider visionSphere;
	public float AbilityPriority; //Determines which abilities display on first/second/third rows according to a  grid system q-r,a-f,z-v  any more than three units, it goes to another page
	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> allies = new List<GameObject>();

	private UnitState myState;



	// Use this for initialization
	void Start () {
		if (visionSphere == null) {
			visionSphere = this.gameObject.GetComponent<SphereCollider>();}

	
		vision = GetComponentInChildren<VisionSphere> ();

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

		if(myCost == null)
			{
			myCost = gameObject.GetComponent<AbstractCost>();
		}
	
		GameManager man = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ();
		if (PlayerOwner != man.playerNumber) {
			this.gameObject.tag = "Enemy";
		} else {
			this.gameObject.tag = "Player";
		}
	
		man.initialize ();
		man.playerList [PlayerOwner - 1].addUnit (this.gameObject);
		man.playerList [PlayerOwner - 1].UnitCreated(myStats.supply);


		visionSphere.radius = visionRange + gameObject.GetComponent<CharacterController>().radius;

		if (cMover != null) {
			changeState (new DefaultState (this, cMover, myWeapon));
		}

	}



	public GameObject getObject()
	{return this.gameObject;}



	
	// Update is called once per frame
	void Update () {
		if (myState != null) {
			myState.Update ();
		} 




	}


	public bool UseQAbility()
	{
	
		if (QAbility != null) {

			if(QAbility.canActivate())
			{
				return QAbility.Activate();
			}
		}
		return true;
	}

	public bool UseWAbility()
	{
		if (WAbility != null) {
			if(WAbility.canActivate())
			{return WAbility.Activate();}
					}

		return true;
			}


	public bool UseEAbility()
	{if (EAbility != null) {
			if(EAbility.canActivate())
			{return EAbility.Activate();}
							}
	
		return true;}


	public bool UseRAbility()
	{if (RAbility != null) {
			if(RAbility.canActivate())
			{return RAbility.Activate();}
									}
		return true;}



	public void GiveOrder (Order order)
	{if (!isAStructure) {
			switch (order.OrderType) {
			//Stop Order----------------------------------------
			case Const.ORDER_STOP:
				changeState (new DefaultState (this, cMover, myWeapon));
				break;
			
			//Move Order ---------------------------------------------
			case Const.ORDER_MOVE_TO:
		
				if (attackWhileMoving && myWeapon) {

					changeState (new AttckWhileMoveState (order.OrderLocation, this, cMover, myWeapon));
				} else {
					changeState (new MoveState (order.OrderLocation, this, cMover, myWeapon));
				}
				//cMover.resetMoveLocation(order.OrderLocation);
			
				break;
			
			case Const.ORDER_ATTACK:
		//	Debug.Log("Attacking");
				changeState (new InteractState (order.Target.gameObject, this, cMover, myWeapon));
			
				break;
		

			case Const.ORDER_AttackMove:
				if (myWeapon)
					changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, this, cMover, myWeapon, this.gameObject.transform.position));
				else {
					changeState (new MoveState (order.OrderLocation, this, cMover, myWeapon));
				}
				break;
		
			}
		}

	}


	void OnTriggerEnter(Collider other)
	{
		//need to set up calls to listener components
		//this will need to be refactored for team games
		if (!other.isTrigger) {

			UnitManager manage = other.gameObject.GetComponent<UnitManager>();
			if(manage){
			if(manage.PlayerOwner != PlayerOwner)
				{if(!other.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.invisible))
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
		} else if (allies.Contains (other.gameObject)) {
			allies.Remove(other.gameObject);
		}
		enemies.Remove (other.gameObject);
		if (enemies.Count == 0) {
			
			//myManager.currentState = UnitManager.state.stop;
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
		float bestPriority = 0;

		for (int i = 0; i < enemies.Count; i ++) {
			if (enemies[i] != null) {

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




	public void changeState(UnitState nextState)
		{
		{myState = nextState;

		}


		if (nextState.GetType is AttackMoveState) {

			((AttackMoveState)nextState).setHome(this.gameObject.transform.position);
		}
	}


	public void cleanEnemy()
	{
		enemies.RemoveAll(item => item == null);
	
	}


	public void setWeapon(IWeapon weap)
		{
		myWeapon = weap;
		myState.myWeapon = weap;
	}

	public bool isIdle()
		{
		if(myState.GetType() == typeof(DefaultState))
		{return true;	}
		return false;
	}


}




