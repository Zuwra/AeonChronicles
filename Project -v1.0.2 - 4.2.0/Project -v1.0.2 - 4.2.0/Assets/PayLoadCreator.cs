using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayLoadCreator : MonoBehaviour {




	//set all payloads as inactive before starting the scene
	public List<GameObject> myPayloads;
	[Tooltip("Make sure this list has the same number of indeces as the first list")]
	public List<int> spawnTimes;

	int currentIndex;
	public GameObject endZone;
	// Use this for initialization
	void Start () {

		if(myPayloads.Count> 0){
			Invoke( "SpawnPayload", spawnTimes[0]);
		}

	}


	void SpawnPayload()
	{
		Debug.Log ("Turning on Payload");
		myPayloads [currentIndex].SetActive (true);
		if (endZone) {
			myPayloads [currentIndex].GetComponent<UnitManager> ().GiveOrder (Orders.CreateMoveOrder (endZone.transform.position));
		}

		currentIndex ++;
		if (currentIndex < myPayloads.Count) {
			Invoke( "SpawnPayload", spawnTimes[currentIndex]-spawnTimes[currentIndex -1]);
		}
	}


}
