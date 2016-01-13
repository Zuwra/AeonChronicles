﻿using UnityEngine;
using System.Collections;

public class DaexaWorkerInteract : MonoBehaviour , Iinteract {

	private UnitManager myManager;
	public float miningTime;

	public float resourceOne;
	public float resourceTwo;


	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		myManager.setInteractor (this);

		StartCoroutine (delayer());
	

	}

	IEnumerator delayer()
	{
		yield return new WaitForSeconds (1);
		findNearestOre ();
	}

	public void findNearestOre()
	{

		float distance = 100000;

		GameObject closest = null;
		foreach (GameObject obj in myManager.neutrals) {

			float temp = Vector3.Distance (obj.transform.position, this.gameObject.transform.position);
			if (temp < distance) {
				distance = temp;
				closest = obj;
			}
		
		}
		if (closest != null) {
			
			myManager.changeState (new MiningState (closest, myManager, myManager.cMover, myManager.myWeapon, miningTime, resourceOne, resourceTwo));
		}
	}



	// Update is called once per frame
	void Update () {

	}
	public void initialize(){
		Start ();
	}


	public new  void computeInteractions (Order order)
	{

		switch (order.OrderType) {
		//Stop Order----------------------------------------
		case Const.ORDER_STOP:
			myManager.changeState (new DefaultState (myManager, myManager.cMover, myManager.myWeapon));
			break;

		//Move Order ---------------------------------------------
		case Const.ORDER_MOVE_TO:
			myManager.changeState (new MoveState (order.OrderLocation, myManager, myManager.cMover, myManager.myWeapon));

			break;

		case Const.ORDER_ATTACK:
			myManager.changeState (new InteractState (order.Target.gameObject, myManager, myManager.cMover, myManager.myWeapon));

			break;


		case Const.ORDER_AttackMove:
			if (myManager.myWeapon)
				myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.cMover, myManager.myWeapon, myManager.gameObject.transform.position));
			else {
				myManager.changeState (new MoveState (order.OrderLocation, myManager, myManager.cMover, myManager.myWeapon));
			}
			break;


		case Const.ORDER_Follow:
			

			if (order.Target.gameObject.GetComponent<OreDispenser> () != null) {
				myManager.changeState (new MiningState (order.Target.gameObject, myManager, myManager.cMover, myManager.myWeapon, miningTime, resourceOne, resourceTwo));
			} else {

				myManager.changeState (new FollowState (order.Target.gameObject, myManager, myManager.cMover, myManager.myWeapon));
			}
			break;



		}





	}
}