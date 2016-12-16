using UnityEngine;
using System.Collections;

public class ArmoryInteractor: BuildingInteractor {

	public GameObject RemoveEffect;

	public override void computeInteractions (Order order)
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


			rallyPoint= Vector3.zero ;
			break;

		case Const.ORDER_Follow:
	
			AttackMoveSpawn = false;
			if( Vector3.Distance(this.gameObject.transform.position, order.Target.transform.position) < 45)
			{
				foreach (TurretMount tm in order.Target.GetComponentsInChildren<TurretMount>()) {
					if (tm.turret) {
						Instantiate( RemoveEffect, tm.transform.position + Vector3.up, tm.transform.rotation, tm.transform);
						GameObject turret = tm.unPlaceTurret ();

						if (turret) {
							UnitManager um = turret.GetComponent<UnitManager> ();


							foreach (buildTurret bt in GetComponents<buildTurret>()) {
								
								if (bt.Name.Contains (um.UnitName)) {
									bt.changeCharge (1);
									um.myStats.kill (null);
									GameManager.main.activePlayer.unitsLost--;
									break;
								}
							}

						
						}
					}
				}
			}
			rallyPoint= Vector3.zero ;
			break;



		}





	}

}


	

