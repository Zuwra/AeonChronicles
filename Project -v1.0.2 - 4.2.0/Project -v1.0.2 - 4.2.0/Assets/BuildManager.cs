using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour {



	public List<UnitProduction> buildOrder = new List<UnitProduction>();

	private Selected mySelect;
	private BuilderUI build;
	private RaceManager raceMan;
	private bool isWorker;
	public bool waitingOnSupply;

	// Use this for initialization
	void Start () {
		raceMan = GameObject.FindObjectOfType<GameManager> ().activePlayer ;
		build = GameObject.FindObjectOfType<BuilderUI> ();
		mySelect = GetComponent<Selected> ();
		if (GetComponent<newWorkerInteract> ()) {
			isWorker = true;}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void cancel()
	{
		//Debug.Log ("Cancel one");
		
			if (buildOrder.Count > 0) {
			if (!isWorker) {
				buildOrder [0].DeQueueUnit ();
		
				buildOrder [0].cancelBuilding ();
			}
				buildOrder.RemoveAt (0);

			if (buildOrder.Count > 0) {
				float Sup = buildOrder [0].unitToBuild.GetComponent<UnitStats> ().supply;

				if (Sup == 0 || raceMan.hasSupplyAvailable (Sup)) {
					buildOrder [0].startBuilding ();
					build.hasSupply ();
				} else {
					build.NoSupply ();
					StartCoroutine (waitOnSupply (Sup));
				}

			} else {
			
				waitingOnSupply = false;
				if (mySelect.IsSelected) {
					//Debug.Log ("Resetting it");
					build.hasSupply ();
				}
			}
				if (mySelect.IsSelected) {
					build.bUpdate (this.gameObject);
				}

		}

		mySelect.updateIconNum ();
	}



	public void checkForSupply()
	{if (!buildOrder [0].unitToBuild) {
			buildOrder [0].startBuilding ();
			return;}
		float Sup = buildOrder [0].unitToBuild.GetComponent<UnitStats> ().supply;

		if (Sup == 0 || raceMan.hasSupplyAvailable (Sup)) {
			buildOrder [0].startBuilding ();
			if (mySelect.IsSelected) {
				waitingOnSupply = false;
				build.hasSupply ();
			
			}
		} else {
			if (mySelect.IsSelected){
				waitingOnSupply = true;
				build.NoSupply ();}
			StartCoroutine (waitOnSupply (Sup));
		}
	}

	public void cancel(int n)
	{//	Debug.Log ("Cancel other");
		if (n == 0) {
			if (buildOrder.Count > 0) {

				buildOrder [0].DeQueueUnit ();
				buildOrder [0].cancelBuilding ();

				buildOrder.RemoveAt (0);

				if (buildOrder.Count > 0) {
					checkForSupply ();

				}
				if (mySelect.IsSelected) {
					build.bUpdate (this.gameObject);
				}

			}
		} else {
			
			if (buildOrder.Count > n) {
				buildOrder [n].DeQueueUnit ();
				buildOrder.RemoveAt (n);


				if (mySelect.IsSelected) {
					build.bUpdate (this.gameObject);
				}
			}


		}
		mySelect.updateIconNum ();
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
			checkForSupply ();

		}
		mySelect.updateIconNum ();
		return true;

	}


	public bool unitFinished(UnitProduction prod)
	{
		buildOrder.RemoveAt(0);
	
		if(buildOrder.Count > 0)
		{
			checkForSupply ();

		}
		if (mySelect.IsSelected) {
			build.bUpdate (this.gameObject);
		}
		mySelect.updateIconNum ();
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
					build.hasSupply ();
					waitingOnSupply = false;
					break;
				} 
			} else {
				break;
			}
		}
		
	}

}
