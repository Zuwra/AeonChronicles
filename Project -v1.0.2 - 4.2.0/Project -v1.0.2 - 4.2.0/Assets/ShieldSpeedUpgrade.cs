using UnityEngine;
using System.Collections;

public class ShieldSpeedUpgrade : Upgrade {

	override
	public void applyUpgrade(GameObject obj)
	{
		ShieldSpeedBoost ssb = obj.GetComponent<ShieldSpeedBoost> ();

			ssb.enabled = true;
		obj.GetComponent<UnitManager> ().abilityList.Add (ssb);



	}

	public override void unApplyUpgrade (GameObject obj){
		
		ShieldSpeedBoost ssb = obj.GetComponent<ShieldSpeedBoost> ();
		ssb.enabled = false;
		obj.GetComponent<UnitManager> ().abilityList.Remove(ssb);

	}
}
