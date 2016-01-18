using UnityEngine;
using System.Collections;

public class StandardInteract : MonoBehaviour, Iinteract {

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

	public void computeInteractions (Order order)
	{

			switch (order.OrderType) {
			//Stop Order----------------------------------------
			case Const.ORDER_STOP:
			myManager.changeState (new DefaultState (myManager, myManager.cMover, myManager.myWeapon));
				break;

				//Move Order ---------------------------------------------
		case Const.ORDER_MOVE_TO:


			if (attackWhileMoving &&  myManager.myWeapon) {

				myManager.changeState (new AttckWhileMoveState (order.OrderLocation, myManager, myManager.cMover, myManager.myWeapon));
				} else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager, myManager.cMover, myManager.myWeapon));
				}
				break;

			case Const.ORDER_ATTACK:
				myManager.changeState (new InteractState (order.Target.gameObject, myManager, myManager.cMover, myManager.myWeapon));

				break;


		case Const.ORDER_AttackMove:
			if (myManager.myWeapon) {
				
				myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.cMover, myManager.myWeapon, myManager.gameObject.transform.position));
			}else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager, myManager.cMover, myManager.myWeapon));
				}
				break;
			case Const.ORDER_Follow:

			myManager.changeState (new FollowState (order.Target.gameObject,  myManager, myManager.cMover, myManager.myWeapon));
				break;



			}


	}



}
