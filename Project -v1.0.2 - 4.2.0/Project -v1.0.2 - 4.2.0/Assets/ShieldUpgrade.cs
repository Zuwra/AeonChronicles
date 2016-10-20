using UnityEngine;
using System.Collections;

public class ShieldUpgrade  : Upgrade {


	override
	public void applyUpgrade (GameObject obj){

		DayexaShield manager = obj.GetComponent<DayexaShield>();
		//Debug.Log ("Checking " + obj);
		if (manager) {
			manager.changeAbsorption (2);

			manager.rechargeRate += 1;
		}
	}

	public override void unApplyUpgrade (GameObject obj){
		DayexaShield manager = obj.GetComponent<DayexaShield>();

		if (manager) {
			manager.changeAbsorption (-2);
	
			manager.rechargeRate -= 1;
		
		}
	}

}
