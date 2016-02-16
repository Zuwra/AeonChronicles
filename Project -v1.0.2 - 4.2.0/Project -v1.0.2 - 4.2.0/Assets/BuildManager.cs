using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour {



	public List<UnitProduction> buildOrder = new List<UnitProduction>();

	private Selected mySelect;
	private BuilderUI build;

	// Use this for initialization
	void Start () {
		build = GameObject.FindObjectOfType<BuilderUI> ();
		mySelect = GetComponent<Selected> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void cancel()
	{
		if (buildOrder.Count > 0) {
			buildOrder [0].cancelBuilding ();
			buildOrder.RemoveAt (0);

			if(buildOrder.Count > 0)
			{
				buildOrder [0].startBuilding();

			}
			if (mySelect.IsSelected) {
				build.bUpdate ();
			}
		}
	}

	public void cancel(int n)
	{
		if (n == 0) {
			cancel ();
		} else {
			if (buildOrder.Count > n) {
				buildOrder.RemoveAt (n);

				if (mySelect.IsSelected) {
					build.bUpdate ();
				}
			}


		}
	}

	public bool buildUnit(UnitProduction prod)
	{Debug.Log ("Adding " + prod);
		buildOrder.Add (prod);
		if (mySelect.IsSelected) {
			build.bUpdate ();
		}
	
		if (buildOrder.Count == 1) {
			buildOrder [0].startBuilding();
			return false;}
		return true;

	}


	public bool unitFinished(UnitProduction prod)
	{
		buildOrder.RemoveAt(0);
		Debug.Log ("Removing " + buildOrder.Count);
		if(buildOrder.Count > 0)
		{
			buildOrder [0].startBuilding();

		}
		if (mySelect.IsSelected) {
			build.bUpdate ();
		}

		return true;
	}

}
