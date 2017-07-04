using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	public RaceManager[] playerList = new RaceManager[3];
	public int playerNumber;
	public RaceManager activePlayer;
	private RaceManager playerOne;
	private RaceManager playerTwo;

	private bool initialized = false;
	public static GameManager main;
	public 	List<MiniMapUIController> MiniMaps;

	public static GameManager getInstance()
	{
		if (main == null) {
			main = GameObject.FindObjectOfType<GameManager>();
		}
		return main;
	}

	// Use this for initialization
	void Awake () {
		main = this;

		foreach (MiniMapUIController min in MiniMaps) {
			if (min) {
				min.DubAwake ();
			}
		}

	}

	void Start()
	{
		foreach (MiniMapUIController min in MiniMaps) {
			if (min) {
				min.Initialize ();
			}
		}
	}


	public void initialize()
		{
		if (!initialized) {
			initialized = true;
		}
	}

	public RaceManager getActivePlayer()
	{
		return activePlayer;
	}





}

