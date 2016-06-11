using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour {

	public Canvas myCanvas;
	public Text timeDisplay;
	public Text enemyDisplay;
	public Text allyDisplay;
	public Text objDisplay;
	public Text techDisplay;
	public Text ResourceTitle;
	public Text ResourceDisplay;

	// Use this for initialization
	void Start () {
		if (LevelData.myLevels.Count > 0) {
			SetResults (LevelData.myLevels [LevelData.currentLevel]);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void SetResults(LevelData.levelInfo info)
	{
		MissionManager.main.toggleVictory ();
		//myCanvas.enabled = true;
		timeDisplay.text = ""+info.time;
		enemyDisplay.text =""+ info.EnemiesDest;
		allyDisplay.text = ""+info.unitsLost;
		objDisplay.text = ""+info.bonusObj;
		techDisplay.text = ""+info.TechCredits;

		if (info.Resources > 0) {
			ResourceTitle.text = "Resources Collected: ";
			ResourceDisplay.text = "" + info.Resources;
		} else {
			ResourceTitle.text = "";
			ResourceDisplay.text = "";
		}
	}
		


}
