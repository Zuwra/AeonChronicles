using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeedUpgrade  : Upgrade {

	public List<unitAmount> unitsToUpgrade ;

	[System.Serializable]
	public struct unitAmount
	{
		public string UnitName;
		public float flatamount;
		public float percAmount;
	}


	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		foreach (unitAmount ua in unitsToUpgrade) {
			if (manager.UnitName.Contains(ua.UnitName)) {
				
				manager.cMover.changeSpeed(ua.percAmount,ua.flatamount,true,null);

			

			}
		}


	}

	public override void unApplyUpgrade (GameObject obj){

	}


}
