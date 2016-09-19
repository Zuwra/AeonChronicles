using UnityEngine;
using System.Collections;

public class HealthUpgrade : Upgrade {


	public float healthAmount;
	public string unitName;

	override
	public void applyUpgrade (GameObject obj){

		UnitManager manager = obj.GetComponent<UnitManager> ();

		if(manager){
			if (manager.UnitName == unitName ) {

				obj.GetComponent<UnitStats> ().Maxhealth += healthAmount;

				obj.GetComponent<UnitStats> ().health += healthAmount;
			}
		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}
}
