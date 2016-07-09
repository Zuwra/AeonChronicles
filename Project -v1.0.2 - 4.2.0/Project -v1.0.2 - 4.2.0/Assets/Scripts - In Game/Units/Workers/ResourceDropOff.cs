using UnityEngine;
using System.Collections;


public class ResourceDropOff : MonoBehaviour {

	public bool ResourceOne;
	public bool ResourceTwo;

	private RaceManager raceM;

	// Use this for initialization
	void Start () {
		raceM = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer;
		raceM.addDropOff (this.gameObject);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void dropOff(float one, float two)
	{
		raceM.updateResources (one, two, true);
	}




}
