using UnityEngine;
using System.Collections;

public class CoyoteMoveFireUpgrade : SpecificUpgrade{

    override
    public void applyUpgrade(GameObject obj)
	{
		if (confirmUnit (obj)) {
			UnitManager manager = obj.GetComponent<UnitManager> ();

			if (manager) {
				if (manager.UnitName == "Manticore") {
					obj.GetComponent<StandardInteract> ().attackWhileMoving = true;
				} else if (manager.UnitName == "Zephyr") {
				
					foreach (AngleWeapon angle in manager.GetComponents<AngleWeapon>()) {
						angle.angleOfAttack = 45;

					}
				}
			}
		}
	}

	public override void unApplyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
			UnitManager manager = obj.GetComponent<UnitManager> ();

			if (manager) {
				if (manager.UnitName == "Manticore") {
					obj.GetComponent<StandardInteract> ().attackWhileMoving = false;
				}
			}
		}
	}


	public override float ChangeString (string name, float number)
	{
		return number;
	}
}
