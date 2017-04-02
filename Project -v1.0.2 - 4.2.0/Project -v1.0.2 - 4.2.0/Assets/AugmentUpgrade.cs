using UnityEngine;
using System.Collections;

public class AugmentUpgrade : Upgrade {



	override
	public void applyUpgrade (GameObject obj){


		UnitManager manager = obj.GetComponent<UnitManager>();
		if (manager.UnitName == "Augmentor") {
	
			obj.GetComponent<Augmentor>().changeSpeed(.35f);
			manager.GetComponent<UnitStats> ().UnitDescription = "Enhance one of your structures.\n Production & Research structures -> 75% production increase, unlocks options.\nAether Core -> Defensive Cannon\nOre Deposits -> 35% Mining Increase";
			obj.GetComponent<Augmentor> ().Descripton = "Enhance one of your structures.\n Production & Research structures -> 75% production increase, unlocks options.\nAether Core -> Defensive Cannon\nOre Deposits -> 35% Mining Increase";
		}

	}

	public override void unApplyUpgrade (GameObject obj){

	}


}
