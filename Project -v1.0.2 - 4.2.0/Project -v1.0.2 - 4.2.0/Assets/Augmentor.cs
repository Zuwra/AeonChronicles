using UnityEngine;
using System.Collections;

public class Augmentor : TargetAbility, Iinteract, Modifier {


	UnitManager manager;
	GameObject attached;
	DetachAugment detacher;
	IMover myMover;
	public rotater myRotate;
	public float SpeedPlus = 1.35f;

	// Use this for initialization
	void Start () {
		
		myType = type.target;
		manager.myWeapon.Clear ();
		detacher = GetComponent<DetachAugment> ();
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

			StartCoroutine (delayCast());
			return;
		}

		myRotate.speed *= 3;

		manager.myStats.myHeight = UnitTypes.HeightType.Ground;
		manager.myStats.SetTags ();
		detacher.allowDetach (true);
		attached = target;
		target.GetComponent<UnitManager> ().myStats.addDeathTrigger (this);

		AAP.myAugment = this.gameObject;

		this.gameObject.transform.position = target.transform.position+  target.transform.rotation * AAP.attachPoint;


		MissileArmer armer = attached.GetComponent<MissileArmer> ();

		if (armer) {
			armer.shields = false;
			foreach (IWeapon w in GetComponents<IWeapon>()) {
				if (!manager.myWeapon.Contains (w)) {

					manager.myWeapon.Add (w);
					manager.getUnitStats ().attackPriority = 3;
				}

			}
		} else {
			manager.getUnitStats ().attackPriority = 3;
		}
		UnitManager unitMan = target.GetComponent<UnitManager> ();



		OreDispenser OD = target.GetComponent<OreDispenser> ();
		if (OD) {
			OD.returnRate = 1.3f;
		} 
		else if (unitMan.UnitName.Contains("Yard") ||unitMan.UnitName == "Armory" ||unitMan.UnitName.Contains("Lab") || unitMan.UnitName.Contains("Bay") || unitMan.UnitName.Contains("Academy")|| unitMan.UnitName.Contains("Flux")  ) {
			unitMan.GetComponent<Selected> ().setCooldownColor (Color.yellow);
			int xxx = 0;
			foreach (Ability bu in unitMan.abilityList){
				if (bu) {
					if (bu is UnitProduction) {
						((UnitProduction)bu).setBuildRate (SpeedPlus);
						if(xxx>1 && !unitMan.UnitName.Contains("Yard"))
						{
							((UnitProduction)bu).active = true;
							bu.SendMessage ("attachAddon", SendMessageOptions.DontRequireReceiver);
						}

					}
				}
				xxx++;
			}

		}


