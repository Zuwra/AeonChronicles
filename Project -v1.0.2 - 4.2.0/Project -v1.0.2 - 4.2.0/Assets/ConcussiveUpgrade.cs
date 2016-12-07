using UnityEngine;
using System.Collections;

public class ConcussiveUpgrade :Upgrade{


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

		//obj.GetComponent<SlowDebuff> ().enabled = true;
		if (obj.GetComponent<IWeapon> ()) {
			IWeapon.bonusDamage bd = new IWeapon.bonusDamage ();
			bd.bonus = 10;
			bd.type = UnitTypes.UnitTypeTag.Structure;
			obj.GetComponent<IWeapon> ().extraDamage.Add (bd);
		}
	}


	public override void unApplyUpgrade (GameObject obj){
		IWeapon.bonusDamage toRemove = new IWeapon.bonusDamage();
		foreach (IWeapon.bonusDamage bd in GetComponent<IWeapon>().extraDamage) {
			if (bd.type == UnitTypes.UnitTypeTag.Structure && bd.bonus == 10) {
				toRemove = bd;
			}
		}

		if (toRemove.bonus != 0) {
			GetComponent<IWeapon> ().extraDamage.Remove (toRemove);
		}
		//obj.GetComponent<SlowDebuff> ().enabled = false;

	}
}
