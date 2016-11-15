using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AugmentAetherVictory  : Objective {

	public int numOfAugments;

	List<AugmentAttachPoint> myGuys = new List<AugmentAttachPoint> ();
	bool routineStarted = false;
	public float delayVic;
	public WaveSpawner counterAttack;

	public int waveNumber = 0;

	bool finishedOnce;
	public SceneEventTrigger attackTrig;
	// Update is called once per frame
	void Update () {
	


	}

	public void OnTriggerEnter(Collider other)

	{
		UnitManager m = other.GetComponent<UnitManager> ();
		if (m && m.UnitName =="Aether Core") {
			Debug.Log ("Adding one ather core");
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
				if (completed) {
					//completed = false;
					//unComplete ();
				}
		
				continue;
			}
			else{
				if(!completed){
				int n = 0;
				myGuys.RemoveAll (item => item == null);
				foreach (AugmentAttachPoint agp in myGuys) {


					if (agp.myAugment) {

							Debug.Log ("Found one " + agp.myAugment);
						n++;}
				}
				if (n >= numOfAugments && !completed) {
					completed = true;
					
						if (finishedOnce) {
							complete ();
							nextArea ();
						} else {
							StartCoroutine (actuallyComplete ());
						
						}
					//nextArea ();
				}
				else if (completed) {
					//completed = false;
					//unComplete ();
					}
				}
			}
		}
	}

	IEnumerator actuallyComplete()
	{
		if (counterAttack) {
			counterAttack.spawnWave (0);
			if (attackTrig) {
				attackTrig.trigger (0, 0, Vector3.zero, null, false);}

		} else {
			complete ();
			nextArea ();
			return true;
		}
		finishedOnce = true;
		yield return new WaitForSeconds (delayVic);

		int n = 0;
		myGuys.RemoveAll (item => item == null);
		foreach (AugmentAttachPoint agp in myGuys) {
			if (agp.myAugment) {

				n++;}
		}
		if (n >= numOfAugments && !completed) {
			completed = true;
			StartCoroutine (actuallyComplete ());
			complete ();
			nextArea ();
		} else {
			completed = false;}
	

	}

	public void nextArea()
	{
		if(nextObjective){
			nextObjective.GetComponent<FogOfWarUnit> ().enabled = true;
		}

	}

}
