using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour {



	public List<UnitProduction> buildOrder = new List<UnitProduction>();



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public bool buildUnit(UnitProduction prod)
	{
		buildOrder.Add (prod);

	
		if (buildOrder.Count == 1) {
			buildOrder [0].startBuilding();
			return false;}
		return true;

	}


	public bool unitFinished(UnitProduction prod)
	{
		buildOrder.RemoveAt(0);

		if(buildOrder.Count > 0)
		{
			buildOrder [0].startBuilding();

		}
		return true;
	}

}
