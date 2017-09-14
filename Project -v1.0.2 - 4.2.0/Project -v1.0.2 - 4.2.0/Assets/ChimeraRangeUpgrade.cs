using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Actually now just switches out the prefab for the ammo used.
public class ChimeraRangeUpgrade : Upgrade {

	public Sprite replacementImage;
	public float RangeIncrease;
	public GameObject newAmmo;
	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		if (manager.UnitName == "Chimera") {
			foreach (ChangeAmmo ca in manager.GetComponents<ChangeAmmo>()) {
				if(ca.Name == "Ammo: Siege Rounds [Z]")
				{ca.iconPic = replacementImage;
					ca.myAmmo = newAmmo;
					ca.resetBulletPool ();
					//ca.range += RangeIncrease;
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
