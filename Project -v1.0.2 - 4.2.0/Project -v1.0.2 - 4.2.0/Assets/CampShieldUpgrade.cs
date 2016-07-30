using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CampShieldUpgrade :Upgrade{


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
			us.MaxEnergy *=1.5f;
			us.currentEnergy *=1.5f;

	}


	public override void unApplyUpgrade (GameObject obj){


		UnitStats us = obj.GetComponent<UnitStats>();
		us.MaxEnergy /=1.5f;
		us.currentEnergy /=1.5f;

	}


}
