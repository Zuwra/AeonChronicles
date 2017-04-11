using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AugmentAetherVictory  : Objective {

	public int numOfAugments;

	public AetherCapture myAetherCore;
	public GameObject actualAetherCore;

	//bool routineStarted = false;
	public float delayVic;
	public WaveSpawner counterAttack;

	public int waveNumber = 0;

	bool finishedOnce;
	public SceneEventTrigger attackTrig;
	public  Selected CoolDown;
	float TimeLeft =30;

	new void Start()
	{base.Start ();
	
		StartCoroutine (checkForVictory ());
	}

	IEnumerator checkForVictory()
	{
		while (true) {
			
			yield return new WaitForSeconds (1);

			if (TimeLeft <= 0) {
				break;
			}
			if (actualAetherCore.GetComponent<AugmentAttachPoint> ().myAugment) {
				StartCoroutine (cooldownBar ());
				spawnWave ();

			
			}
		}
		myAetherCore.capture ();
		nextArea ();
	}

	IEnumerator cooldownBar()
	{
		float miniCooldown = 1;
		while(miniCooldown > 0)
		{

			yield return new WaitForSeconds (.1f);
			miniCooldown -= .1f;
			TimeLeft -= .1f;
			CoolDown.updateCoolDown (TimeLeft / 30);
		
		}

	}

	bool waveHasSpawned;
	void spawnWave()
	{
		if (!waveHasSpawned) {
			waveHasSpawned = true;
			attackTrig.trigger (0, 0, Vector3.zero, null, false);
			counterAttack.spawnWave (waveNumber);
		}
	
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
