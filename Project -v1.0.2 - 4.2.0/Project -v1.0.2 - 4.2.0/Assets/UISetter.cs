using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetter : MonoBehaviour {


	public Button CommandMinimize;
	public Button LeftMinimize;
	public Button RightMinimize;
	public GameObject ResourceBar;

	public List<Image> minimapPics;
	public Text LevelTitle;

	// Use this for initialization
	void Start () {

		int LevelNum = GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber;
		LevelCompilation comp = ((GameObject)Resources.Load ("LevelEditor")).GetComponent<LevelCompilation> ();	

		if (!comp.MyLevels [LevelNum].UIBarsNUlts.CommandsOpen) {
			CommandMinimize.onClick.Invoke ();
		}

		if (!comp.MyLevels [LevelNum].UIBarsNUlts.LeftBarOpen) {
			LeftMinimize.onClick.Invoke ();
		}

		if (!comp.MyLevels [LevelNum].UIBarsNUlts.RightBarOpen) {
			RightMinimize.onClick.Invoke ();
		}

		if (!comp.MyLevels [LevelNum].UIBarsNUlts.resourcesOpen) {
			ResourceBar.SetActive (false);
		}

		RaceManager racer = GameObject.FindObjectOfType<GameManager> ().playerList [0];
		racer.ResourceOne = comp.MyLevels [LevelNum].startingMoney;

		int NumOfUlts = 4;
		if (!comp.MyLevels [LevelNum].UIBarsNUlts.UltOneActivated) {
			racer.UltOne.active = false;
			racer.ultBOne.gameObject.SetActive (false);
			NumOfUlts--;
		}

		if (!comp.MyLevels [LevelNum].UIBarsNUlts.UltTwoActivated) {
			racer.UltTwo.active = false;
			racer.ultBTwo.gameObject.SetActive (false);
			NumOfUlts--;
		}

		if (!comp.MyLevels [LevelNum].UIBarsNUlts.UltThreeActivated) {
			racer.UltThree.active = false;
			racer.ultBThree.gameObject.SetActive (false);
			NumOfUlts--;
		}

		if (!comp.MyLevels [LevelNum].UIBarsNUlts.UltFourActivated) {
			racer.UltFour.active = false;
			racer.ultBFour.gameObject.SetActive (false);
			NumOfUlts--;
		}

		if (NumOfUlts ==0) {
			racer.ultBFour.gameObject.transform.parent.gameObject.SetActive (false);
		}

		foreach (Image im in minimapPics) {
			im.sprite = comp.MyLevels [LevelNum].MinimapPic;
		}


		LevelTitle.text = comp.MyLevels [LevelNum].LevelName;
	}

}
