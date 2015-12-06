using UnityEngine;
using System.Collections;

public class GRI_WarFactory : Building {

	// Use this for initialization
	new void Start () 
	{
		AssignDetails (ItemDB.GRIWarFactory);
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
