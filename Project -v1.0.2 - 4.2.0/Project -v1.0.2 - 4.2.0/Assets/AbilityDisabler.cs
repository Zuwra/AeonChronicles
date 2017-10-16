using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDisabler : Upgrade {

	public List<unitToAffect> ToDisable;
	public List<unitToReplace> ToReplace;
	[System.Serializable]
	public struct unitToAffect
	{
		public GameObject Unit;
		public List<int> index;
		public bool clearNull;

	}

	[System.Serializable]
	public struct unitToReplace
	{
		public GameObject Unit;
		public int index;
		public GameObject ReplaceWith;

	}




	override
	public void applyUpgrade (GameObject obj){


	UnitManager manager = obj.GetComponent<UnitManager>();

		foreach (unitToAffect affect in ToDisable) {
			if (affect.Unit.GetComponent<UnitManager> ().UnitName == manager.UnitName) {
				foreach(int i in affect.index){
					if (i < manager.abilityList.Count) {
						manager.abilityList [i].enabled = false;
						manager.abilityList [i] = null;

					}
				}
				if (affect.clearNull) {
					manager.abilityList.RemoveAll (item => item == null);
				}
			}
			
		
		}

		foreach (unitToReplace affect in ToReplace) {
			if (affect.Unit.GetComponent<UnitManager> ().UnitName == manager.UnitName) {

				if(manager.abilityList [affect.index]){
					BuildUnit builder = (BuildUnit)manager.abilityList [affect.index];
					builder.unitToBuild = affect.ReplaceWith;
				}
			}
		}




	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
