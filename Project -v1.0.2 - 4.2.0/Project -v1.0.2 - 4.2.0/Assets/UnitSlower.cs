using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlower : MonoBehaviour {


	public bool slowUnits;

	int playerOwner;
	float mySpeed;

	public float slowAmount = -.45f;

	public List<string> excludedUnits;
	// Use this for initialization
	void Start () {
		playerOwner = GetComponentInParent<UnitManager> ().PlayerOwner;
		mySpeed = GetComponentInParent<UnitManager> ().cMover.MaxSpeed;
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		//Debug.Log ("Entering unit " + col.gameObject);
		UnitManager manage = col.GetComponent<UnitManager> ();
		if (manage) {
			if (manage.PlayerOwner == playerOwner && manage.cMover && !excludedUnits.Contains(manage.UnitName)) {
				StartCoroutine (waitForSec (manage));
			
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		UnitManager manage = col.GetComponent<UnitManager> ();
		if (manage) {
			if (manage.PlayerOwner == playerOwner && manage.cMover && !excludedUnits.Contains(manage.UnitName)) {
				manage.cMover.removeSpeedBuff (this);

			}
		}
	}


	IEnumerator waitForSec( UnitManager manage)
	{
		yield return null;
		if (slowUnits) {
			manage.cMover.changeSpeed (slowAmount, 0, false, this);
		} else {
			manage.cMover.changeSpeed (.5f, 0,false, this);
		}


	}


}
