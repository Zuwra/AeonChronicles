using UnityEngine;
using System.Collections;

public class BuildingInteractor : MonoBehaviour, Iinteract {

	private UnitManager myManager;

	public Vector3 rallyPoint = Vector3.zero;
	public GameObject rallyUnit;
	public bool AttackMoveSpawn;
	public GameObject RallyPointObj;
	public LineRenderer myLine;


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
			if (RallyPointObj) {
				if (myLine) {
					myLine.SetPositions (new Vector3[]{ this.gameObject.transform.position, order.OrderLocation });
				}
				RallyPointObj.transform.position = order.OrderLocation;
			}
			GetComponent<Selected> ().RallyUnit = null;
			rallyPoint = order.OrderLocation;
			rallyUnit = null;
			break;



		case Const.ORDER_AttackMove:
			AttackMoveSpawn = true;
			GetComponent<Selected> ().RallyUnit =null;
			if (RallyPointObj) {
				if (myLine) {
					myLine.SetPositions (new Vector3[]{ this.gameObject.transform.position, order.OrderLocation });
				}
				RallyPointObj.transform.position = order.OrderLocation;
			}
			rallyPoint = order.OrderLocation;
			rallyUnit = null;

			break;
		case Const.ORDER_Interact:
			AttackMoveSpawn = false;
			rallyUnit = order.Target.gameObject;
			GetComponent<Selected> ().RallyUnit = order.Target.gameObject;
			if (RallyPointObj) {
				
				RallyPointObj.transform.position = order.Target.gameObject.transform.position;
				if (myLine) {
					myLine.SetPositions (new Vector3[]{ this.gameObject.transform.position, order.Target.gameObject.transform.position });
				}

			}
			rallyPoint= Vector3.zero ;
			break;

		case Const.ORDER_Follow:
			AttackMoveSpawn = false;
			rallyUnit = order.Target.gameObject;
			if (RallyPointObj) {

				RallyPointObj.transform.position = order.Target.gameObject.transform.position;
				if (myLine) {
					myLine.SetPositions (new Vector3[] {
						this.gameObject.transform.position,
						order.Target.gameObject.transform.position
					});
				}

			}
			GetComponent<Selected> ().RallyUnit = order.Target.gameObject;
			rallyPoint= Vector3.zero ;
			break;



		}





	}



}
