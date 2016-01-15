using UnityEngine;
using System.Collections;

public class DamageUpgrade : Upgrade {


	public float damageAmount;

	override
	public void applyUpgrade (GameObject obj){

		UnitManager manager = obj.GetComponent<UnitManager> ();

		if(manager){
			if (manager.UnitName == "Coyote" || manager.UnitName == "Swallow")
			if (obj.GetComponent<IWeapon> ()) {
				obj.GetComponent<IWeapon> ().baseDamage += damageAmount;
			}

		}
	}



}
