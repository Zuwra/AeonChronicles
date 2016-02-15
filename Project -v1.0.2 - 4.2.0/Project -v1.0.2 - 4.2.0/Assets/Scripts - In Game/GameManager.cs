using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {



	public RaceManager[] playerList = new RaceManager[3];
	public int playerNumber;
	public RaceManager activePlayer;
	private RaceManager playerOne;
	private RaceManager playerTwo;

	private bool initialized = false;



	// Use this for initialization
	void Awake () {

	//	playerList = new RaceManager[GetComponents<RaceManager>().Length];
		if(!initialized)
		foreach(RaceManager race in GetComponents<RaceManager> ())
		{playerList[race.playerNumber-1] = race;}

		//activePlayer = playerList [playerNumber - 1];
	

	}


	public void initialize()
		{
		if (!initialized) {
			initialized = true;
			playerList = new RaceManager[3];
			foreach(RaceManager race in GetComponents<RaceManager> ())
			{Debug.Log (race.playerNumber -1);
				playerList[race.playerNumber-1] = race;}
			
			activePlayer = playerList [playerNumber - 1];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public RaceManager getActivePlayer()
	{
		return activePlayer;
	}





}

