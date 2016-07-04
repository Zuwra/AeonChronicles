using UnityEngine;
using System.Collections;

public class ObjectiveTrigger : SceneEventTrigger {

	// triggers new bonus objective when your troops enter an area
	public Objective myObj;
	public bool finishObjective;
	public bool UnitEnter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other)
	{/*
		if (UnitEnter) {
			if (other.GetComponent<UnitManager> ())
			if (other.GetComponent<UnitManager> ().PlayerOwner == 1) {

				trigger (0, 0, Vector3.zero, null, finishObjective);

			}
		}*/
	}


	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		if (finishObjective) {
			myObj.complete ();
		} else {
			VictoryTrigger.instance.addObjective (myObj);
		}
		//Destroy (this.gameObject);
	
	}


}
