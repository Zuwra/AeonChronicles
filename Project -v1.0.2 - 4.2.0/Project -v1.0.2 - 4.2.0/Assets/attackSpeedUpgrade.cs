using UnityEngine;
using System.Collections;

public class attackSpeedUpgrade : Upgrade {


	public float speedPeriodDec;
	public string unitName;

	override
	public void applyUpgrade (GameObject obj){

		UnitManager manager = obj.GetComponent<UnitManager> ();

		if(manager){
			if (manager.UnitName == unitName ) {

				foreach (IWeapon weap in manager.myWeapon) {
					weap.changeAttackSpeed (0, speedPeriodDec, true, null);
				}
				foreach (ChangeAmmo ca in manager.GetComponents<ChangeAmmo>()) {
					ca.attackPeriod -= speedPeriodDec;
				}
			}
		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}
}
