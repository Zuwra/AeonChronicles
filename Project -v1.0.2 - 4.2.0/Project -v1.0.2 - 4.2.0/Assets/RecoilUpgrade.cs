using UnityEngine;
using System.Collections;

public class RecoilUpgrade : Upgrade {

	override
	public void applyUpgrade(GameObject obj)
	{

		obj.GetComponent<DayexaShield> ().AbsorbRecoil = true;

	
	}

	public override void unApplyUpgrade (GameObject obj){
		obj.GetComponent<DayexaShield> ().AbsorbRecoil = false;
	}
}
