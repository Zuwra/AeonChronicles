using UnityEngine;
using System.Collections;

public class ObjectiveTrigger : MonoBehaviour {


	public string objective;
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
			ObjectiveManager.instance.setObjective (objective);
			Destroy (this.gameObject);
		}
	}




}
