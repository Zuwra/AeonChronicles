using UnityEngine;
using System.Collections;

public class TurboRockets  : Upgrade {


	public float speedAmount;

	override
	public void applyUpgrade (GameObject obj){


		if(obj.GetComponent<UnitManager>().UnitName.Equals("Hornet")){
	
			obj.GetComponent<UnitManager> ().cMover.changeSpeed (0, speedAmount, true, this);



		}
	}
	public override void unApplyUpgrade (GameObject obj){

	}


}
