using UnityEngine;
using System.Collections;

public class DaexaWorkerInteract : MonoBehaviour , Iinteract {

	private UnitManager myManager;
	public float miningTime;

	public float resourceOne;
	public float resourceTwo;
	public GameObject Hook;
	private Vector3 hookPos;
	private bool retractHook;


	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		myManager.setInteractor (this);

		StartCoroutine (delayer());
		if (Hook) {
			hookPos = this.gameObject.transform.position - Hook.transform.position;
		
		}

	}

	IEnumerator delayer()
	{
		yield return new WaitForSeconds (1);
		findNearestOre ();
	}

	public void findNearestOre()
	{

		float distance = 100000;

		OreDispenser closest = null;
		foreach (GameObject obj in myManager.neutrals) {
			OreDispenser dis = obj.GetComponent<OreDispenser> ();
			if (!dis || dis.currentMinor) {
				continue;
			}
			float temp = Vector3.Distance (obj.transform.position, this.gameObject.transform.position);
			if (temp < distance) {
				distance = temp;
				closest = dis;
			}
		
		}
		if (closest != null) {
			
			myManager.changeState (new MiningState (closest, myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos));
		}
	}



	// Update is called once per frame
	void Update () {
		if (retractHook) {

			Hook.transform.Translate (Vector3.up * 20 * Time.deltaTime, Space.Self);


			if (Hook.transform.position.y > this.gameObject.transform.position.y -hookPos.y ) {
				Hook.transform.position = this.gameObject.transform.position - hookPos;
				retractHook = false;

			}


		}

	}
	public void initialize(){
		Start ();
	}


	public  void computeInteractions (Order order)
	{
		//Debug.Log ("interacting" + order.OrderType);
		switch (order.OrderType) {
		//Stop Order----------------------------------------
		case Const.ORDER_STOP:
			myManager.changeState (new DefaultState ());
			checkHook ();
			break;

		//Move Order ---------------------------------------------
		case Const.ORDER_MOVE_TO:
			myManager.changeState (new MoveState (order.OrderLocation, myManager));
			checkHook ();
			break;

		case Const.ORDER_Interact:
			
			if(order.Target.gameObject.GetComponent<OreDispenser> () != null)
			{myManager.changeState (new MiningState (order.Target.gameObject.GetComponent<OreDispenser>(), myManager, miningTime, resourceOne, resourceTwo, Hook,hookPos));
				break;}
			
			checkHook ();

			if (order.Target) {
				if (order.Target.GetComponent<UnitManager> () == null) {
					order.Target = order.Target.transform.parent.gameObject;
				}
			}


			if ((order.Target.gameObject.GetComponent<TurretMount> () && order.Target.gameObject.GetComponent<TurretMount> ().enabled == true) || order.Target.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Turret))
			{
				myManager.changeState (new AbilityFollowState (order.Target.gameObject, order.Target.gameObject.transform.position, GetComponent<TurretPickUp>() ));}
		
			else if (order.Target.gameObject.GetComponentInChildren<TurretMount> () && order.Target.gameObject.GetComponentInChildren<TurretMount> ().enabled==true)
				{GameObject obj = order.Target.gameObject.GetComponentInChildren<TurretMount> ().gameObject;
				myManager.changeState (new AbilityFollowState (obj, order.Target.gameObject.transform.position, GetComponent<TurretPickUp>() ));
			
			}
			else{
				
				myManager.changeState (new FollowState (order.Target.gameObject, myManager));
			}
		
			break;


		case Const.ORDER_AttackMove:
			if (myManager.myWeapon.Count >0)
				myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager,  myManager.gameObject.transform.position));
			else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager));
			}
			checkHook ();
			break;


		case Const.ORDER_Follow:
			
			if(order.Target.gameObject.GetComponent<OreDispenser> () != null)
			{myManager.changeState (new MiningState (order.Target.gameObject.GetComponent<OreDispenser>(), myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos));
				break;}

			checkHook ();

			if (order.Target) {
				if (order.Target.GetComponent<UnitManager> () == null) {
					order.Target = order.Target.transform.parent.gameObject;
				}
			}


			if ((order.Target.gameObject.GetComponent<TurretMount> () && order.Target.gameObject.GetComponent<TurretMount> ().enabled == true) || order.Target.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Turret))
			{
				myManager.changeState (new AbilityFollowState (order.Target.gameObject, order.Target.gameObject.transform.position, GetComponent<TurretPickUp>() ));}

			else if (order.Target.gameObject.GetComponentInChildren<TurretMount> () && order.Target.gameObject.GetComponentInChildren<TurretMount> ().enabled==true)
			{GameObject obj = order.Target.gameObject.GetComponentInChildren<TurretMount> ().gameObject;
				myManager.changeState (new AbilityFollowState (obj, order.Target.gameObject.transform.position, GetComponent<TurretPickUp>() ));

			}
			else{
		
				myManager.changeState (new FollowState (order.Target.gameObject, myManager));
			}

			break;
		



		}





	}
	public UnitState computeState(UnitState s)
	{

		return s;
	}


	public void checkHook()
	{
		if (Hook.transform.position.y <this.gameObject.transform.position.y - hookPos.y ) {
			
			retractHook = true;

		}
	}
}