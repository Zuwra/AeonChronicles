using UnityEngine;
using System.Collections;

public class incendiaryUpgrade:SpecificUpgrade{

	public GameObject incendProj;
	public GameObject gatProj;
	public GameObject RailProj;


	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
			if (obj.GetComponent<Projectile> ()) {
				obj.GetComponent<IWeapon> ().projectile = incendProj;
			}
		}
	}


	public override void unApplyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
		if (obj.GetComponent<Projectile> ()) {

			UnitManager manage = obj.GetComponent<UnitManager> ();
			if (manage.UnitName == "Imperio Cannon") {
				manage.GetComponent<IWeapon> ().projectile = RailProj;
			} else if (manage.UnitName == "Minigun") {
				manage.GetComponent<IWeapon> ().projectile = gatProj;
			}
		}

		//obj.GetComponent<SlowDebuff> ().enabled = false;
		}
	}

	public override float ChangeString (string name, float number)
	{

		return number;
	}
}
