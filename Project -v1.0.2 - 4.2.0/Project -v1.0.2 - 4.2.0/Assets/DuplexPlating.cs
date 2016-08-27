using UnityEngine;
using System.Collections;

public class DuplexPlating  :Upgrade{


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){


		UnitStats us = obj.GetComponent<UnitStats>();
		if (us.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			us.armor += 4;
			us.Maxhealth *= 1.2f;
			us.health *= 1.2f;
		}
	}


	public override void unApplyUpgrade (GameObject obj){


		UnitStats us = obj.GetComponent<UnitStats>();
		if (us.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			us.armor -= 4;
			us.Maxhealth /= 1.2f;
			us.health /= 1.2f;
		}
	}


}
