using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroMaker : MonoBehaviour {

	public Image GeneralSprite;
	public Image LevelScenery;

	public Text LevelDescription;
	public Text LevelTitle;

	public GameObject newUnitPrefab;
	public GameObject newUnitPanel;

	public Text Intelligence;
	List<GameObject> newUnitIcons = new List<GameObject>();

	public void LoadLevel(LevelInfo info)
	{
		GeneralSprite.sprite = info.GeneralPic;
		LevelScenery.sprite = info.ScenaryPic;

		LevelDescription.text = info.Description [0].LongDescription;
		LevelTitle.text = info.LevelName;
	

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


}
