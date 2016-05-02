using UnityEngine;
using System.Collections;

public class ObjectiveTrigger : MonoBehaviour {

	// triggers new bonus objective when your troops enter an area
	public Objective myObj;
	public bool finishObjective;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other)
	{

		if (other.GetComponent<UnitManager> ())
		if (other.GetComponent<UnitManager> ().PlayerOwner == 1) {
			if (finishObjective) {
				myObj.complete ();
			} else {
				VictoryTrigger.instance.addObjective (myObj);
			}
			Destroy (this.gameObject);
		}
	}




}
