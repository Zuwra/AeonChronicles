using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine.Serialization;
public class NewUnitPanel : MonoBehaviour {


	public List<NewUnit> units = new List<NewUnit> ();
	public GameObject nextButton;
	public GameObject prevButton;
	public Text myTitle;
	public Text mydescript;
	public Image myImage;
	public List<GameObject> ArsenalButtons;


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
		main = this;
		int LevelNum = GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber;
		LevelCompilation comp = ((GameObject)Resources.Load ("LevelEditor")).GetComponent<LevelCompilation> ();	
		if (comp.MyLevels [LevelNum].displayArsenal.tobeSeen.Count == 0) {
			foreach (GameObject obj in ArsenalButtons) {
				obj.SetActive (false);
			}
		} else {
		
			foreach (GameObject manage in  comp.MyLevels [LevelNum].displayArsenal.tobeSeen) {

				addUnitToList (manage);
		
			}

	

		
			previous ();
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
