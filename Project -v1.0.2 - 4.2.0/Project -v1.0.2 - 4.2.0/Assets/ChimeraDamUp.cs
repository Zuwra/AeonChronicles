using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraDamUp : Upgrade {

	public Sprite replacementImage;
	public float DamageIncrease;
	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		if (manager.UnitName == "Chimera") {
			foreach (ChangeAmmo ca in manager.GetComponents<ChangeAmmo>()) {
				if(ca.Name == "Ammo: Penetrating Shot [X]")
				{ca.iconPic = replacementImage;
					ca.attackDamage += DamageIncrease;
					if (ca.autocast) {
						ca.Activate ();
					}
				}
			}

		}

	}

	public override void unApplyUpgrade (GameObject obj){

	}


}
