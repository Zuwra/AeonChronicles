using UnityEngine;
using System.Collections;

public class ExtendedBarrels :Upgrade{


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

		foreach (IWeapon weap in obj.GetComponents<IWeapon>()) {
			weap.range *= 1.15f;
		}


	}


	public override void unApplyUpgrade (GameObject obj)
	{

		foreach (IWeapon weap in obj.GetComponents<IWeapon>()) {
			weap.range /= 1.15f;
		}


	}
}