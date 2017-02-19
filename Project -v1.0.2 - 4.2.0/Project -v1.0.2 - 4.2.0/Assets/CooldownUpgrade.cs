using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownUpgrade : Upgrade
{




	override
	public void applyUpgrade(GameObject obj)
	{

		UnitManager manager = obj.GetComponent<UnitManager>();

		if (manager.UnitName == "Vulcan")
		{
			manager.GetComponent<BloodMist> ().myCost.cooldown -= 10;
			manager.GetComponent<DeployTurret> ().ReplicationTime -= 10;;
			manager.GetComponent<DeployTurret> ().myCost.cooldown -= 10;
			manager.GetComponent<SingleTarget> ().myCost.cooldown -=10;
		}

	}
	public override void unApplyUpgrade (GameObject obj){

	}


}
