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
		
		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
