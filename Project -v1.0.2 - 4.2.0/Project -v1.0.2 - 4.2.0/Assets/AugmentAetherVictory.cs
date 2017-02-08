using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AugmentAetherVictory  : Objective {

	public int numOfAugments;

	public AetherCapture myAetherCore;
	public GameObject actualAetherCore;

	bool routineStarted = false;
	public float delayVic;
	public WaveSpawner counterAttack;

	public int waveNumber = 0;

	bool finishedOnce;
	public SceneEventTrigger attackTrig;


	void Start()
	{base.Start ();
	
		StartCoroutine (checkForVictory ());
	}

	IEnumerator checkForVictory()
	{


		while (true) {
			
			yield return new WaitForSeconds (2);

			if (actualAetherCore.GetComponent<AugmentAttachPoint> ().myAugment) {
				break;
			
			}
		}
		attackTrig.trigger (0, 0, Vector3.zero, null, false);
		counterAttack.spawnWave (waveNumber);
		yield return new WaitForSeconds (delayVic);
		myAetherCore.capture ();

		
	}




	public void nextArea()
	{
		if(nextObjective){
			complete ();
			Debug.Log ("Calling next area");
			nextObjective.GetComponent<FogOfWarUnit> ().enabled = true;
		}

	}

}
