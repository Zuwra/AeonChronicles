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

	IEnumerator delayCast()
	{
		yield return new WaitForSeconds (1);
		Cast ();
	}


	override
	public void Cast(){
		
		Unattach ();
		if (!target) {
			manager.changeState (new DefaultState ());
			return;
		}

		AugmentAttachPoint AAP = target.GetComponent<AugmentAttachPoint> ();
		if (AAP.myAugment) {
		
			return;}
		//Make sure its not under construction
		BuildingInteractor BI = target.GetComponent<BuildingInteractor> ();

		if (!BI.ConstructDone ()) {
			//Debug.Log ("Delaying check");
			StartCoroutine (delayCast());
			return;
		}

	//	Debug.Log ("Attaching");

		manager.myStats.myHeight = UnitTypes.HeightType.Ground;
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

					manager.myWeapon.Add (w);
				}

			}
		}

		OreDispenser OD = target.GetComponent<OreDispenser> ();
		if (OD) {
			OD.returnRate = 1.2f;
		}

		else if (target.GetComponent<UnitManager> ().UnitName == "Construction Yard") {
			foreach (BuildUnit bu in target.GetComponents<BuildUnit>()) {
				//bu.buildTime *= .65f;
				bu.myCost.cooldown = bu.buildTime;
				bu.setBuildRate (1.35f);
			}

		}

		else if (target.GetComponent<UnitManager> ().UnitName == "Armory") {
			foreach (buildTurret bt in target.GetComponents<buildTurret>()) {
				if (bt.Name.Contains ("Repair")) {
					bt.active = true;
				}
			}

		}

		if (GetComponent<Selected> ().IsSelected) {
			RaceManager.updateActivity ();
		} else if (target.GetComponent<Selected> ().IsSelected) {
			RaceManager.updateActivity ();
		}


	}

	//Triggers if the attached building dies
	public float modify(float d, GameObject src)
	{
		Unattach ();
		manager.myStats.kill(null);
		return d;
	}

	void OnDestroy()
	{Unattach ();
		
	}


	public void Unattach()
	{if (!attached) {
			return;}
		manager.myStats.myHeight = UnitTypes.HeightType.Air;
		attached.GetComponent<UnitManager> ().myStats.removeDeathTrigger (this);
		MissileArmer armer = attached.GetComponent<MissileArmer> ();

		if (armer) {
			armer.shields = true;
			manager.myWeapon.Clear ();
		}

		else if (attached.GetComponent<UnitManager> ().UnitName == "Construction Yard") {
			foreach (BuildUnit bu in attached.GetComponents<BuildUnit>()) {
				//bu.buildTime /= .65f;

				bu.myCost.cooldown = bu.buildTime;
				bu.setBuildRate (1);
			}

		}

		OreDispenser OD = attached.GetComponent<OreDispenser> ();
		if (OD) {
			OD.returnRate = 1;
		}

		else if (attached.GetComponent<UnitManager> ().UnitName == "Armory") {
			foreach (buildTurret bt in attached.GetComponents<buildTurret>()) {
				if (bt.Name.Contains ("Repair")) {
					bt.active =false;
				}
			}

		}
		if (GetComponent<Selected> ().IsSelected) {
			
			RaceManager.updateActivity ();
		}
		else if (target.GetComponent<Selected> ().IsSelected) {
			RaceManager.updateActivity ();
		}

		attached.GetComponent<AugmentAttachPoint> ().myAugment = null;
		attached = null;
		detacher.allowDetach (false);

		RaycastHit objecthit;

		Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

		if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, (~8))) {

			down =objecthit.point;
			manager.changeState (new MoveState (down, manager,true));
		}


	}

	override public void setAutoCast()
	{
	}

	override
	public  bool Cast(GameObject tar, Vector3 location){
		target = tar;
		attached = tar;

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
		AugmentAttachPoint AAP =target.GetComponent<AugmentAttachPoint> ();
		if (!AAP) {
			return false;}
		if (AAP.myAugment) {
			return false;}

		if(target.GetComponent<OreDispenser> () )
		{return true;
		}

		UnitManager m = target.GetComponent<UnitManager> ();
		if (m == null) {
			return false;}

		if (m.PlayerOwner != manager.PlayerOwner) {
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
	{

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

		if (s is DefaultState) {
			return new HoldState(manager);
		}
		return s;
	}

	// Attack move towards a ground location (Tab - ground)
	public void  AttackMove(Order order)
	{
		if (attached) {
			manager.changeState (new MoveState (order.OrderLocation, manager,true));

		}
	}

	// Right click on a objt/unit
	public void Interact(Order order)
	{
		if (!attached) {
			if (!attached && isValidTarget (order.Target, Vector3.zero)) {
				manager.UseTargetAbility (order.Target, Vector3.zero, 0);
				//Debug.Log ("Ordered to follow");
				return;
			}
		}



		UnitManager manage = order.Target.GetComponent<UnitManager> ();
		if (!manage) {
			manage = order.Target.GetComponentInParent<UnitManager> ();
		}

		if (manage != null) {

			if (!attached && manage.PlayerOwner != this.gameObject.GetComponent<UnitManager>().PlayerOwner  ) {

				manager.changeState (new FollowState (order.Target.gameObject, manager));
			} else if(!attached && isValidTarget(order.Target, Vector3.zero)){
				manager.UseTargetAbility (order.Target, Vector3.zero, 0);
					
				}
			} else {
			
				manager.changeState (new FollowState (order.Target.gameObject,  manager));
			}

	}
	//Right click on the ground
	public void Move(Order order)
	{
		if (!attached) {
			
			manager.changeState (new MoveState (order.OrderLocation, manager,false));
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
		//	target = null;
		}
		//manager.changeState (new HoldState(manager));
	}

	//Right click on a unit/object. how is this different than interact? is it only on allied units?
	public void Follow(Order order){
		if (!attached) {
			if (!attached && isValidTarget (order.Target, Vector3.zero)) {
				manager.UseTargetAbility (order.Target, Vector3.zero, 0);
				//Debug.Log ("Ordered to follow");
			}
			else{
				manager.changeState (new MoveState (order.OrderLocation,manager,true));
				if (target) {
					target = null;}
			}
		}

	}




}
