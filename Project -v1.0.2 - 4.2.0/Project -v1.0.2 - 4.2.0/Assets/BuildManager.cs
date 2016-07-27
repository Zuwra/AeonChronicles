using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour {



	public List<UnitProduction> buildOrder = new List<UnitProduction>();

	private Selected mySelect;
	private BuilderUI build;
	private RaceManager raceMan;

	// Use this for initialization
	void Start () {
		raceMan = GameObject.FindObjectOfType<GameManager> ().activePlayer ;
		build = GameObject.FindObjectOfType<BuilderUI> ();
		mySelect = GetComponent<Selected> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void cancel()
	{
		if (buildOrder.Count > 0) {
			buildOrder [0].DeQueueUnit ();
			buildOrder [0].cancelBuilding ();
			buildOrder.RemoveAt (0);

			if(buildOrder.Count > 0)
			{
				float Sup = buildOrder [0].unitToBuild.GetComponent<UnitStats> ().supply;

				if (Sup == 0 ||raceMan.hasSupplyAvailable (Sup)) {
					buildOrder [0].startBuilding ();
				} else {
					StartCoroutine (waitOnSupply (Sup));
				}

			}
			if (mySelect.IsSelected) {
				build.bUpdate (this.gameObject);
			}
		}
	}

	public void cancel(int n)
	{
		if (n == 0) {
			cancel ();
		} else {
			
			if (buildOrder.Count > n) {
				buildOrder [n].DeQueueUnit ();
				buildOrder.RemoveAt (n);

				if (mySelect.IsSelected) {
					build.bUpdate (this.gameObject);
				}
			}


		}
	}

	public bool buildUnit(UnitProduction prod)
	{if (buildOrder.Count >= 5) {
			return false;
		
		}

		buildOrder.Add (prod);
		if (mySelect.IsSelected) {
			build.bUpdate (this.gameObject);
		}
	
		if(buildOrder.Count == 1)
		{
			float Sup = buildOrder [0].unitToBuild.GetComponent<UnitStats> ().supply;

			if (Sup == 0 ||raceMan.hasSupplyAvailable (Sup)) {
				buildOrder [0].startBuilding ();
			} else {
				StartCoroutine (waitOnSupply (Sup));
			}

		}
		return true;

	}


	public bool unitFinished(UnitProduction prod)
	{
		buildOrder.RemoveAt(0);
	
		if(buildOrder.Count > 0)
		{
			float Sup = buildOrder [0].unitToBuild.GetComponent<UnitStats> ().supply;

			if (Sup == 0 || raceMan.hasSupplyAvailable (Sup)) {
				buildOrder [0].startBuilding ();
			} else {
				StartCoroutine (waitOnSupply (Sup));
			}

		}
		if (mySelect.IsSelected) {
			build.bUpdate (this.gameObject);
		}

		return true;
	}


	IEnumerator waitOnSupply(float supply)
	{

		ErrorPrompt.instance.notEnoughSupply ();
		while (buildOrder.Count > 0) {
			yield return new WaitForSeconds (.3f);
			
			if (buildOrder.Count > 0) {
				if (supply == 0 ||raceMan.hasSupplyAvailable (supply)) {
					buildOrder [0].startBuilding ();
					break;
				} 
			} else {
				break;
			}
		}
		
	}

}
