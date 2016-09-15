using UnityEngine;
using System.Collections;

public class DamageUpgrade : Upgrade {


    public float defaultAmount = 1;
    public float gatlingDamage = 1;
    public float railgunDamage = 3;
    public float mortarDamage = 3;
    public float hornetDamage = 2;

	override
	public void applyUpgrade (GameObject obj){

        UnitManager manager = obj.GetComponent<UnitManager>();
		//Debug.Log ("Checking " + obj);
		foreach(IWeapon wep in obj.GetComponents<IWeapon>())
		if(wep){
			if (obj.GetComponent<GatlingGun>() != null)
            {
                wep.baseDamage += gatlingDamage;
            }
				else if (manager.UnitName.Contains("Imperio"))
            {
					wep.baseDamage += railgunDamage;
            }
            else if (obj.GetComponent<mortarPod>() != null)
            {
					wep.baseDamage += mortarDamage;
            }
            else if (manager.UnitName.Equals("Harpy"))
				{//Debug.Log ("Weapon is " + wep.Title);
					wep.baseDamage += hornetDamage;
					if (wep.baseDamage > 40) {
						wep.baseDamage += 8;
					}
            }

		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
