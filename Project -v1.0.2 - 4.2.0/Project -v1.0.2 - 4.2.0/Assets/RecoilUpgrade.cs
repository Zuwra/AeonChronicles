using UnityEngine;
using System.Collections;

public class RecoilUpgrade : SpecificUpgrade {

	override
	public void applyUpgrade(GameObject obj)
	{
		if (confirmUnit (obj)) {
			obj.GetComponent<DayexaShield> ().AbsorbRecoil = true;
		}
	
	}

	public override void unApplyUpgrade (GameObject obj){
		if (confirmUnit (obj)) {
			obj.GetComponent<DayexaShield> ().AbsorbRecoil = false;
		}
	}
}
