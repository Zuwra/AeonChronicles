using UnityEngine;
using System.Collections;

public class DoubleUpgrade:SpecificUpgrade{



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){
		if (confirmUnit (obj)) {
			if (obj.GetComponent<IWeapon> ()) {
				DoubleUpgradeApp dua = obj.GetComponent<DoubleUpgradeApp> ();
				if (!dua) {
					dua = obj.AddComponent<DoubleUpgradeApp> ();
				}
				dua.doubleIt = true;
			}
		}
	}


	public override void unApplyUpgrade (GameObject obj){
		if (confirmUnit (obj)) {
			if (obj.GetComponent<DoubleUpgradeApp> ()) {
			
				obj.GetComponent<DoubleUpgradeApp> ().doubleIt = false;
			}
		}

		//obj.GetComponent<SlowDebuff> ().enabled = false;

	}
}
