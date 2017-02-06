using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUpgrade : SpecificUpgrade {


	public GameObject SuperPenetrator;
	override
	public void applyUpgrade(GameObject obj)
	{

		if (confirmUnit (obj)) {

			UnitManager manager = obj.GetComponent<UnitManager> ();
			if (!manager) {
				return;
			}
			if (manager.UnitName == "Triton") {
				manager.GetComponent<DayexaShield> ().maxDamagePerSec = 10;
			} else if (manager.UnitName == "Vulcan") {
				foreach (BloodMist bm in obj.GetComponents<BloodMist>()) {
					bm.chargeCount = bm.maxChargeCount;
				}
				obj.GetComponent<DeployTurret> ().chargeCount = 2;
			} else if (manager.UnitName == "Chimera") {
			
				foreach (ChangeAmmo ca in manager.GetComponents<ChangeAmmo>()) {
					if (ca.Name == "Ammo: Pentrating Shot [X]") {
						ca.myAmmo = SuperPenetrator;
					}
				
				}
			

			}	

		}

	}

	public override void unApplyUpgrade (GameObject obj){
		
	}
}
