using UnityEngine;
using System.Collections;

public class AugmentUpgrade : Upgrade {



	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		if (manager.UnitName == "Augmentor") {
			obj.GetComponent<Augmentor> ().SpeedPlus += .15f;
			manager.GetComponent<UnitStats> ().UnitDescription = "Enhance one of your structures.\nConstruction Yard -> 50% production increase.\nTurret Shop -> Enables Repair Pods\nAether Core -> Defensive Cannon\nOre Deposits -> 30% Mining Increase";
			obj.GetComponent<Augmentor> ().Descripton = "Enhance one of your structures.\nConstruction Yard -> 50% production increase.\nTurret Shop -> Enables Repair Pods\nAether Core -> Defensive Cannon\nOre Deposits -> 30% Mining Increase";
		}

	}

	public override void unApplyUpgrade (GameObject obj){

	}


}
