using UnityEngine;
using System.Collections;

public class Augmentor : TargetAbility, Iinteract, Modifier {


	UnitManager manager;
	GameObject attached;
	DetachAugment detacher;
	IMover myMover;
	// Use this for initialization
	void Start () {
		
		myType = type.target;
		manager.myWeapon.Clear ();
		detacher = GetComponent<DetachAugment> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	override
	public void Cast(){
		
		Unattach ();
		AugmentAttachPoint AAP = target.GetComponent<AugmentAttachPoint> ();
		if (AAP.myAugment) {
		
			return;}
		detacher.allowDetach (true);
		attached = target;
		target.GetComponent<UnitManager> ().myStats.addDeathTrigger (this);

		AAP.myAugment = this.gameObject;
		Vector3 attachSpot = target.transform.position+ AAP.attachPoint;

		this.gameObject.transform.position = attachSpot;


		MissileArmer armer = attached.GetComponent<MissileArmer> ();

		if (armer) {
			armer.shields = false;
			foreach (IWeapon w in GetComponents<IWeapon>()) {
				if (!manager.myWeapon.Contains (w)) {
					Debug.Log ("Adding");
					manager.myWeapon.Add (w);
				}

			}
		}

		if (target.GetComponent<UnitManager> ().UnitName == "Construction Yard") {
			foreach (BuildUnit bu in target.GetComponents<BuildUnit>()) {
				bu.buildTime *= .65f;
				bu.myCost.cooldown = bu.buildTime;
			}

		}

		if (target.GetComponent<UnitManager> ().UnitName == "Turret Shop") {
			foreach (buildTurret bt in target.GetComponents<buildTurret>()) {
				if (bt.Name.Contains ("Repair")) {
					bt.active = true;
				}
			}

		}

		if (GetComponent<Selected> ().IsSelected) {
			RaceManager.updateActivity ();
		}


	}

	//Triggers if the attached building dies
	public float modify(float d, GameObject src)
	{
		manager.myStats.kill(null);
		return d;
	}


	public void Unattach()
	{if (!attached) {
			return;}

		attached.GetComponent<UnitManager> ().myStats.removeDeathTrigger (this);
		MissileArmer armer = attached.GetComponent<MissileArmer> ();

		if (armer) {
			armer.shields = true;
			manager.myWeapon.Clear ();
		}

		if (attached.GetComponent<UnitManager> ().UnitName == "Construction Yard") {
			foreach (BuildUnit bu in attached.GetComponents<BuildUnit>()) {
				bu.buildTime /= .65f;
				bu.myCost.cooldown = bu.buildTime;
			}

		}

		if (attached.GetComponent<UnitManager> ().UnitName == "Turret Shop") {
			foreach (buildTurret bt in attached.GetComponents<buildTurret>()) {
				if (bt.Name.Contains ("Repair")) {
					bt.active =false;
				}
			}

		}
		if (GetComponent<Selected> ().IsSelected) {
			Debug.Log ("Updating UI");
			RaceManager.updateActivity ();
		}
		attached.GetComponent<AugmentAttachPoint> ().myAugment = null;
		attached = null;
		detacher.allowDetach (false);


	}

	override public void setAutoCast()
	{
	}

	override
	public  bool Cast(GameObject tar, Vector3 location){
		target = tar;
		attached = tar;
		Debug.Log ("Casting 2");
		Vector3 attachSpot = target.transform.position;

		this.gameObject.transform.position = attachSpot;
		///myMover = manager.cMover;
		/// 
	



		Debug.Log ("State " + manager.getState());
		return false;

	}

	override
	public  bool isValidTarget (GameObject target, Vector3 location){

		if (target == null) {
			return false;
		}

		UnitManager m = target.GetComponent<UnitManager> ();
		if (m == null) {
			return false;}

		if (m.PlayerOwner != manager.PlayerOwner) {
			return false;}

		AugmentAttachPoint AAP = m.GetComponent<AugmentAttachPoint> ();
		if (!AAP) {
			return false;}
		if (AAP.myAugment) {
			return false;}

		return true;
	}

	override
	public continueOrder canActivate(bool showError){

		continueOrder order = new continueOrder ();


		order.nextUnitCast = false;
		return order;
	}

	override
	public void Activate()
	{Debug.Log ("Activating");

	}  // returns whether or not the next unit in the same group should also cast it




	// Use this for initialization
	void Awake () {
		manager = GetComponent<UnitManager> ();
		manager.setInteractor (this);

	}



	public void initialize(){
		Awake ();
	}




	// When creating other interactor classes, make sure to pass all relevant information into whatever new state is being created (IMover, IWeapon, UnitManager)
	public void computeInteractions (Order order)
	{

		switch (order.OrderType) {
		case Const.Order_HoldGround:

			HoldGround (order);
			break;


		case Const.Order_Patrol:
			Patrol (order);
			break;


		case Const.ORDER_STOP:
			Stop (order);
			break;

		case Const.ORDER_MOVE_TO:
			Move (order);
			break;

		case Const.ORDER_Interact:
			Interact (order);
			break;

			// ATTACK MOVE - Move towards a location and attack enemies on the way.
		case Const.ORDER_AttackMove:
			//	Debug.Log ("Setting to attack move");
			AttackMove (order);

			break;


			// Right click on a allied unit
		case Const.ORDER_Follow:
			Follow (order);
			break;



		}


	}

	public UnitState computeState(UnitState s)
	{
		if (s is AbilityFollowState) {
			Unattach ();
		}
		return s;
	}

	// Attack move towards a ground location (Tab - ground)
	public void  AttackMove(Order order)
	{
		if (attached) {
			manager.changeState (new MoveState (order.OrderLocation, manager));

		}
	}

	// Right click on a objt/unit
	public void Interact(Order order)
	{
		UnitManager manage = order.Target.GetComponent<UnitManager> ();
		if (!manage) {
			manage = order.Target.GetComponentInParent<UnitManager> ();
		}

		if (manage != null) {

			if (!attached && manage.PlayerOwner != this.gameObject.GetComponent<UnitManager>().PlayerOwner  ) {
					
				manager.changeState (new FollowState (order.Target.gameObject, manager));
			} else if(attached && attached.GetComponent<MissileArmer>()){
					
					//manager.changeState (new InteractState (manage.gameObject,manager));
				}
			} else {
				manager.changeState (new FollowState (order.Target.gameObject,  manager));
			}

	}
	//Right click on the ground
	public void Move(Order order)
	{
		if (!attached) {
			
			manager.changeState (new MoveState (order.OrderLocation, manager));
		}

		if (target) {
			target = null;}
	}

	//Stop, caps lock
	public void Stop(Order order)
	{manager.changeState (new DefaultState ());
		if (target) {
			target = null;}
	}

	//Shift-Tab 
	public void Patrol(Order order)
	{if (target) {
			target = null;}
		if(!attached)
		manager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.patrol, manager, manager.gameObject.transform.position));
	}

	//Shift-Caps
	public void HoldGround(Order order)
	{if (target) {
			target = null;}
		manager.changeState (new HoldState(manager));
	}

	//Right click on a unit/object. how is this different than interact? is it only on allied units?
	public void Follow(Order order){
		if (!attached) {


			manager.changeState (new MoveState (order.OrderLocation,manager));
		}
		if (target) {
			target = null;}
	}




}
