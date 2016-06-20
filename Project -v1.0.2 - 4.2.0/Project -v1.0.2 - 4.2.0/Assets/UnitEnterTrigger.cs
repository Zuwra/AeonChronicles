using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitEnterTrigger : MonoBehaviour {

	public int player = 1;

	public  int index;
	public  float input;
	public  Vector3 location;
	public  GameObject target; 
	public  bool doIt;

	public List<SceneEventTrigger> myTriggers;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<UnitManager> ())
		if (other.GetComponent<UnitManager> ().PlayerOwner == player) {

			foreach (SceneEventTrigger trig in myTriggers) {
				trig.trigger (index, input, location, target, doIt);
			}
			Destroy (this.gameObject);
		}
	}


}
