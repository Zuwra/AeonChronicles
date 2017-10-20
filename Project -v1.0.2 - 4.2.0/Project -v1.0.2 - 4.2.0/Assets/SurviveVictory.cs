using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveVictory : Objective {

	public MultiShotParticle myParticles;

	public GameObject QuakeBuilding;
	public float SurvivalTime;
	int pulsesUsed;
	float startTime;

	string rawObjectText;
	// Use this for initialization
	new void Start () {
		rawObjectText = description;
		startTime = Time.time;
		VictoryTrigger.instance.addObjective (this);
	

		Invoke ("WaitFunction", SurvivalTime);
		InvokeRepeating ("UpdateObj", 1,1);
	}


	public void UpdateObj()
	{
		description = rawObjectText + " " + Clock.convertToString(SurvivalTime + (pulsesUsed * 15) - (Time.timeSinceLevelLoad - startTime));
		VictoryTrigger.instance.UpdateObjective (this);
	}


	public void increaseWait ()
	{
		CancelInvoke ("WaitFunction");
		pulsesUsed++;
		Invoke ("WaitFunction", SurvivalTime + (pulsesUsed * 10) - Time.timeSinceLevelLoad);
	}

	void WaitFunction()
	{

		StartCoroutine (endEffects ());
	}

	IEnumerator endEffects()
	{
		MainCamera.main.ShakeCamera (7, 20, .1f);
		MainCamera.main.setCutScene (QuakeBuilding.transform.position, 100);
		yield return new WaitForSeconds (.5f);
		myParticles.playEffect ();
		QuakeBuilding.GetComponentInChildren<Animator> ().SetTrigger("Pulse");
		yield return new WaitForSeconds (1f);
		myParticles.playEffect ();
		QuakeBuilding.GetComponentInChildren<Animator> ().SetTrigger("Pulse");
		yield return new WaitForSeconds (1f);
		myParticles.playEffect ();
		QuakeBuilding.GetComponentInChildren<Animator> ().SetTrigger("Pulse");
		yield return new WaitForSeconds (1f);
		myParticles.playEffect ();
		QuakeBuilding.GetComponentInChildren<Animator> ().SetTrigger("Pulse");
		yield return new WaitForSeconds (1f);
		complete ();


	}




}
