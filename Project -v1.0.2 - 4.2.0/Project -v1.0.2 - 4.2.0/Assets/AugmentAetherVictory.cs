using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AugmentAetherVictory  : Objective {

	public int numOfAugments;

	List<AugmentAttachPoint> myGuys = new List<AugmentAttachPoint> ();
	bool routineStarted = false;

	
	// Update is called once per frame
	void Update () {
	


	}

	public void OnTriggerEnter(Collider other)

	{
		UnitManager m = other.GetComponent<UnitManager> ();
		if (m && m.UnitName =="Aether Core") {
			Debug.Log ("Adding one");
			myGuys.Add (other.GetComponent<AugmentAttachPoint> ());
			if (!routineStarted) {
				routineStarted = true;
				StartCoroutine (checkForVictory ());
			}

		}
	}

	IEnumerator checkForVictory()
	{


		while (true) {

			yield return new WaitForSeconds (3);
		
			if (myGuys.Count < numOfAugments) {
		
				continue;
			}
			else{
				int n = 0;
			
				foreach (AugmentAttachPoint agp in myGuys) {
					if (agp.myAugment) {

						n++;}
				}
				if (n >= numOfAugments) {
					complete ();
				}
			}
		}
	}
}
