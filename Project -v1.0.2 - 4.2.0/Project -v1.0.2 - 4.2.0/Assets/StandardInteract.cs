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
		

	public void initialize(){
		Awake ();
	}
	public virtual UnitState computeState(UnitState s)
	{
		//Debug.Log ("Getting called up here");
		return s;
	}

	// When creating other interactor classes, make sure to pass all relevant information into whatever new state is being created (IMover, IWeapon, UnitManager)
	public virtual void computeInteractions (Order order)
	{
	//	Debug.Log ("Queued " + order.queued);
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
			if (myManager.cMover) {
				Move (order);
			}
				break;

			case Const.ORDER_Interact:
				Interact (order);
				break;

		// ATTACK MOVE - Move towards a location and attack enemies on the way.
		case Const.ORDER_AttackMove:
		//	Debug.Log ("Setting to attack move");
			if (myManager.cMover) {
				AttackMove (order);
			}

				break;


			// Right click on a allied unit
			case Const.ORDER_Follow:
				Follow (order);
				break;



			}


	}

	// Attack move towards a ground location (Tab - ground)
	public virtual void  AttackMove(Order order)
	{if (!myManager) {
			return;
		}
		if (myManager.myWeapon.Count > 0) {

			myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.gameObject.transform.position),false,order.queued);
		}else {
			myManager.changeState (new MoveState (order.OrderLocation, myManager, true),false,order.queued);
		}
	}

	// Right click on a obj/unit
	public virtual void Interact(Order order)
	{//Debug.Log ("First Intereact");
		UnitManager manage = order.Target.GetComponent<UnitManager> ();
		if (!manage) {
			manage = order.Target.GetComponentInParent<UnitManager> ();
		}

		if (manage != null) {

			if (manage ==myManager) {
				return;
			}
			if (manage.PlayerOwner != myManager.PlayerOwner  ) {
				if (this.gameObject.GetComponent<UnitManager> ().myWeapon.Count == 0) {
					myManager.changeState (new FollowState (order.Target.gameObject, myManager),false,order.queued);
				} else {
					//Debug.Log ("Ordering to interact " + manage.gameObject);
					myManager.changeState (new InteractState (manage.gameObject, myManager),false,order.queued);
				}
			} else {
				myManager.changeState (new FollowState (order.Target.gameObject,  myManager),false,order.queued);
			}
		}
	}
	//Right click on the ground
	public void Move(Order order)
	{//Debug.Log (" Ordering " + attackWhileMoving + "   " + myManager.myWeapon.Count);
		if (attackWhileMoving &&  myManager.myWeapon.Count >0) {
			//Debug.Log ("Setting to attack move");
			myManager.changeState (new AttckWhileMoveState (order.OrderLocation, myManager),false,order.queued);
		} else {
			myManager.changeState (new MoveState (order.OrderLocation, myManager, false),false,order.queued);
		}
	}

	//Stop, caps lock
	public void Stop(Order order)
	{myManager.changeState (new DefaultState ());
		
	}

	//Shift-Tab 
	public void Patrol(Order order)
	{
		myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.patrol, myManager, myManager.gameObject.transform.position),false,order.queued);
	}

	//Shift-Caps
	public void HoldGround(Order order)
	{
		myManager.changeState (new HoldState(myManager));
	}

	//Right click on a unit/object. how is this different than interact? is it only on allied units?
	public virtual void Follow(Order order){

		if (order.Target == this.gameObject) {
			return;
		}
	//	Debug.Log ("First ORder");
		if (myManager.myWeapon.Count > 0) {

			myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.gameObject.transform.position),false,order.queued);
		}else {
			myManager.changeState (new MoveState (order.OrderLocation, myManager),false,order.queued);
		}
	}

}
