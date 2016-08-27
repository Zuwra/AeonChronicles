using UnityEngine;
using System.Collections;

public class incendiaryUpgrade:Upgrade{

	public GameObject incendProj;
	public GameObject gatProj;
	public GameObject RailProj;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

		obj.GetComponent<IWeapon> ().projectile = incendProj;

	}


	public override void unApplyUpgrade (GameObject obj){

		UnitManager manage = obj.GetComponent<UnitManager> ();
		if (manage.UnitName == "Imperio Cannon") {
			manage.GetComponent<IWeapon> ().projectile = RailProj;
		}
		else if (manage.UnitName == "Minigun") {
			manage.GetComponent<IWeapon> ().projectile =gatProj;
		}


		//obj.GetComponent<SlowDebuff> ().enabled = false;

	}
}
