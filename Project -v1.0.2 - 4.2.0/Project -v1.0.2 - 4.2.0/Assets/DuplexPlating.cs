using UnityEngine;
using System.Collections;

public class DuplexPlating  :SpecificUpgrade{




	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
			UnitStats us = obj.GetComponent<UnitStats> ();
			if (us.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				us.armor += 4;
				us.Maxhealth *= 1.2f;
				us.health *= 1.2f;
			}
		}
	}


	public override void unApplyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
			UnitStats us = obj.GetComponent<UnitStats> ();
			if (us.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				us.armor -= 4;
				us.Maxhealth /= 1.2f;
				us.health /= 1.2f;
			}
		}
	}

	public override float ChangeString (string name, float number)
	{
		if ("Health" == name) {
			number *= 1.2f;
		}

		if ("Armor" == name) {
			number += 4;
		}

		return number;
	}

}
