using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveVictory : Objective {



	public float SurvivalTime;

	// Use this for initialization
	new void Start () {
		base.Start ();
	

		Invoke ("complete", SurvivalTime);
	}







}
