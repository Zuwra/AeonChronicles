using UnityEngine;
using System.Collections;

public class ExtendedBarrels :SpecificUpgrade{




	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){
		
		if (confirmUnit (obj)) {
	
			foreach (IWeapon weap in obj.GetComponents<IWeapon>()) {
				weap.range *= 1.2f;
			}
		}

	}


	public override void unApplyUpgrade (GameObject obj)
	{
		if (confirmUnit (obj)) {
			foreach (IWeapon weap in obj.GetComponents<IWeapon>()) {
				weap.range /= 1.2f;
			}
		}

	}

	public override float ChangeString (string name, float number)
	{
		if ("Range" == name) {
			number *= 1.2f;
		}

	
		return number;
	}


}