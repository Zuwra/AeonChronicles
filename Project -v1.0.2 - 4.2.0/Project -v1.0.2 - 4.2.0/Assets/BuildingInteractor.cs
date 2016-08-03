using UnityEngine;
using System.Collections;

public class BuildingInteractor : MonoBehaviour, Iinteract {

	private UnitManager myManager;

	public Vector3 rallyPoint = Vector3.zero;
	public GameObject rallyUnit;
	public bool AttackMoveSpawn;
	public GameObject RallyPointObj;
	public LineRenderer myLine;
	private float underConstruction;
	private bool doneConstruction;

	private GameObject sourceObj;
	public Animator myAnim;

	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		myManager.setInteractor (this);

		if (Clock.main.getTotalSecond () < 3) {
			doneConstruction = true;
			underConstruction = 1;
			if (myAnim) {
				myAnim.SetInteger ("State", 1);
			}
		}

	}

	// Update is called once per frame
	void Update () {

	}
	public void initialize(){
		Start ();
	}


	public void startConstruction(GameObject obj)
	{sourceObj = obj;
		doneConstruction = false;


		foreach (Ability ab in  GetComponent<UnitManager>().abilityList) {
			ab.active = false;
			//ab.enabled = false;
		}
	}

	public bool ConstructDone()
	{return doneConstruction;
	}
	public float getProgess()
	{
		return underConstruction;
	}

	public float construct(float m)
	{
		if (doneConstruction) {
			return 1;}
		
		underConstruction += m;
		myManager.myStats.heal (myManager.myStats.Maxhealth * m);
		if (underConstruction >= 1) {
			doneConstruction = true;

			UnitManager template = sourceObj.GetComponent<UnitManager> ();
			for (int i = 0; i < myManager.abilityList.Count; i++) {

				if (template.abilityList [i].active) {
					myManager.abilityList [i].active = true;
				}
				if (template.abilityList [i].enabled) {

					myManager.abilityList [i].enabled = true;
				}
			}

			return 1;
		}
		return underConstruction;
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

	public UnitState computeState(UnitState s)
	{
		
		return s;
	}


}
