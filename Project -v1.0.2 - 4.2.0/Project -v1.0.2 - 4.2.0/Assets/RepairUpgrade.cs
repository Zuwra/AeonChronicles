using UnityEngine;
using System.Collections;

public class RepairUpgrade : Upgrade {

    public int repairAmount = 20;

    override
    public void applyUpgrade(GameObject obj)
    {

		RepairTurret turret = obj.GetComponent<RepairTurret> ();
		if(turret){
			turret.repairRate = repairAmount;
			turret.drone.GetComponent<RepairDrone> ().repairRate = repairAmount;
           }
        
    }

	public override void unApplyUpgrade (GameObject obj){

	}
}
