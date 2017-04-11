using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadFirendlyEndZone : MonoBehaviour {


	public List<GameObject> AreaOne;
	[Tooltip("This is the time between each wave, or since the start of the level for the first one")]
	public List<float> AreaOneSpawnTimes;

	public List<GameObject> AreaTwo;
	public List<float> AreaTwoSpawnTimes;

	public List<Objective> myObjectives;


	int payloadIndex;
	// Use this for initialization
	void Start () {
		 
		GameObject DeleteOne = AreaOne [Random.Range (0, AreaOne.Count)];
		DeleteOne.transform.parent.gameObject.SetActive (false);
		AreaOne.Remove (DeleteOne);

		GameObject DeleteTwo = AreaOne [Random.Range (0, AreaOne.Count)];
		DeleteTwo.transform.parent.gameObject.SetActive (false);
		AreaOne.Remove (DeleteTwo);

		GameObject DeleteThree = AreaTwo [Random.Range (0, AreaTwo.Count)];
		DeleteThree.transform.parent.gameObject.SetActive (false);
		AreaTwo.Remove (DeleteThree);


		foreach (GameObject obj in AreaOne) {
			obj.SetActive (false);

		}
		foreach (GameObject obj in AreaTwo) {
			obj.SetActive (false);

		}

		int spawnIndex = Random.Range (0, AreaOne.Count);
		StartCoroutine (OpenLocation (AreaOne [spawnIndex], AreaOneSpawnTimes [0]));
		AreaOne.RemoveAt (spawnIndex);
		AreaOneSpawnTimes.RemoveAt (0);

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

		if (AreaOneSpawnTimes.Count > 0) {
		
			int spawnIndex = Random.Range (0, AreaOne.Count);
			StartCoroutine (OpenLocation (AreaOne [spawnIndex], AreaOneSpawnTimes [0]));
			AreaOne.RemoveAt (spawnIndex);
			AreaOneSpawnTimes.RemoveAt (0);

		} else if(AreaTwoSpawnTimes.Count > 0) {

			int spawnIndex = Random.Range (0, AreaTwo.Count);
			StartCoroutine (OpenLocation (AreaTwo [spawnIndex], AreaTwoSpawnTimes [0]));
			AreaTwo.RemoveAt (spawnIndex);
			AreaTwoSpawnTimes.RemoveAt (0);
		
		}
	}


}
