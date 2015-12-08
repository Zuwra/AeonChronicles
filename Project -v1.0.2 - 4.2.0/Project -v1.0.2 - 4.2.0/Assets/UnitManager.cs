using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//this class extends RTSObject through the Unit class
public class UnitManager : Unit,IOrderable{



	public bool isAStructure;

	public int PlayerOwner;
	public customMover cMover;
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
	
		RaceManager man = GameObject.Find ("GameRaceManager").GetComponent<RaceManager> ();
		man.addUnit (this.gameObject);

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
	{
		switch (order.OrderType) {
		//Stop Order----------------------------------------
		case Const.ORDER_STOP:
			changeState (new DefaultState (this, cMover, myWeapon));
			break;
			
		//Move Order ---------------------------------------------
		case Const.ORDER_MOVE_TO:
		
			if(attackWhileMoving)
				{
				Debug.Log("attack while moving");
				changeState (new AttckWhileMoveState(order.OrderLocation, this, cMover, myWeapon));}
			else
			{Debug.Log("moving");
				changeState (new MoveState (order.OrderLocation, this, cMover, myWeapon));}
				//cMover.resetMoveLocation(order.OrderLocation);
			
			break;
			
		case Const.ORDER_ATTACK:
		//	Debug.Log("Attacking");
			changeState (new InteractState (order.Target.gameObject,this, cMover, myWeapon));
			
			break;
		

		case Const.ORDER_AttackMove:
			changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command,this, cMover, myWeapon, this.gameObject.transform.position));
		
			break;
		
	}


	}


	void OnTriggerEnter(Collider other)
	{
		//Debug.Log (this.gameObject.name + "   targeting " + other.gameObject.name + "  " + other.gameObject.tag);
		if (!other.isTrigger) {
			
			//if (other.gameObject.layer.Equals("Unit"))
			UnitManager manage = other.GetComponent<UnitManager> ();
			if (manage == null) {
				manage = other.GetComponentInParent<UnitManager> ();
			}
			
			if (manage != null) {
				if (other.GetComponent<UnitManager> ().PlayerOwner != PlayerOwner) {
				
					enemies.Add (other.gameObject);
				}
				else{allies.Add(other.gameObject);}
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{enemies.Remove (other.gameObject);
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



}




