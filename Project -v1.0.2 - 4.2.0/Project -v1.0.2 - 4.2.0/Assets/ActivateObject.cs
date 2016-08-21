using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateObject : SceneEventTrigger {

	public List<GameObject> objectsToActivate = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		if (!hasTriggered) {
			hasTriggered = true;

			foreach (GameObject obj in objectsToActivate) {
				if (obj) {
					obj.SetActive (true);
				}
			}

		}

	}
}
