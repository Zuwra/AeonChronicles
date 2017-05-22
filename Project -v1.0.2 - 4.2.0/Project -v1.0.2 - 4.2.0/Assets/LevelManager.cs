using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	AudioSource mySource;
	public AudioClip buttonPress;

	public GameObject MainScreen;

	public List<GameObject> levelIntros = new List<GameObject> ();
	public List<GameObject> Expositions = new List<GameObject> ();

	public List<Dropdown> difficultyBars = new List<Dropdown> ();
	public List<string> levelNames;
	public List<Button> levelButtons = new List<Button> ();
	public GameObject currentIntro;
	public Canvas Technology;
	public Canvas TechTree;
	public Canvas UltTree;
 
	public List<Text> creditDisplayers = new List<Text> ();

	public Button techButton;
	public Button UltButton;
	public Canvas Vehicles;
	public Canvas Turrets;
	public Canvas Structures;

	private Canvas currentTech;
	public GameObject LevelSelector;

	public GameObject defaultStructure;
	public GameObject defaultVehicle;
	public GameObject defaultTurret;

	public VoiceContainer voicePacks;
	public Text ReplayButtonText;

	public LevelIntroMaker IntroMaker;

	public static LevelManager main;
	// Use this for initialization
	void Awake () {
		mySource = GetComponent<AudioSource> ();
		main = this;
		foreach (GameObject obj in levelIntros) {
		//	obj.SetActive (false);
		}

		foreach (GameObject ob in Expositions) {
			ob.SetActive (false);
		}
		Debug.Log (" current level " + LevelData.getHighestLevel());
		for (int i = 0; i < levelButtons.Count; i++) {
			levelButtons [i].interactable = (i <= Mathf.Min(3, LevelData.getHighestLevel()));
		}

		ReplayButtonText.text = "Replay Previous:\n" + levelNames [PlayerPrefs.GetInt ("RecentLevel")];

		//levelIntros [LevelData.currentLevel].SetActive (true);
		if (levelIntros.Count > 0) {
			currentIntro = levelIntros [ Mathf.Min(3, LevelData.getHighestLevel())];
		
		currentTech = Vehicles;
			if (LevelData.getHighestLevel() == 0) {
			techButton.interactable = false;
			UltButton.interactable = false;
			} else if (LevelData.getHighestLevel() == 1) {
			techButton.interactable = true;
			UltButton.interactable = false;
		} else {
			techButton.interactable = true;
			UltButton.interactable = true;
		}
		

		}

		Turrets.enabled = false;
		Structures.enabled = false;
		Vehicles.enabled = false;

		setDifficultyDropDowns (LevelData.getDifficulty());

		changeMoney (0);

	}



	public void nextLevel()
	{
		
		//Debug.Log ("Being called");
		Expositions [LevelData.getHighestLevel()].SetActive (true);

	}



	public void closeLevelIntro()
	{
		currentIntro.GetComponent<Canvas> ().enabled = false;// .SetActive (false);
		//MainScreen.SetActive (true);

		GameObject.FindObjectOfType<MissionMapManager> ().toggleMissionMap (true);

		foreach (ToolTip tt in GameObject.FindObjectsOfType<ToolTip>()) {
			tt.toolbox.enabled = false;
		}


	}

	public void openLevelIntro(int n)
	{
		levelIntros [0].GetComponent<Canvas> ().enabled = true; //.SetActive (true);
		IntroMaker.LoadLevel (LevelCompilation.getLevelInfo ().MyLevels [n]);
		currentIntro = levelIntros [0];
		MainScreen.SetActive (false);
		//currentIntro.SetActive (!currentIntro.activeSelf );
	}

	public void ToggleVehicle()
	{currentTech.enabled = false;
		currentTech = Vehicles;
		Vehicles.enabled = true;
		GameObject.FindObjectOfType<CampTechCamManager> ().loadTech (defaultVehicle);
	}

	public void ToggleStruct()
	{currentTech.enabled = false;
		currentTech = Structures;
		Structures.enabled = true;
		GameObject.FindObjectOfType<CampTechCamManager> ().loadTech (defaultStructure);
	}

	public void ToggleTurret()
	{Debug.Log ("Toggling Turet");
		currentTech.enabled = false;
		currentTech = Turrets;
		Turrets.enabled = true;
		GameObject.FindObjectOfType<CampTechCamManager> ().loadTech (defaultTurret);
	}


	public void showMainScreen()
	{


	}


	public void ToggleTech()
	{	mySource.PlayOneShot (buttonPress);
		Technology.gameObject.SetActive (!Technology.gameObject.activeSelf);//.enabled = !Technology.enabled;
		LevelSelector.SetActive(!LevelSelector.activeSelf);
		if (LevelSelector.activeSelf) {
			GameObject.FindObjectOfType<CampTechCamManager> ().returnToStart ();
		}
		//currentIntro.SetActive (!currentIntro.activeSelf );
	}

	public void toggleTechTree()
	{mySource.PlayOneShot (buttonPress);
		//Technology.enabled = !Technology.enabled;
		TechTree.enabled = !TechTree.enabled;
	}

	public void toggleUltTree()
	{mySource.PlayOneShot (buttonPress);
		//Technology.enabled = !Technology.enabled;
		UltTree.enabled = !UltTree.enabled;
	}

	public void setDifficulty(Dropdown i)
	{
		if (Time.timeSinceLevelLoad > 1) {
			mySource.PlayOneShot (buttonPress);
		}
		LevelData.setDifficulty (i.value + 1);
		setDifficultyDropDowns (i.value);
	
	
	}

	public void setDifficultyDropDowns(int i )
	{
		foreach (Dropdown dd in difficultyBars) {
			if (dd.value!= i-1) {
				dd.value = i-1;
			}
		}
	}


	public void changeMoney(int input)
	{

		LevelData.addMoney (input);
		foreach (Text t in creditDisplayers) {
			t.text = "" + LevelData.getMoney ();
		}

		//Debug.Log ("money " + LevelData.totalXP);
	}

	public void setAnnouncer(Dropdown i)
	{
		for (int j = 0; j < voicePacks.LockedVoicePacks.Count; j++) {
			if(i.options[i.value].text == voicePacks.LockedVoicePacks[j].voicePackName ){
					
				PlayerPrefs.SetInt ("VoicePack", j);

				if (Time.timeSinceLevelLoad > 2) {
					mySource.PlayOneShot (voicePacks.LockedVoicePacks [j].getVoicePackLine ());
				}
			}
		}
	}

}
