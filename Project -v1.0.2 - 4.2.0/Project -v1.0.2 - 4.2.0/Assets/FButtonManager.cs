using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FButtonManager : MonoBehaviour {

	public Text Ffive;
	public Text Fsix;
	public Text fSeven;
	public Text fEight;

	public static FButtonManager main;

	public Text idleWorkers;
	public Text TotalArmy;
	public Text unbound;
	public Text AllBuildings;

	//SelectedManager selectManager;


	// Use this for initialization
	void Awake () {
		//selectManager = GameObject.Find ("Manager").GetComponent<SelectedManager>();
		//setButtons ();
		main = this;
	
	}
	


	public void updateNumbers(List<GameObject> myUnits)
	{
		int tArmy = 0;
		int totalBuilding = 0;

		foreach (GameObject obj in myUnits) {
			UnitManager manage = obj.GetComponent<UnitManager> ();
			if (manage.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				totalBuilding++;
			} else if (!manage.myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
				tArmy++;
			}
		}

		TotalArmy.text = "" + tArmy;
		AllBuildings.text = "" + totalBuilding;
	}


	// THis will need to be changed for future race workers.
	public void changeWorkers ()
	{int workerCount = 0;
		foreach (newWorkerInteract worker in GameObject.FindObjectsOfType<newWorkerInteract>()) {
			if (worker.GetComponent<UnitManager> ().getState () is DefaultState) {
				workerCount++;
			}
		}

		idleWorkers.text = "" + workerCount;
	}

	public void updateTankNumber()
	{
		unbound.text = "" + SelectedManager.main.getUnarmedTankCount ();
		//Debug.Log ("Updated " + SelectedManager.main.getUnarmedTankCount ());
	}

}
