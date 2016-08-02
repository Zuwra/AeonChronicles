using UnityEngine;
using System.Collections;

public class newWorkerInteract : MonoBehaviour , Iinteract {

	private UnitManager myManager;
	public float miningTime;

	public float resourceOne;
	public float resourceTwo;
	public GameObject Hook;
	private Vector3 hookPos;
	private bool retractHook;
	private OreDispenser myOre;

	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		myManager.setInteractor (this);


		StartCoroutine (delayer());
		if (Hook) {
			hookPos = this.gameObject.transform.position - Hook.transform.position;

		}

	

	}
	public UnitState computeState(UnitState s)
	{

		return s;
	}

	IEnumerator delayer()
	{
		yield return new WaitForSeconds (1);
		//Debug.Log ("Finding Ore");
		findNearestOre ();
	}

	public void findNearestOre()
	{

		float distance = 100000;
		if (myManager.getState () is MiningState) {
			return;}
		GameObject closest = null;

		foreach (GameObject obj in GameManager.main.playerList[2].getUnitList()) {
			if (FogOfWar.current.IsInCompleteFog (obj.transform.position)) {
				continue;
			}
			OreDispenser dis = obj.GetComponent<OreDispenser> ();

			if (!dis || dis.currentMinor) {
				
				continue;
			}

			float temp = Vector3.Distance (obj.transform.position, this.gameObject.transform.position);
			if (temp < distance) {
				//Debug.Log ("Setting " + obj +  "   " + temp + "   " + distance);
				distance = temp;

				closest = obj;
				distance = temp;
				//Debug.Log ("Setting " + obj +  "   " + temp + "   " + distance);
			}

		}
		if (closest != null) {
			myOre = closest.GetComponent<OreDispenser> ();
			myOre.currentMinor = this.gameObject;
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
			if (myOre) {
				myOre.currentMinor = null;
				myOre = null;
			}
			myManager.changeState (new DefaultState ());
			checkHook ();
			break;

			//Move Order ---------------------------------------------
		case Const.ORDER_MOVE_TO:
			
			myManager.changeState (new MoveState (order.OrderLocation, myManager));
			if (myOre) {
				myOre.currentMinor = null;
				myOre = null;
			}
			checkHook ();
			break;

		case Const.ORDER_Interact:
			
			if(order.Target.gameObject.GetComponent<OreDispenser> () != null)
			{
				if (!order.Target.gameObject.GetComponent<OreDispenser> ().currentMinor) {
					if (myOre) {
						myOre.currentMinor = null;
						myOre = null;
					}
					myOre = order.Target.gameObject.GetComponent<OreDispenser> ();
					myManager.changeState (new MiningState (order.Target.gameObject, myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos));
					order.Target.gameObject.GetComponent<OreDispenser> ().currentMinor = this.gameObject;
				} else if (order.Target.gameObject.GetComponent<OreDispenser> ().currentMinor == this.gameObject) {
				}
				else{
					
					myManager.changeState (new MoveState (order.Target.gameObject.transform.position, myManager));
					ErrorPrompt.instance.showError ("Deposit already occupied");
				}
				break;}

			checkHook ();

			if (order.Target) {
				if (order.Target.GetComponent<UnitManager> () == null) {
					order.Target = order.Target.transform.parent.gameObject;
				}
			}


				myManager.changeState (new FollowState (order.Target.gameObject, myManager));


			break;


		case Const.ORDER_AttackMove:
			
			if (myOre) {
				myOre.currentMinor = null;
				myOre = null;
			}
			if (myManager.myWeapon.Count >0)
				myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager,  myManager.gameObject.transform.position));
			else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager));
			}
			checkHook ();
			break;


		case Const.ORDER_Follow:
			
			if(order.Target.gameObject.GetComponent<OreDispenser> () != null)
			{myManager.changeState (new MiningState (order.Target.gameObject, myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos));
				break;}

			checkHook ();

			if (order.Target) {
				if (order.Target.GetComponent<UnitManager> () == null) {
					order.Target = order.Target.transform.parent.gameObject;
				}
			}




			myManager.changeState (new FollowState (order.Target.gameObject, myManager));


			break;




		}





	}

	public void checkHook()
	{
		if (Hook.transform.position.y <this.gameObject.transform.position.y - hookPos.y ) {

			retractHook = true;

		}
	}
}