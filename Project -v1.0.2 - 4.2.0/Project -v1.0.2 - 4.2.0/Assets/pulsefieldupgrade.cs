using UnityEngine;
using System.Collections;

public class pulsefieldupgrade : Upgrade {

	public float rechargeIncrease;

	override
	public void applyUpgrade (GameObject obj){

		AetherRelay manager = obj.GetComponent<AetherRelay>();
		//Debug.Log ("Checking " + obj);
		if (manager) {

			manager.energyChargeRate += rechargeIncrease;
			//Debug.Log ("Recharge rate is " + manager.energyChargeRate);
			manager.Descripton = "Passively Regenerates allied units energy (" + manager.energyChargeRate + " per second).\n\nActivate to instead discharge energy (20 per second) to damage nearby enemy units.";
		
		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
