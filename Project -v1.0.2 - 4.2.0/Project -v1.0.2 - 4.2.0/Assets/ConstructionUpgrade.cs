using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructionUpgrade  :SpecificUpgrade{

	public List<GameObject> steelCraftors = new List<GameObject>();



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){


		UnitManager man = obj.GetComponent<UnitManager> ();
		if (man.UnitName.Contains ("Steel")) {
		
			foreach (Ability ab in man.abilityList) {


				if (ab){
					foreach (string toApply in unitsToApply) {
						if (ab.Name.Contains (toApply)) {
							((BuildStructure)ab).buildTime *= .75f;
							ab.myCost.ResourceOne *= .75f;
							((BuildStructure)ab).animationRate = 1.333f;

							break;
						}
					}

				}

			}
		
		}
	
	}


	public override void unApplyUpgrade (GameObject obj){


	}


	public override float ChangeString (string name, float number)
	{
		return number;
	}

}
