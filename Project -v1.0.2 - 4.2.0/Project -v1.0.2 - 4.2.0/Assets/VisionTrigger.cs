using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisionTrigger : MonoBehaviour {

	public int PlayerNumber;

	public List<UnitManager> InVision;
	public abstract void  UnitEnterTrigger(UnitManager manager);
	public abstract void  UnitExitTrigger(UnitManager manager);
	public bool CheckForDeaths = false;

	void OnEnable()
	{
		if (CheckForDeaths) {
			InvokeRepeating ("CheckNull", .5f, .5f);
		}
	}

	void CheckNull(){
		
		if (InVision.RemoveAll (item => item == null) > 0) {
			UnitExitTrigger (null);
		}
	}

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
