using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlower : MonoBehaviour {


	public bool slowUnits;

	int playerOwner;
	float mySpeed;
	// Use this for initialization
	void Start () {
		playerOwner = GetComponentInParent<UnitManager> ().PlayerOwner;
		mySpeed = GetComponentInParent<UnitManager> ().cMover.MaxSpeed;
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		UnitManager manage = col.GetComponent<UnitManager> ();
		if (manage) {
			if (manage.PlayerOwner == playerOwner && manage.cMover) {
				StartCoroutine (waitForSec (manage));
			
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		UnitManager manage = col.GetComponent<UnitManager> ();
		if (manage) {
			if (manage.PlayerOwner == playerOwner && manage.cMover) {
				manage.cMover.removeSpeedBuff (this);

			}
		}
	}


	IEnumerator waitForSec( UnitManager manage)
	{
		yield return null;

		if (slowUnits) {
			manage.cMover.changeSpeed (.45f, 0, false, this);
		} else {
			manage.cMover.changeSpeed (1.5f, 0,false, this);
		}


	}


}
