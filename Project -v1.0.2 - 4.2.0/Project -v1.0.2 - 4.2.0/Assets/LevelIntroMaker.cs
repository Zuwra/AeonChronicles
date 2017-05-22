using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroMaker : MonoBehaviour {

	public Image GeneralSprite;
	public Image LevelScenery;

	public Text LevelDescription;
	public Text LevelTitle;


	public void LoadLevel(LevelInfo info)
	{
		GeneralSprite.sprite = info.GeneralPic;
		LevelScenery.sprite = info.ScenaryPic;

		LevelDescription.text = info.Description [0].LongDescription;
		LevelTitle.text = info.LevelName;
	
	}


}
