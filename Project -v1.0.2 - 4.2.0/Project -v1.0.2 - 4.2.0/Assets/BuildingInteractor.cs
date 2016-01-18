using UnityEngine;
using System.Collections;

public class BuildingInteractor : MonoBehaviour, Iinteract {

	private UnitManager myManager;

	public Vector3 rallyPoint = Vector3.zero;
	public GameObject rallyUnit;
	public bool AttackMoveSpawn;



	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		myManager.setInteractor (this);

	}

	// Update is called once per frame
	void Update () {

	}
	public void initialize(){
		Start ();
	}


	public void computeInteractions (Order order)
	{

		switch (order.OrderType) {
		//Stop Order----------------------------------------
		case Const.ORDER_STOP:
			
		//	myManager.changeState (new DefaultState (myManager, myManager.cMover, myManager.myWeapon));
			break;

			//Move Order ---------------------------------------------
		case Const.ORDER_MOVE_TO:
			AttackMoveSpawn = false;
			rallyPoint = order.OrderLocation;
			rallyUnit = null;
			break;

		case Const.ORDER_ATTACK:
			AttackMoveSpawn = false;
			rallyUnit = order.Target.gameObject;
			rallyPoint = Vector3.zero;
			break;


		case Const.ORDER_AttackMove:
			AttackMoveSpawn = true;
			rallyPoint = order.OrderLocation;
			rallyUnit = null;

			break;
		case Const.ORDER_Follow:
			AttackMoveSpawn = false;
			rallyUnit = order.Target.gameObject;
			rallyPoint= Vector3.zero;
			break;



		}





	}



}
