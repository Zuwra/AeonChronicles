using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionMapManager : MonoBehaviour {


	LevelManager levelman;


	public List<Button> missionButtons;

	public List<int> firstLevelPulse;

	// Use this for initialization
	void Start () {


		levelman = GameObject.FindObjectOfType<LevelManager> ();

		int highestLevel = LevelData.getHighestLevel ();

		for (int i = 0; i < missionButtons.Count; i++) {
			if (i <= highestLevel) {
				missionButtons [i].interactable = (true);
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

	}

	public void toggleMissionMap()
	{
		GetComponent<Canvas> ().enabled = !GetComponent<Canvas> ().enabled;

	}
}
