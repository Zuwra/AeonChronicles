using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadFirendlyEndZone : MonoBehaviour {



	public List<VariableTargetZones> targetZones;
	public List<Objective> myObjectives;

	[Tooltip("Objectives will be invisible until they are added to objectives")]
	public bool hideUntilRevealed;

	Coroutine currentCo;

	int payloadIndex;
	// Use this for initialization
	void Start () {

		foreach (VariableTargetZones zone in targetZones) {
		
			while (zone.AreaTargets.Count > zone.SinceLastLaunch.Count) {
				int n = Random.Range (0, zone.AreaTargets.Count);
				zone.AreaTargets [n].transform.parent.gameObject.SetActive (false);
				zone.AreaTargets.RemoveAt (n);
			}
		
		}

		if(hideUntilRevealed){
			foreach (VariableTargetZones zone in targetZones) 
			{
				foreach (GameObject obj in zone.AreaTargets) {
					obj.SetActive (false);
				}
			}
		}


		int spawnIndex = Random.Range (0, targetZones[0].AreaTargets.Count);
		currentLoc = targetZones [0].AreaTargets [spawnIndex];
		currentCo =  StartCoroutine (OpenLocation (targetZones[0].AreaTargets[spawnIndex],targetZones[0].SinceLastLaunch[0]));
		targetZones[0].AreaTargets.RemoveAt (spawnIndex);
		targetZones[0].SinceLastLaunch.RemoveAt (0);

	}

	GameObject currentLoc;

	public void DoItNow()
	{
		StopCoroutine (currentCo);
		currentCo =  StartCoroutine (OpenLocation (currentLoc,20));

	}

	IEnumerator OpenLocation(GameObject loc, float waitTime)
	{



		yield return new WaitForSeconds (waitTime);
		loc.SetActive (true);
		loc.GetComponent<FogOfWarUnit> ().enabled = true;
		loc.GetComponent<FogOfWarUnit> ().move ();
		loc.GetComponent<ObjectiveTrigger> ().myObj = myObjectives [payloadIndex];
		myObjectives [payloadIndex].enabled = true;
		payloadIndex++;

		while (targetZones.Count >0 && targetZones [0].AreaTargets.Count == 0) {
			targetZones.RemoveAt (0);
		}

		if (targetZones.Count > 0) {
			
			int spawnIndex = Random.Range (0, targetZones[0].AreaTargets.Count);
			currentLoc = targetZones [0].AreaTargets [spawnIndex];
			currentCo =  StartCoroutine (OpenLocation (targetZones[0].AreaTargets[spawnIndex],targetZones[0].SinceLastLaunch[0]));
			targetZones[0].AreaTargets.RemoveAt (spawnIndex);
			targetZones[0].SinceLastLaunch.RemoveAt (0);

		} 
	}


	[System.Serializable]
	public class VariableTargetZones{
		public List<GameObject> AreaTargets;
		public List<float> SinceLastLaunch;

	}

}
