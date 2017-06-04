using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayLoadCreator : MonoBehaviour {


	[Tooltip("set all payloads as inactive before starting the scene")]
	public List<PayloadWave> PayLoadWaves;

	int currentIndex;
	public GameObject endZone;
	// Use this for initialization
	void Start () {

		if(PayLoadWaves.Count> 0){
			Invoke( "SpawnPayload", PayLoadWaves[0].spawnTime);
		}

	}


	public void SpawnEarly(float delayTime)
	{
		CancelInvoke ("SpawnPayload");

		if (currentIndex < PayLoadWaves.Count) {

			Invoke( "SpawnPayload", 20 );
		}
	}

	void SpawnPayload()
	{
		if (PayLoadWaves [currentIndex].TextIndex > -1) {
			dialogManager.instance.playLine (PayLoadWaves [currentIndex].TextIndex);
		}
	//	Debug.Log ("Turning on Payload");
		foreach (GameObject obj in PayLoadWaves[currentIndex].myPayloads) {
			obj.SetActive (true);

			if (endZone) {
				obj.GetComponent<UnitManager> ().GiveOrder (Orders.CreateMoveOrder (endZone.transform.position));
			}
		}



		currentIndex ++;
		if (currentIndex < PayLoadWaves.Count) {
			
			Invoke( "SpawnPayload", PayLoadWaves[currentIndex].spawnTime - PayLoadWaves[currentIndex-1].spawnTime );
		}
	}


}


[System.Serializable]
public class PayloadWave{
	public int spawnTime;
	public List<GameObject> myPayloads;
	public int TextIndex = -1;


}
