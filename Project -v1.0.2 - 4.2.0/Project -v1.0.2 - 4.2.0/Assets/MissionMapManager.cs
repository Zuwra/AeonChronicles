using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionMapManager : MonoBehaviour {


	LevelManager levelman;


	public List<Button> missionButtons;
	public List<GameObject> LineConnecters;


	// Use this for initialization
	void Start () {
		levelman = GameObject.FindObjectOfType<LevelManager> ();


		for (int i = 0; i < missionButtons.Count; i++) {
			missionButtons [i].interactable = (i <= Mathf.Min(3, LevelData.getHighestLevel()));
		}
		for (int i = 0; i < LineConnecters.Count; i++) {
			LineConnecters [i].SetActive(i <= LevelData.getHighestLevel()-2);
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
