using UnityEngine;
using System.Collections;

public class AugmentUpgrade : Upgrade {



	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		if (manager.UnitName == "Augmentor") {
			obj.GetComponent<Augmentor> ().SpeedPlus += .15f;
		}

	}

	public override void unApplyUpgrade (GameObject obj){

	}


}
