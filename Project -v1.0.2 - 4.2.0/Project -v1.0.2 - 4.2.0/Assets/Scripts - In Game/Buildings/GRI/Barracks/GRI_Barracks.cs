using UnityEngine;
using System.Collections;

public class GRI_Barracks : Building {

	// Use this for initialization
	new void Start () 
	{
		AssignDetails (ItemDB.GRIBarracks);
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
