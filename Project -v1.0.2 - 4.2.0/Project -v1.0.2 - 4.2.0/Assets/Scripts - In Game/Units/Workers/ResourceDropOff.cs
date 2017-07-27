using UnityEngine;
using System.Collections;


public class ResourceDropOff : MonoBehaviour {

	public bool ResourceOne;
	public bool ResourceTwo;

	private RaceManager raceM;

	// Use this for initialization
	void Start () {
		raceM = GameManager.main.activePlayer;
		raceM.addDropOff (this.gameObject);

	
	}
	


	public void dropOff(float one, float two)
	{
		raceM.updateResources (one, two, true);
	}




}
