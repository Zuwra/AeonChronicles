using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectmanager : MonoBehaviour {


	public List<Button> levelButtons;
	// Use this for initialization
	void Start () {

		for (int i = 0; i < levelButtons.Count; i++) {
			if (i <= LevelData.getHighestLevel()) {
				levelButtons [i].interactable = true;
			} else {
				levelButtons [i].interactable = false;
			}
		}

	
	}

	public void resetProgress()
	{PlayerPrefs.DeleteAll ();

		LevelData.reset ();
		for (int i = 0; i < levelButtons.Count; i++) {
			if (i <= LevelData.getHighestLevel()) {
				levelButtons [i].interactable = true;
			} else {
				levelButtons [i].interactable = false;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
