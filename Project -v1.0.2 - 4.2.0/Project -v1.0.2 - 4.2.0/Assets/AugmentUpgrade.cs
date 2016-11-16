using UnityEngine;
using System.Collections;

public class AugmentUpgrade : Upgrade {



	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		if (manager.UnitName == "Augmentor") {
	
			obj.GetComponent<Augmentor>().changeSpeed(.15f);
			manager.GetComponent<UnitStats> ().UnitDescription = "Enhance one of your structures.\n Production & Research structures -> 50% production increase, unlocks options.\nAether Core -> Defensive Cannon\nOre Deposits -> 30% Mining Increase";
			obj.GetComponent<Augmentor> ().Descripton = "Enhance one of your structures.\n Production & Research structures -> 50% production increase, unlocks options.\nAether Core -> Defensive Cannon\nOre Deposits -> 30% Mining Increase";
		}

	}

	public override void unApplyUpgrade (GameObject obj){

	}


}
