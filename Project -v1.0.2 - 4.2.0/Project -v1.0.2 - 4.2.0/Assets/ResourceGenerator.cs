using UnityEngine;
using System.Collections;

public class ResourceGenerator : MonoBehaviour {

	private RaceManager racemanager;


	public float ResourceOneRate;
	public float ResourceTwoRate;

	private float nextActionTime;

	// Use this for initialization
	void Start () {
		racemanager = GameObject.Find ("GameRaceManager").GetComponent<RaceManager> ();

		nextActionTime = Time.time;
	}

	// Update is called once per frame
	void Update () {

		if (Time.time > nextActionTime) {
			nextActionTime += 1f;

		
			racemanager.updateResources(ResourceOneRate,ResourceTwoRate, true);
				
				

			}


	}



}
