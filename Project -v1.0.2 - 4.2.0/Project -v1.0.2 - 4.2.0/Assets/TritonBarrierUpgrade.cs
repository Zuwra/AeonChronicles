using UnityEngine;
using System.Collections;

public class TritonBarrierUpgrade  : Upgrade {


	override
	public void applyUpgrade (GameObject obj){

		UnitManager manager = obj.GetComponent<UnitManager> ();

		if (manager && manager.UnitName.Contains("Triton")) {
			DayexaShield ds = obj.GetComponent<DayexaShield> ();
			ds.maxDamagePerSec = 10;
	

		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
