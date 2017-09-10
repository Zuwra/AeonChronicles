using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CampShieldUpgrade :SpecificUpgrade{



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

	
		if (confirmUnit (obj)) {
			//Debug.Log ("Checking " + obj);
			UnitStats us = obj.GetComponent<UnitStats> ();
			us.MaxEnergy *= 1.5f;
			us.currentEnergy *= 1.5f;
		}
	}


	public override void unApplyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
			UnitStats us = obj.GetComponent<UnitStats> ();
			us.MaxEnergy /= 1.5f;
			us.currentEnergy /= 1.5f;
		}
	}

	public override float ChangeString (string name, float number)
	{
		if ("Energy" == name) {
			return number * 1.5f;
		}

		return number;
	}

}
