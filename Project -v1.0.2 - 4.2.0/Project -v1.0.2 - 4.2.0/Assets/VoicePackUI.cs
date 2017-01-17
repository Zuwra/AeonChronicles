using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VoicePackUI : MonoBehaviour {

	public VoiceContainer myPacks;
	public AchievementManager myAchievements;
	public Dropdown myDropdown;


	// Use this for initialization
	void Start () {
		foreach (Achievement ach in myAchievements.myAchievements) {
			if (ach.IsAccomplished () && ach.TechReward < 0) {
				foreach (VoicePack vp in myPacks.LockedVoicePacks) {
					if (vp.UnlockNumber == ach.TechReward) {
						myDropdown.AddOptions (new List<string>{vp.voicePackName});
					}
				}

			}
		
		}
		myDropdown.value = PlayerPrefs.GetInt ("VoicePack", 0);
	}
	

}
