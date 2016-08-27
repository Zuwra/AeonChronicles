using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructionUpgrade  :Upgrade{

	public List<GameObject> steelCraftors = new List<GameObject>();


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){


		UnitManager us = obj.GetComponent<UnitManager>();
		if (us.UnitName.Contains("Yard")) {
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
		if (us.UnitName.Contains("Core")) {
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
		if (us.UnitName.Contains("Factory")) {
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


	public override void unApplyUpgrade (GameObject obj){


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
