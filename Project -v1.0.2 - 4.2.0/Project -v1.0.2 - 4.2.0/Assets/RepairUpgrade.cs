using UnityEngine;
using System.Collections;

public class RepairUpgrade : Upgrade {

    public int repairAmount = 20;

    override
    public void applyUpgrade(GameObject obj)
    {

        UnitManager manager = obj.GetComponent<UnitManager>();

        if (manager)
        {
			if (manager.UnitName .Contains("Repair"))
            {
                obj.GetComponent<RepairTurret>().repairRate = repairAmount;
            }
        }
    }

	public override void unApplyUpgrade (GameObject obj){

	}
}