		if (target.GetComponent<Selected> ().IsSelected) {
			RaceManager.updateActivity ();
		}


	}

	//Triggers if the attached building dies
	public float modify(float d, GameObject src, DamageTypes.DamageType theType )
	{
		Unattach ();
		manager.myStats.kill(null);
		return d;
	}

	void OnDestroy()
	{Unattach ();
		
	}



	public void changeSpeed(float n)
	{
		SpeedPlus += n;
		if (!target) {
			return;}
		UnitManager unitMan = attached.GetComponent<UnitManager> ();

		OreDispenser OD = attached.GetComponent<OreDispenser> ();
		if (OD) {
			OD.returnRate = 1.3f;
		} 
		else if (unitMan.UnitName.Contains("Yard") ||unitMan.UnitName == "Armory" ||unitMan.UnitName.Contains("Lab") ||unitMan.UnitName.Contains("Bay") || unitMan.UnitName.Contains("Academy") || unitMan.UnitName.Contains("Flux") ) {

			int xxx = 0;
			foreach (Ability bu in unitMan.abilityList){
				if (bu) {
					if (bu is UnitProduction) {
						((UnitProduction)bu).setBuildRate (1);
						if(xxx>1 && !unitMan.UnitName.Contains("Yard"))
						{
							((UnitProduction)bu).active = false;
						
						}

					}
				}
				xxx++;
			}

		}
	}

	public void Unattach()
	{if (!attached) {
			return;}
		manager.getUnitStats ().attackPriority = 2;
		myRotate.speed /= 3;
		manager.myStats.myHeight = UnitTypes.HeightType.Air;
		attached.GetComponent<UnitManager> ().myStats.removeDeathTrigger (this);
		MissileArmer armer = attached.GetComponent<MissileArmer> ();
		UnitManager man = attached.GetComponent<UnitManager> ();

		manager.myStats.SetTags ();

		if (armer) {
			armer.shields = true;
			manager.myWeapon.Clear ();
		}

		OreDispenser OD = attached.GetComponent<OreDispenser> ();
		if (OD) {
			OD.returnRate = 1;
		} else if (man.UnitName.Contains ("Yard") || man.UnitName == "Armory" || man.UnitName.Contains ("Lab") || man.UnitName.Contains ("Bay") || man.UnitName.Contains ("Academy")|| man.UnitName.Contains("Flux")) {
			man.GetComponent<Selected> ().setCooldownColor (Color.white);
			int xxx = 0;
		
			foreach (Ability bu in man.abilityList) {
				if (bu) {
					if (bu is UnitProduction) {

						((UnitProduction)bu).setBuildRate (1);
						if (xxx > 1 && !man.UnitName.Contains ("Yard")) {
							bu.SendMessage ("removeAddon", SendMessageOptions.DontRequireReceiver);
							((UnitProduction)bu).active = false;
						}

					}
				}
				xxx++;
			}

		}



	
		if (target && target.GetComponent<Selected> ().IsSelected) {
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

	public void Dying()
	{
		if (attached) {
			UnitManager man = attached.GetComponent<UnitManager> ();
			if (man && (man.UnitName.Contains ("Yard") || man.UnitName == "Armory" || man.UnitName.Contains ("Lab") || man.UnitName.Contains ("Bay") || man.UnitName.Contains ("Academy")|| man.UnitName.Contains("Flux"))) {
				
				attached.GetComponent<Selected> ().setCooldownColor (Color.white);
			}
		}
	}

	override public void setAutoCast(bool offOn)
	{
	}

	override
	public  bool Cast(GameObject tar, Vector3 location){
		target = tar;
		attached = tar;

		Vector3 attachSpot = target.transform.position;

		this.gameObject.transform.position =  target.transform.rotation * attachSpot;
		///myMover = manager.cMover;
		/// 
	

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

		if (m.PlayerOwner == 3) {
			return true;}

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
	public void computeInteractions (Order order )
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

		if (attached) {
			if (s is DefaultState) {
				return new HoldState (manager);
			}
		}
		return s;
	}

	// Attack move towards a ground location (Tab - ground)
	public void  AttackMove(Order order)
	{
		if (attached) {
			manager.changeState (new MoveState (order.OrderLocation, manager,true),false,order.queued);

		}
	}

	// Right click on a objt/unit
	public void Interact(Order order)
	{

		if (!attached) {
			if (!attached && isValidTarget (order.Target, Vector3.zero)) {
				manager.UseTargetAbility (order.Target, Vector3.zero, 0, false);

				return;
			}
		} else {
			if (isValidTarget (order.Target, Vector3.zero)) {
	
				manager.UseTargetAbility (order.Target, Vector3.zero, 0, false);
			}
		}

		UnitManager manage = order.Target.GetComponent<UnitManager> ();
		if (!manage) {
			manage = order.Target.GetComponentInParent<UnitManager> ();
		}

		if (manage != null) {

			if (!attached && manage.PlayerOwner != this.gameObject.GetComponent<UnitManager>().PlayerOwner ) {

				manager.changeState (new FollowState (order.Target.gameObject, manager),false,order.queued);
			} else if(!attached && isValidTarget(order.Target, Vector3.zero)){
				manager.UseTargetAbility (order.Target, Vector3.zero, 0, false);

				}
			} else {

			manager.changeState (new FollowState (order.Target.gameObject,  manager),false,order.queued);
			}

	}
	//Right click on the ground
	public void Move(Order order)
	{
		if (!attached) {
			//Debug.Log ("Im moving");
			manager.changeState (new MoveState (order.OrderLocation, manager),false,order.queued);

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
			manager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.patrol, manager, manager.gameObject.transform.position),false,order.queued);
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
		if (order.Target == this.gameObject) {
			return;
		}

		if (!order.Target) {
		
			return;}

		if (!attached) {
			if (isValidTarget (order.Target, Vector3.zero)) {
				manager.UseTargetAbility (order.Target, Vector3.zero, 0, order.queued);

			} else {
				manager.changeState (new MoveState (order.OrderLocation, manager, true), false, order.queued);
	
				if (target) {
					target = null;
				}
			}
		} else {
		
			int count = 0; // The uagmentor will only target a thing if it is attached and if it is the only uagmentor selected
			foreach (RTSObject obj in SelectedManager.main.ActiveObjectList()) {
				if (obj.getUnitManager ().UnitName == "Augmentor") {
					count++;
					if (count > 1) {
						return;}
				}
			}

				if (isValidTarget (order.Target, Vector3.zero)) {

					manager.UseTargetAbility (order.Target, Vector3.zero, 0, order.queued);
					//Debug.Log ("Ordered to follow");
				}
			}

		}






}
