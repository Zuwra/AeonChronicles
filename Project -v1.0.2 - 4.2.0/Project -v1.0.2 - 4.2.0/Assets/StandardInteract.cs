using UnityEngine;
using System.Collections;

public class StandardInteract : MonoBehaviour, Iinteract {
	// Used on normal units for basic things like attacking, moving, patroling, etc.
	// if you want to make your own interact this class, extend this and override the commands down at the bottom


	protected UnitManager myManager;
	public bool attackWhileMoving;

	// Use this for initialization
	void Awake () {

		myManager = GetComponent<UnitManager> ();
		myManager.setInteractor (this);
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void initialize(){
		Awake ();
	}
	public virtual UnitState computeState(UnitState s)
	{

		return s;
	}

	// When creating other interactor classes, make sure to pass all relevant information into whatever new state is being created (IMover, IWeapon, UnitManager)
	public virtual void computeInteractions (Order order)
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

	// Attack move towards a ground location (Tab - ground)
	public virtual void  AttackMove(Order order)
	{
		if (myManager.myWeapon.Count > 0) {

			myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.gameObject.transform.position));
		}else {
			myManager.changeState (new MoveState (order.OrderLocation, myManager, true));
		}
	}

	// Right click on a obj/unit
	public virtual void Interact(Order order)
	{Debug.Log ("First Intereact");
		UnitManager manage = order.Target.GetComponent<UnitManager> ();
		if (!manage) {
			manage = order.Target.GetComponentInParent<UnitManager> ();
		}

		if (manage != null) {

			if (manage.PlayerOwner != this.gameObject.GetComponent<UnitManager>().PlayerOwner  ) {
				if (this.gameObject.GetComponent<UnitManager> ().myWeapon == null) {
					myManager.changeState (new FollowState (order.Target.gameObject, myManager));
				} else {
					//Debug.Log ("Ordering to interact " + manage.gameObject);
					myManager.changeState (new InteractState (manage.gameObject, myManager));
				}
			} else {
				myManager.changeState (new FollowState (order.Target.gameObject,  myManager));
			}
		}
	}
	//Right click on the ground
	public void Move(Order order)
	{
		if (attackWhileMoving &&  myManager.myWeapon.Count >0) {

			myManager.changeState (new AttckWhileMoveState (order.OrderLocation, myManager));
		} else {
			myManager.changeState (new MoveState (order.OrderLocation, myManager));
		}
	}

	//Stop, caps lock
	public void Stop(Order order)
	{myManager.changeState (new DefaultState ());
		
	}

	//Shift-Tab 
	public void Patrol(Order order)
	{
		myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.patrol, myManager, myManager.gameObject.transform.position));
	}

	//Shift-Caps
	public void HoldGround(Order order)
	{
		myManager.changeState (new HoldState(myManager));
	}

	//Right click on a unit/object. how is this different than interact? is it only on allied units?
	public virtual void Follow(Order order){

		Debug.Log ("First ORder");
		if (myManager.myWeapon.Count > 0) {

			myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.gameObject.transform.position));
		}else {
			myManager.changeState (new MoveState (order.OrderLocation, myManager));
		}
	}

}
