using UnityEngine;
using System.Collections;

public class sawTrigger : MonoBehaviour {


	public MiningSaw mySaw;
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
			mySaw.turnOn ();
			Destroy (this.gameObject);
		}
	}



}
