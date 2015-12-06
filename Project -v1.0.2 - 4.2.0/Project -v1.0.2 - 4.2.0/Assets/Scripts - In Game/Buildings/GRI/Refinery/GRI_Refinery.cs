using UnityEngine;
using System.Collections;

public class GRI_Refinery : Building {

	// Use this for initialization
	new void Start () 
	{
		AssignDetails (ItemDB.GRIRefinery);
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
