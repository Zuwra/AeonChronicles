using UnityEngine;
using System.Collections;

public class ExtendedBarrels :SpecificUpgrade{




	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){
		Debug.Log ("Trying to appl " + obj.name);
		if (confirmUnit (obj)) {
			Debug.Log (" Applying range to " + obj.name);
			foreach (IWeapon weap in obj.GetComponents<IWeapon>()) {
				weap.range *= 1.15f;
			}
		}

	}


	public override void unApplyUpgrade (GameObject obj)
	{
		if (confirmUnit (obj)) {
			foreach (IWeapon weap in obj.GetComponents<IWeapon>()) {
				weap.range /= 1.15f;
			}
		}

	}
}