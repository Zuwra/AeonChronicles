using UnityEngine;
using System.Collections;

public class ShieldUpgrade  : Upgrade {


	override
	public void applyUpgrade (GameObject obj){

		DayexaShield manager = obj.GetComponent<DayexaShield>();
		//Debug.Log ("Checking " + obj);
		if (manager) {
			manager.rechargeRate += 2;
			manager.changeAbsorption (2);

		}
	}

	public override void unApplyUpgrade (GameObject obj){
		DayexaShield manager = obj.GetComponent<DayexaShield>();

		if (manager) {
			manager.rechargeRate -= 2;

		
		}
	}

}
