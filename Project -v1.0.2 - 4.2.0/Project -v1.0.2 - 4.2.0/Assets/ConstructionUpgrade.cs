using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructionUpgrade  :SpecificUpgrade{

	public List<GameObject> steelCraftors = new List<GameObject>();



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
			UnitManager us = obj.GetComponent<UnitManager> ();
			if (us.UnitName.Contains ("Yard")) {
				foreach (GameObject steel in steelCraftors) {
					UnitManager man = steel.GetComponent<UnitManager> ();
					foreach (Ability ab in man.abilityList) {
						if (ab.Name.Contains ("Yard")) {
							((BuildStructure)ab).buildTime *= .75f;
							ab.myCost.ResourceOne *= .75f;
						}
				
					}

			
				}
			}
			if (us.UnitName.Contains ("Core")) {
				foreach (GameObject steel in steelCraftors) {
					UnitManager man = steel.GetComponent<UnitManager> ();
					foreach (Ability ab in man.abilityList) {
						if (ab.Name.Contains ("Core")) {
							((BuildStructure)ab).buildTime *= .75f;
							ab.myCost.ResourceOne *= .75f;
						}

					}


				}
			}
			if (us.UnitName.Contains ("Factory")) {
				foreach (GameObject steel in steelCraftors) {
					UnitManager man = steel.GetComponent<UnitManager> ();
					foreach (Ability ab in man.abilityList) {
						if (ab.Name.Contains ("Factory")) {
							((BuildStructure)ab).buildTime *= 75f;
							ab.myCost.ResourceOne *= .75f;
						}

					}


				}
			}
		}
	}


	public override void unApplyUpgrade (GameObject obj){

		if (confirmUnit (obj)) {
		UnitManager us = obj.GetComponent<UnitManager>();
		if (us.UnitName.Contains("Yard")) {
			foreach (GameObject steel in steelCraftors) {
				UnitManager man = steel.GetComponent<UnitManager> ();
				foreach (Ability ab in man.abilityList) {
					if (ab.Name.Contains ("Yard")) {
						((BuildStructure)ab).buildTime /= .75f;
						ab.myCost.ResourceOne /= .75f;
					}

				}


			}
		}
		if (us.UnitName.Contains("Core")) {
			foreach (GameObject steel in steelCraftors) {
				UnitManager man = steel.GetComponent<UnitManager> ();
				foreach (Ability ab in man.abilityList) {
					if (ab.Name.Contains ("Core")) {
						((BuildStructure)ab).buildTime /= .75f;
						ab.myCost.ResourceOne /= .75f;
					}

				}


			}
		}
		if (us.UnitName.Contains("Factory")) {
				foreach (GameObject steel in steelCraftors) {
					UnitManager man = steel.GetComponent<UnitManager> ();
					foreach (Ability ab in man.abilityList) {
						if (ab.Name.Contains ("Factory")) {
							((BuildStructure)ab).buildTime /= .75f;
							ab.myCost.ResourceOne /= .75f;
						}

					}

				}
			}
		}
	}


}
