using UnityEngine;
using System.Collections;

public class ObjectiveTrigger : SceneEventTrigger {

	// triggers new bonus objective when your troops enter an area
	public Objective myObj;
	public bool finishObjective;

	public UnityEngine.Events.UnityEvent FunctionTrigger;
	//public bool UnitEnter;

	/*
	//void OnTriggerEnter(Collider other)
	//{
		if (UnitEnter) {
			if (other.GetComponent<UnitManager> ())
			if (other.GetComponent<UnitManager> ().PlayerOwner == 1) {

				trigger (0, 0, Vector3.zero, null, finishObjective);

			}
		}
	//}
	*/

	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		if (finishObjective && myObj) {
			myObj.complete ();
		} else {
			VictoryTrigger.instance.addObjective (myObj);
		}

		if (FunctionTrigger != null) {
			FunctionTrigger.Invoke ();
		}
		//Destroy (this.gameObject);
	
	}


}
