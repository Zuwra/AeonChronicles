using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateObject : SceneEventTrigger {

	public List<GameObject> objectsToActivate = new List<GameObject>();

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
