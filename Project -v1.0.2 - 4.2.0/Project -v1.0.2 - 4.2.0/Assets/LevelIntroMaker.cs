using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroMaker : MonoBehaviour {

	public Image GeneralSprite;
	public Image LevelScenery;

	public Text LevelDescription;
	public List<Text> LevelTitles;


	public GameObject newUnitPrefab;
	public GameObject newUnitPanel;

	public Text Intelligence;
	public List<GameObject> newUnitIcons = new List<GameObject>();

	LevelInfo currentInfo;

	public TextChanger LoadingTip;

	public void LoadLevel(LevelInfo info)
	{
		currentInfo = info;
		GeneralSprite.sprite = info.GeneralPic;
		LevelScenery.sprite = info.ScenaryPic;
		int index = 0;
		if (info.getCompletionCount () > 0) {
			index = info.getCompletionCount () % info.Description.Count;
		}

		LevelDescription.text = info.Description [ index].LongDescription;
		foreach (Text t in LevelTitles) {
			t.text = info.LevelName;
		}

		newUnitPanel.SetActive (!(info.newUnits.Count == 0 && info.newAbilities.Count == 0));

		foreach (GameObject obj in newUnitIcons) {
			Destroy (obj);
		}
		newUnitIcons.Clear ();

		foreach (LevelInfo.NewThing thing in info.newUnits) {
			GameObject obj = (GameObject)Instantiate (newUnitPrefab, newUnitPanel.transform);
			obj.GetComponent<Image> ().sprite = thing.Icon;
			obj.transform.Find ("Title").GetComponent<Text> ().text = thing.thingName;
			obj.transform.Find ("Image").Find ("Text").GetComponent<Text> ().text = thing.thingDescrption;
			obj.transform.Find ("Image").gameObject.SetActive (false);
			newUnitIcons.Add (obj);
		}

		foreach (LevelInfo.NewThing thing in info.newAbilities) {
			GameObject obj = (GameObject)Instantiate (newUnitPrefab, newUnitPanel.transform);
			obj.GetComponent<Image> ().sprite = thing.Icon;
			obj.transform.Find ("Title").GetComponent<Text> ().text = thing.thingName;
			obj.transform.Find ("Image").Find ("Text").GetComponent<Text> ().text = thing.thingDescrption;
			obj.transform.Find ("Image").gameObject.SetActive (false);
			newUnitIcons.Add (obj);
		}


		Intelligence.text = "";
		foreach (LevelInfo.NewThing thing in info.Intelligence) {
			Intelligence.text += ("\n\n" +thing.thingDescrption);
		}

	}


	public void LoadLevel()
	{
		GameObject.FindObjectOfType<MissionManager> ().StartMission (currentInfo.SceneNumber);
		foreach (Text t in LevelTitles) {
			t.text = currentInfo.LevelName;
		}
			
		int i = Resources.Load<GameObject> ("LevelEditor").GetComponent<LevelCompilation>().MyLevels.IndexOf(currentInfo);
		if (PlayerPrefs.GetInt ("L" + i + "Win") == 0) {
			LoadingTip.loadLevelTip (currentInfo.defaultTip);
		} else {
			LoadingTip.setRandomTip ();
		}
	}

}
