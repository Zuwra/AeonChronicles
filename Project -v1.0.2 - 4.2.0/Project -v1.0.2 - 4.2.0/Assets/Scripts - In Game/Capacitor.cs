using UnityEngine;
using System.Collections;

public class Capacitor : MonoBehaviour {


	private RaceManager racemanager;


	public float maxEnergy;
	public float myEnergy;
	public float RegenRate;

	private float nextActionTime;

	// Use this for initialization
	void Start () {
		racemanager = GameObject.Find ("GameRaceManager").GetComponent<RaceManager> ();
		GameObject.Find ("GameRaceManager").GetComponent<GarataiManager> ().addCapacitor(this);
		nextActionTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextActionTime) {
			nextActionTime += 1f;

			if(myEnergy < maxEnergy){
				if(myEnergy + RegenRate < maxEnergy)
					{
					myEnergy += RegenRate;
					racemanager.updateResources(0,RegenRate, true);
				}
				else
				{float amount = maxEnergy - myEnergy;
					myEnergy = maxEnergy;
				racemanager.updateResources(0,amount, true);
				}
	
			}
		}
	
	}



	public void decrementEnergy(float amount)
	{
		myEnergy -= amount;
	}
}
