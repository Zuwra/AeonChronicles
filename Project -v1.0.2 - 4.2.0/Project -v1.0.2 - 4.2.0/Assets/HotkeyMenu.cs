using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class HotkeyMenu : MonoBehaviour {


	public GameObject raceInfo;
	public Dropdown fFive;
	public Dropdown fSix;
	public Dropdown fSeven;
	public Dropdown fEight;


	private Text fFiveBut;
	private Text fSixBut;

	private Text fSevenBut;

	private Text fEightBut;

	private List<string> objectList;
	private SelectedManager selectMan;

	// Use this for initialization
	void Start () {


		fFiveBut = GameObject.Find ("GameHud").GetComponentInChildren<FButtonManager> ().Ffive;
		fSixBut = GameObject.Find ("GameHud").GetComponentInChildren<FButtonManager> ().Fsix;
		fSevenBut = GameObject.Find ("GameHud").GetComponentInChildren<FButtonManager> ().fSeven;
		fEightBut = GameObject.Find ("GameHud").GetComponentInChildren<FButtonManager> ().fEight;


		selectMan = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
		foreach (RaceInfo info in raceInfo.GetComponents<RaceInfo>()) {
			
			if (info.race == GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.myRace) {

				objectList = new List<string> ();

				foreach (GameObject build in info.buildingList) {
					objectList.Add (build.name);
				}

				foreach (GameObject obj in info.unitList) {
					objectList.Add (obj.name);
				}

				fFive.AddOptions (objectList);
				fSix.AddOptions (objectList);
				fSeven.AddOptions (objectList);
				fEight.AddOptions (objectList);

			}
		}
		selectMan.sUnitOne = objectList [0];
		selectMan.sUnitTwo = objectList [1];
		selectMan.sUnitThree = objectList [2];
		selectMan.sUnitFour = objectList [3];


		fFive.value = 0;
		fSix.value = 1;
		fSeven.value = 2;
		fEight.value = 3;
	

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setFive()
	{
		selectMan.sUnitOne = objectList [fFive.value];
		fFiveBut.text =  "(F5) All " + objectList [fFive.value];
	}

	public void setSix()
	{
		selectMan.sUnitTwo = objectList [fSix.value];
		fSixBut.text = "(F6) All " +objectList [fSix.value];
	}

	public void setSeven()
	{
		selectMan.sUnitThree = objectList [fSeven.value];
		fSevenBut.text = "(F7) All " +objectList [fSeven.value];
	}

	public void setEight()
	{
		selectMan.sUnitFour = objectList [fEight.value];
		fEightBut.text = "(F8) All " +objectList [fEight.value];
	}
}
