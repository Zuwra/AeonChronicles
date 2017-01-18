﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewAchievmentUI : MonoBehaviour {



	public AchievementManager AchievementMan;
	public GameObject AchievePanel;
	public GameObject NewTitle;

	void Start()
	{
		NewTitle.SetActive (false);
		foreach (Achievement ach in AchievementMan.myAchievements) {
			if (ach.IsAccomplished () && !ach.HasBeenRewarded ()) {
				ach.Reward ();

				GameObject.FindObjectOfType<LevelManager> ().changeMoney (ach.TechReward);

				GameObject obj = (GameObject)Instantiate (AchievePanel, this.transform);
				obj.transform.FindChild ("Title").GetComponent<Text> ().text = ach.Title;
				obj.transform.FindChild ("Description").GetComponent<Text> ().text = ach.GetDecription();
				obj.transform.FindChild ("Icon").GetComponent<Image> ().sprite = ach.myIcon;
				obj.transform.localScale = new Vector3 (1, 1, 1);

				if (ach.TechReward > 0) {
					obj.transform.FindChild ("RewardDescription").GetComponent<Text> ().text = 
						"+ " + ach.TechReward + " Tech Credits";
					obj.transform.FindChild ("RewardPic").GetComponent<Image> ().enabled = true;
				} else {
					obj.transform.FindChild ("RewardDescription").GetComponent<Text> ().text = 
						"New Voice Pack!";
					obj.transform.FindChild ("RewardHelp").GetComponent<Text> ().enabled = true;
				}
				NewTitle.SetActive (true);
			}

		}
	}
}
