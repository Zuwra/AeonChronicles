using UnityEngine;
using System.Collections;

public class StandardInteract : MonoBehaviour, Iinteract {
	// Used on normal units for basic things like attacking, moving, patroling, etc.


	private UnitManager myManager;
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


	// When creating other interactor classes, make sure to pass all relevant information into whatever new state is being created (IMover, IWeapon, UnitManager)
	public void computeInteractions (Order order)
	{
		// HOLD GROUND -----------------------------------------------------
			switch (order.OrderType) {
		case Const.Order_HoldGround:
			myManager.changeState (new HoldState(myManager));

			break;


		// PATROL ------------------------------------------------------
		case Const.Order_Patrol:
			myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.patrol, myManager, myManager.gameObject.transform.position));

			break;

		//STOP----------------------------------------
		case Const.ORDER_STOP:
			myManager.changeState (new DefaultState ());

				break;

		//MOVE - right clicked on ground ---------------------------------------------
		case Const.ORDER_MOVE_TO:


			if (attackWhileMoving &&  myManager.myWeapon.Count >0) {

				myManager.changeState (new AttckWhileMoveState (order.OrderLocation, myManager));
				} else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager));
				}
				break;


		// Right clicked on an enemy unit
		case Const.ORDER_Interact:

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
				

				break;

		// ATTACK MOVE - Move towards a location and attack enemies on the way.
		case Const.ORDER_AttackMove:
			if (myManager.myWeapon.Count > 0) {
				
				myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.gameObject.transform.position));
			}else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager));
				}
				break;


			// Right click on a allied unit
			case Const.ORDER_Follow:

			myManager.changeState (new FollowState (order.Target.gameObject,  myManager));
				break;



			}


	}



}
