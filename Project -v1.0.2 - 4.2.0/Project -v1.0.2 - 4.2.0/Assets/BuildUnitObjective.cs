using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildUnitObjective : Objective  {

	public List<GameObject> unitsToBuild = new List<GameObject> ();
	public bool anyCombo;

	private int total = -3;

	



	public void buildUnit(UnitManager obj)
	{

		for (int i = 0; i < unitsToBuild.Count; i++) {

			if (unitsToBuild [i].GetComponent<UnitManager> ().UnitName == obj.UnitName) {
				if (anyCombo) {
					total++;
					//Debug.Log (total +"  " +unitsToBuild.Count );
					if (total == unitsToBuild.Count) {
						complete ();
					}
				} else {
					unitsToBuild.RemoveAt (i);
				
					if (unitsToBuild.Count == 0) {
					
						complete ();

					}
				}
				return;
			
			}
		}
	}
}
