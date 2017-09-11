using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjective : Objective {
	
	public float remainingTime;

	string initialDescript;
	// Use this for initialization
	new void Start () {

		if (ActiveOnStart) {
			BeginObjective ();
		}

		initialDescript = description;

	}

	public override void BeginObjective()
	{base.BeginObjective ();
		StartCoroutine (countDown());
	}


	IEnumerator countDown()
	{
		Debug.Log ("Starting countddown");
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
