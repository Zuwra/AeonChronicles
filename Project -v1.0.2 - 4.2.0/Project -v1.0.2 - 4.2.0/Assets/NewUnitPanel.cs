using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine.Serialization;
public class NewUnitPanel : MonoBehaviour {


	public bool activeOnStart;
	public List<NewUnit> units = new List<NewUnit> ();
	public GameObject nextButton;
	public GameObject prevButton;
	public Text myTitle;
	public Text mydescript;
	public Image myImage;


	private int index =0;

	[System.Serializable]
	public struct NewUnit{
		//public Color myColor;
		[TextArea(2,10)]
		public string Title;
		[TextArea(2,10)]
		public string Description;
		public Sprite myPic;


	}

	public static NewUnitPanel main;
	List<string> usedNames = new List<string>();
	// Use this for initialization
	IEnumerator Start () {
		yield return null;
		units.Clear ();


		foreach (WaveManager manage in GameObject.FindObjectsOfType<WaveManager>()) {
			foreach (GameObject unitType in	manage.getCurrentWaveType().waveRampUp[manage.getCurrentWaveType().waveRampUp.Count -1].waveType) {
				addUnitToList (unitType);
			}
			foreach (GameObject unitType in	manage.getCurrentWaveType().waveRampUp[manage.getCurrentWaveType().waveRampUp.Count -1].mediumExtra) {
				addUnitToList (unitType);
			}
			foreach (GameObject unitType in	manage.getCurrentWaveType().waveRampUp[manage.getCurrentWaveType().waveRampUp.Count -1].HardExtra) {
				addUnitToList (unitType);
			}
		
		}

		foreach (KeyValuePair<string, List<UnitManager>> obj in GameManager.main.playerList[1].getUnitList()) {
			if (obj.Value.Count > 0) {
				//Debug.Log ("Checking " + obj.Key);
				//if (obj.Value [0].getUnitStats ().isUnitType (UnitTypes.UnitTypeTag.Static_Defense)) {
					addUnitToList (obj.Value [0].gameObject);
				//}
			}
		}


		main = this;
		previous ();

		if (activeOnStart) {
			GetComponent<Canvas> ().enabled = true;
			}
	}


	public void addUnitToList(GameObject unitType)
	{
		UnitManager unitsManager = unitType.GetComponent<UnitManager> ();

		if (usedNames.Contains (unitsManager.UnitName)) {
			return;
		}
		UnitStats stats = unitType.GetComponent<UnitStats> ();
		usedNames.Add (unitsManager.UnitName);
		NewUnit newunit = new NewUnit ();
		newunit.Description = stats.UnitDescription;
		newunit.myPic = stats.Icon;
		newunit.Title = unitsManager.UnitName;
		units.Add (newunit);
	}


	public void next()
	{index++;
		if ( index ==  units.Count - 1) {

			nextButton.SetActive (false);
		} else if (index == units.Count) {
			index--;
		}
		if (index > 0) {
			prevButton.SetActive (true);
		}
		loadUnit (index);
	}

	public void previous()
	{
		index--;
		if (index != units.Count-1 && units.Count > 1 ) {
	
			nextButton.SetActive (true);
		}
		if (index == -1) {
			index = 0;
		}
		if (index ==0) {
			prevButton.SetActive (false);
		}
		loadUnit (index);
	}

	public void loadUnit(int i)
	{
		//myTitle.color = units [i].myColor;
		myTitle.text = units [i].Title;
		mydescript.text = units [i].Description;
		myImage.sprite = units [i].myPic;
		index = i;
	}



	public void exit()
	{Time.timeScale = GameSettings.gameSpeed;
		GetComponent<Canvas> ().enabled = false;

	}




}
