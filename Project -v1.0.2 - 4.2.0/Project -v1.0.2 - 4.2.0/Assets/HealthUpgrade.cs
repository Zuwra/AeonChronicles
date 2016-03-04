using UnityEngine;
using System.Collections;

public class HealthUpgrade : Upgrade {


	public float healthAmount;

	override
	public void applyUpgrade (GameObject obj){

		UnitManager manager = obj.GetComponent<UnitManager> ();

		if(manager){
			if (manager.UnitName == "Coyote" ) {

				obj.GetComponent<UnitStats> ().Maxhealth += healthAmount;

				obj.GetComponent<UnitStats> ().health += healthAmount;
			}
		}
	}
}
