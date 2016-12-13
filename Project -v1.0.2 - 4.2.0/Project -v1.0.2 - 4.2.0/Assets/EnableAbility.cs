using UnityEngine;
using System.Collections;

public class EnableAbility : Upgrade {

	public string unitName;
	public string AbilityName;


	override
	public void applyUpgrade (GameObject obj){

		UnitManager man = obj.GetComponent<UnitManager> ();
		if (man) {
			if (man.UnitName == unitName) {
				foreach (Ability ab in man.abilityList) {

					if (ab.Name == AbilityName) {
						ab.active = true;
					}
				}
			}
		}

	}


	public override void unApplyUpgrade (GameObject obj){

	}
}
