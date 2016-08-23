using UnityEngine;
using System.Collections;

public class FusionWeld  : Upgrade {


	public int capacityIncrease;

	override
	public void applyUpgrade (GameObject obj){


		RepairTurret wep = obj.GetComponent<RepairTurret> ();
		if(wep){


			//obj.GetComponent<RepairTurret> ().maxRepair += capacityIncrease;
			obj.GetComponent<RepairTurret> ().repairRate *=2;
			obj.GetComponent<RepairTurret> ().drone.GetComponent<RepairDrone> ().repairRate *= 2;


		}
	}


	public override void unApplyUpgrade (GameObject obj){

	}
}
