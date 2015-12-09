using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {



	public RaceManager[] playerList;
	public int playerNumber;
	public RaceManager activePlayer;
	private RaceManager playerOne;
	private RaceManager playerTwo;



	// Use this for initialization
	void Start () {

		playerList = new RaceManager[GetComponents<RaceManager>().Length];

		foreach(RaceManager race in GetComponents<RaceManager> ())
		{Debug.Log("initializing");
			playerList[race.playerNumber-1] = race;
			}

		activePlayer = playerList [playerNumber - 1];
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}




}

