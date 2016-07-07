using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildUnitObjective : Objective  {

	public List<GameObject> unitsToBuild = new List<GameObject> ();
	public bool anyCombo;

	private int total = -3;

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void buildUnit(GameObject obj)
	{

		for (int i = 0; i < unitsToBuild.Count; i++) {

			if (unitsToBuild [i].GetComponent<UnitManager> ().UnitName == obj.GetComponent<UnitManager> ().UnitName) {
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
