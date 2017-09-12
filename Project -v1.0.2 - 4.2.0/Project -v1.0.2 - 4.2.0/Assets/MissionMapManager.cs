using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionMapManager : MonoBehaviour {


	LevelManager levelman;


	public List<Button> missionButtons;

	public List<int> firstLevelPulse;
	public GameObject MainScreen;

	[Tooltip("Should have three pic in here, bronze, silver, gold")]
	public List<Sprite> DifficultyPics;

	// Use this for initialization
	void Start () {


		levelman = GameObject.FindObjectOfType<LevelManager> ();

		int highestLevel = LevelData.getHighestLevel ();

		for (int i = 0; i < missionButtons.Count; i++) {
			if (i <= highestLevel) { 
				missionButtons [i].interactable = (true);
				if(PlayerPrefs.GetInt ("L" + i + "Dif", -1) > -1){
					missionButtons [i].GetComponent<Image> ().sprite = DifficultyPics [PlayerPrefs.GetInt ("L" + i + "Dif", -1)];}
			}
				else{
				missionButtons [i].interactable = (false);
				missionButtons [i].GetComponentInChildren<Text> ().enabled = false;
				missionButtons [i].GetComponent<ToolTip> ().enabled = false;
			}
			if ( i > highestLevel +1) {
				missionButtons [i].gameObject.SetActive (false);
			
			}

			if (PlayerPrefs.GetInt ("L" + i+"Win", 0) == 0 && highestLevel >=firstLevelPulse[i]) {
				
				missionButtons [i].GetComponent<UIAddons.PulseEffect> ().isPulsing = true;
			}
		
		}

	}



	public void loadMissionIntro(int num)
	{
		levelman.openLevelIntro (num);
		toggleMissionMap (false);
		MainScreen.SetActive (false);

	}

	public void toggleMissionMap(bool onOrOff)
	{
		GetComponent<Canvas> ().enabled = onOrOff;

		MainScreen.SetActive (!onOrOff);

	}
}
