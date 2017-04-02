using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraRangeUpgrade : Upgrade {

	public Sprite replacementImage;
	public float RangeIncrease;
	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		if (manager.UnitName == "Chimera") {
			foreach (ChangeAmmo ca in manager.GetComponents<ChangeAmmo>()) {
				if(ca.Name == "Ammo: Siege Rounds [Z]")
				{ca.iconPic = replacementImage;
					ca.range += RangeIncrease;
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
