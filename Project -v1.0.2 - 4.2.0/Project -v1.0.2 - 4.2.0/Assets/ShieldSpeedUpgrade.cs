using UnityEngine;
using System.Collections;

public class ShieldSpeedUpgrade : SpecificUpgrade {

	override
	public void applyUpgrade(GameObject obj)
	{if (confirmUnit (obj)) {
			ShieldSpeedBoost ssb = obj.GetComponent<ShieldSpeedBoost> ();
			if (ssb) {
				ssb.enabled = true;
				//obj.GetComponent<UnitManager> ().abilityList.Add (ssb);
			
			}
		}

	}

	public override void unApplyUpgrade (GameObject obj){
		if (confirmUnit (obj)) {
			ShieldSpeedBoost ssb = obj.GetComponent<ShieldSpeedBoost> ();
			ssb.enabled = false;
		//	obj.GetComponent<UnitManager> ().abilityList.Remove (ssb);
		}
	}
}
