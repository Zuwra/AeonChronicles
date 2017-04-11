using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjective : Objective {
	
	public float remainingTime;

	string initialDescript;
	// Use this for initialization
	new void Start () {
		base.Start ();
		initialDescript = description;
		StartCoroutine (countDown());
	}

	IEnumerator countDown()
	{

		while (remainingTime > 0 && !completed) {
			yield return new WaitForSeconds (1);
			remainingTime -= 1;
			description = initialDescript + "  " + Clock.convertToString(remainingTime);
			VictoryTrigger.instance.UpdateObjective (this);
		
		
		}
		if (!completed) {
			VictoryTrigger.instance.Lose ();

		}


	}
	// Update is called once per frame

}
