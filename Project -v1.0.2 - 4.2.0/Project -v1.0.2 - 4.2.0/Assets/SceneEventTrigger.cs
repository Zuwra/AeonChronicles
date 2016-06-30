using UnityEngine;
using System.Collections;

public abstract class SceneEventTrigger: MonoBehaviour {

	public bool hasTriggered;
	// Any or none of these values may be used.
	public abstract void trigger (int index, float input, Vector3 location, GameObject target, bool doIt);


}
