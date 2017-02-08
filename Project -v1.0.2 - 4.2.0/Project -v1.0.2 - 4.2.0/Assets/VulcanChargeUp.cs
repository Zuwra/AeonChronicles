using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanChargeUp  : Upgrade
{




	override
	public void applyUpgrade(GameObject obj)
	{

		UnitManager manager = obj.GetComponent<UnitManager>();

		if (manager.UnitName == "Vulcan")
		{
			manager.GetComponent<BloodMist> ().UpMaxCharge ();
			manager.GetComponent<DeployTurret> ().UpMaxCharge ();
			manager.GetComponent<SingleTarget> ().UpMaxCharge ();
		}
	}
	public override void unApplyUpgrade (GameObject obj){

	}


}
