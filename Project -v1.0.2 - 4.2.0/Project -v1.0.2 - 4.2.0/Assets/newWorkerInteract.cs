using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class newWorkerInteract :  Ability, Iinteract {

	private UnitManager myManager;
	public float miningTime;

	public float resourceOne;
	public float resourceTwo;
	public GameObject Hook;
	private Vector3 hookPos;
	private bool retractHook;
	public OreDispenser myOre;
	private GameObject oreBlock;
	private OreDispenser lastOreDeposit;

	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		myManager.setInteractor (this);
		oreBlock = Hook.transform.Find ("Cube").gameObject;

		StartCoroutine (delayer());
		if (Hook) {
			hookPos = this.gameObject.transform.position - Hook.transform.position;

		}

		myType = type.activated;

	}



	public UnitState computeState(UnitState s)
	{
		
		if (autocast && myManager) {
	
			if ((myManager.getState () is ChannelState && s is MoveState) || (myManager.getState () is PlaceBuildingState && s is DefaultState)) {
				if (myManager.PlayerOwner == 1) { // This is a hack to make sure the SUperCraftor in the money level doesn't try to start mining from the start.
					StartCoroutine (autocastReturn ());
				}
			}

		}

		if (!(s is MiningState)) {
			if (myOre) {
				lastOreDeposit = myOre;

				myOre.currentMinor = null;
				myOre = null;
			}
		}

		return s;
	}

	IEnumerator autocastReturn()
	{
		yield return new WaitForSeconds (.3f);
		Activate ();
	}

	IEnumerator delayer()
	{
		yield return new WaitForSeconds (1.5f);
		//Debug.Log ("Finding Ore");
		if (myManager.getState () is DefaultState) {
			findNearestOre ();
		}
	}

	public void findNearestOre()
	{

		float distance = 100000;
		if (myManager.getState () is MiningState) {
			return;}
		OreDispenser closest = null;

		foreach (KeyValuePair<string, List<UnitManager>> pair in  GameManager.main.playerList[2].getUnitList()) {
			foreach (UnitManager obj in pair.Value) {

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

					closest = dis;
				}

			}
		}
		if (closest != null) {
			myOre = closest;
			myManager.changeState (new MiningState (closest, myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos));
		
		}
	}


	public void Redistribute(GameObject targ)
	{

		float distance = 130;
		if (myManager.getState () is MiningState) {
			return;}
		OreDispenser closest = null;

		foreach (KeyValuePair<string, List<UnitManager>> pair in  GameManager.main.playerList[2].getUnitList()) {
			foreach (UnitManager obj in pair.Value) {

				if (FogOfWar.current.IsInCompleteFog (obj.transform.position)) {
					continue;
				}
				OreDispenser dis = obj.GetComponent<OreDispenser> ();

				if (!dis || dis.currentMinor) {

					continue;
				}

				float temp = Vector3.Distance (obj.transform.position, targ.transform.position);
				if (temp < distance) {
					//Debug.Log ("Setting " + obj +  "   " + temp + "   " + distance);
					distance = temp;

					closest = dis;
			
				}
			}
		}
		if (closest != null) {
			myOre = closest;
		
			myManager.changeState (new MiningState (closest, myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos));
	
		} else {
			//myManager.changeState (new MoveState (targ.gameObject.transform.position, myManager));
		
			ErrorPrompt.instance.showError ("Deposits already occupied");
		}
	}





	// Update is called once per frame
	void Update () {
		if (retractHook) {

			Hook.transform.Translate (Vector3.up * 20 * Time.deltaTime, Space.Self);


			if (Hook.transform.position.y > this.gameObject.transform.position.y -hookPos.y ) {
				Hook.transform.position = this.gameObject.transform.position - hookPos;

				if (oreBlock.activeSelf) {
					oreBlock.SetActive (false);

					GetComponent<ResourceDropOff> ().dropOff (resourceOne, resourceTwo);

					PopUpMaker.CreateGlobalPopUp ("+" + +resourceOne, Color.white, myManager.gameObject.transform.position);

				}

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
				lastOreDeposit = myOre;
	
				myOre.currentMinor = null;
				myOre = null;

			}
			myManager.changeState (new DefaultState ());
			checkHook ();
			break;

			//Move Order ---------------------------------------------
		case Const.ORDER_MOVE_TO:
			
			myManager.changeState (new MoveState (order.OrderLocation, myManager),false,order.queued);
			if (myOre) {
				lastOreDeposit = myOre;
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
						lastOreDeposit = myOre;
	
						myOre.currentMinor = null;
						myOre = null;
					}
					myOre = order.Target.gameObject.GetComponent<OreDispenser> ();
					myManager.changeState (new MiningState (order.Target.gameObject.GetComponent<OreDispenser>(), myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos),false,order.queued);
					order.Target.gameObject.GetComponent<OreDispenser> ().currentMinor = this.gameObject;
				} else if (order.Target.gameObject.GetComponent<OreDispenser> ().currentMinor == this.gameObject) {
				}
				else{
					Redistribute (order.Target);
				}
				break;}

			checkHook ();

			if (order.Target) {
				if (order.Target.GetComponent<UnitManager> () == null) {
					order.Target = order.Target.transform.parent.gameObject;
				}
			}


			myManager.changeState (new FollowState (order.Target.gameObject, myManager),false,order.queued);


			break;


		case Const.ORDER_AttackMove:
			
			if (myOre) {
				lastOreDeposit = myOre;

				myOre.currentMinor = null;
				myOre = null;
			}
			if (myManager.myWeapon.Count >0)
				myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager,  myManager.gameObject.transform.position),false,order.queued);
			else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager),false,order.queued);
			}
			checkHook ();
			break;


		case Const.ORDER_Follow:
			
			if (order.Target.gameObject.GetComponent<OreDispenser> () != null) {
				myManager.changeState (new MiningState (order.Target.gameObject.GetComponent<OreDispenser>(), myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos),false,order.queued);
				break;
			}

			checkHook ();

			if (order.Target) {
				if (order.Target.GetComponent<UnitManager> () == null) {
					order.Target = order.Target.transform.parent.gameObject;
				}
			}
			if (order.Target.GetComponent<BuildingInteractor> ()){
			if (!order.Target.GetComponent<BuildingInteractor> ().ConstructDone()) {
					myManager.changeState (new buildResumeState (order.Target.gameObject),false,order.queued);
				}
			} 
			else {

				myManager.changeState (new FollowState (order.Target.gameObject, myManager),false,order.queued);
			}

			break;




		}





	}

	public void checkHook()
	{
		if (Hook.transform.position.y <this.gameObject.transform.position.y - hookPos.y ) {
			//oreBlock.SetActive (false);
			retractHook = true;

		}
	}


	public override void setAutoCast(bool offOn){
	}


	override
	public continueOrder canActivate (bool showError)
	{

		continueOrder order = new continueOrder ();
		order.nextUnitCast = true;
			order.canCast = true;
	
		return order;
	}

	override
	public void Activate()
	{
		if (lastOreDeposit) {
			
				if (lastOreDeposit.currentMinor == null) {
				myManager.changeState (new MiningState (lastOreDeposit.gameObject.GetComponent<OreDispenser>(), myManager, miningTime, resourceOne, resourceTwo, Hook, hookPos));
				} else {
					Redistribute (lastOreDeposit.gameObject);
				}


		} 
		checkHook ();
	}




}