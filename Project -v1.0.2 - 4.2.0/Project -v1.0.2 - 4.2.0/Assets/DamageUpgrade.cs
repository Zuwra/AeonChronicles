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

		IWeapon wep = obj.GetComponent<IWeapon> ();
		if(wep){
			if (obj.GetComponent<GatlingGun>() != null)
            {
                obj.GetComponent<IWeapon>().baseDamage += gatlingDamage;
            }
		    else if (manager.UnitName.Equals("Imperio Cannon"))
            {
                obj.GetComponent<IWeapon>().baseDamage += railgunDamage;
            }
            else if (obj.GetComponent<mortarPod>() != null)
            {
                obj.GetComponent<IWeapon>().baseDamage += mortarDamage;
            }
            else if (manager.UnitName.Equals("Hornet"))
            {
                obj.GetComponent<IWeapon>().baseDamage += hornetDamage;
            }

		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
