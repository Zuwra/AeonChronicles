using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroButton : MonoBehaviour {

	public int LevelIndex;
	public Text TitleText;
	public Text OtherText;

	public Text DescriptionText;
	public Image SceneryPic;
	public Image completedPic;



	// Use this for initialization
	void Start () {
		LevelCompilation myComp = Resources.Load<LevelCompilation> ("LevelEditor");
		TitleText.text = myComp.MyLevels [LevelIndex].LevelName;
		SceneryPic.sprite = myComp.MyLevels [LevelIndex].ScenaryPic;

		int descriptionIndex = 0;
		if (myComp.MyLevels [LevelIndex].getCompletionCount () > 0) {
			descriptionIndex =  myComp.MyLevels [LevelIndex].getCompletionCount () % myComp.MyLevels [LevelIndex].Description.Count;
		}

		DescriptionText.text = myComp.MyLevels [LevelIndex].Description [descriptionIndex].ShortDescription;
		OtherText.text = myComp.MyLevels [LevelIndex].LevelName;


		if (myComp.MyLevels [LevelIndex].getCompletionCount () == 0) {
			completedPic.GetComponent<UIAddons.PulseEffect> ().isPulsing = true;
		} else {
			MissionMapManager mapper =  GameObject.FindObjectOfType<MissionMapManager> ();
			completedPic.sprite = mapper.DifficultyPics [Mathf.Max( PlayerPrefs.GetInt ("L" + LevelIndex + "Dif", 0), 0)];
		
		}
		if (LevelIndex == 0) {
			return;
		}

		foreach (LevelInfo info in myComp.MyLevels) {
			if (info.getCompletionCount () > 0) {
				if (info.UnlockOnWin.Contains (LevelIndex)) {
					return;
				}
			}
		}
	


		gameObject.SetActive (false);
	}

}
