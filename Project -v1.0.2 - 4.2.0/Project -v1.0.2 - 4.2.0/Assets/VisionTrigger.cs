using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisionTrigger : MonoBehaviour {

	public int PlayerNumber;

	public List<UnitManager> InVision;
	public abstract void  UnitEnterTrigger(UnitManager manager);
	public abstract void  UnitExitTrigger(UnitManager manager);

	void OnTriggerEnter(Collider other) {

		UnitManager otherManager = other.gameObject.GetComponent<UnitManager> ();
		if (otherManager && otherManager.PlayerOwner.Equals (PlayerNumber)) {

			InVision.Add (otherManager);
			UnitEnterTrigger (otherManager);
		}

	}

	void OnTriggerExit(Collider other) {

		UnitManager otherManager = other.gameObject.GetComponent<UnitManager> ();
		if (otherManager) {

			InVision.Remove(otherManager);
			UnitExitTrigger (otherManager);
		}

	}


}
