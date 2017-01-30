using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaceTipManager : MonoBehaviour {

	public Dropdown raceDropdown;

	public GameObject racePacket;
	private List<RaceInfo> raceList = new List<RaceInfo>();

	public Image graph;
	public Text summary;
	public Text playAs;
	public Text playAgainst;



	// Use this for initialization
	void Start () {


		foreach (RaceInfo info in racePacket.GetComponents<RaceInfo>()) {
			raceList.Add (info);
		}

		loadRace (0);
	
	}
	



	public void switchList()
	{
		loadRace(raceDropdown.value);


	}

	private void loadRace(int index)
	{

		summary.text = raceList [index].summary;
		playAs.text = raceList [index].playingAs;
		playAgainst.text = raceList [index].playingAgainst;
		graph.material = raceList [index].PowerGraph;
	}


}
