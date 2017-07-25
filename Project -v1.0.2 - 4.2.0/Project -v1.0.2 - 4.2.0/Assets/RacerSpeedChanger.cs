using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerSpeedChanger : MonoBehaviour {

	UnitManager manager;
	public float maxSpeedIncrease;

	// Use this for initialization
	void Start () {
		manager = GetComponent<UnitManager> ();
		StartCoroutine (ChangeSpeed ());
	}


	IEnumerator ChangeSpeed()
	{manager.cMover.changeSpeed (0,UnityEngine.Random.Range(0,maxSpeedIncrease),false,this);
		while (true) {
			yield return new WaitForSeconds (UnityEngine.Random.Range (3, 8));
			manager.cMover.removeSpeedBuff (this);
			manager.cMover.changeSpeed (0,UnityEngine.Random.Range(0,maxSpeedIncrease),false,this);
			//Debug.Log ("Speed is now " + manager.cMover.MaxSpeed);
		}

	}

}
