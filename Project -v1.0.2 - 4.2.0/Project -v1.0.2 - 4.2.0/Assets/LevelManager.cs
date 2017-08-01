using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	AudioSource mySource;
	public AudioClip buttonPress;

	public GameObject MainScreen;

	public GameObject levelIntros;
	public List<GameObject> Expositions = new List<GameObject> ();

	public Dropdown difficultyBars;

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


		foreach (GameObject ob in Expositions) {
			ob.SetActive (false);
		}
		//Debug.Log (" current level " + LevelData.getHighestLevel());
		//Debug.Log ("Num is " +PlayerPrefs.GetInt ("RecentLevel") + "  " + LevelCompilation.getLevelInfo().ls.Count );
		//ReplayButtonText.text = "Replay Previous:\n" + LevelCompilation.getLevelInfo().ls[PlayerPrefs.GetInt ("RecentLevel")].LevelName;


			currentTech = Vehicles;
		if (LevelData.getHighestLevel() == 0) {
			techButton.interactable = false;
			UltButton.interactable = false;
			} 
		else if (LevelData.getHighestLevel() == 1) {
			techButton.interactable = true;
			UltButton.interactable = false;
		} else {
			techButton.interactable = true;
			UltButton.interactable = true;
		}
		

		

		Turrets.enabled = false;
		Structures.enabled = false;
		Vehicles.enabled = false;

		difficultyBars.value = LevelData.getDifficulty () - 1;
	
		changeMoney (0);

	}



	public void nextLevel()
	{
		
		//Debug.Log ("Being called");
		Expositions [LevelData.getHighestLevel()].SetActive (true);

	}



	public void closeLevelIntro()
	{
		levelIntros.GetComponent<Canvas> ().enabled = false;// .SetActive (false);
		//MainScreen.SetActive (true);

		GameObject.FindObjectOfType<MissionMapManager> ().toggleMissionMap (true);

		foreach (ToolTip tt in GameObject.FindObjectsOfType<ToolTip>()) {
			if (tt.toolbox) {
				tt.toolbox.enabled = false;
			} else {
				tt.ToolObj.SetActive (false);
			}
		}


	}

	public void openLevelIntro(int n)
	{
		levelIntros.GetComponent<Canvas> ().enabled = true; //.SetActive (true);
		LevelCompilation comp = Resources.Load<GameObject> ("LevelEditor").GetComponent<LevelCompilation> ();
		IntroMaker.LoadLevel (comp.MyLevels[n]);
		MainScreen.SetActive (false);
	
	}


	public void ToggleVehicle()
	{
		if (currentTech) {
			currentTech.enabled = false;
		}

		currentTech = Vehicles;
		Vehicles.enabled = true;

		GameObject.FindObjectOfType<CampTechCamManager> ().loadTech (defaultVehicle);
	}

	public void ToggleStruct()
	{
		if (currentTech) {
			currentTech.enabled = false;
		}

		currentTech = Structures;
		Structures.enabled = true;

		GameObject.FindObjectOfType<CampTechCamManager> ().loadTech (defaultStructure);

	}

	public void ToggleTurret()
	{
		if (currentTech) {
			currentTech.enabled = false;
		}
		currentTech = Turrets;
		Turrets.enabled = true;

		GameObject.FindObjectOfType<CampTechCamManager> ().loadTech (defaultTurret);
	}


	public void MainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

	public void showMainScreen()
	{


	}


	public void ToggleTech()
	{	mySource.PlayOneShot (buttonPress);
		Technology.enabled = (!Technology.enabled);//.enabled = !Technology.enabled;'

		currentTech.enabled = Technology.enabled;
		LevelSelector.GetComponent<Canvas>().enabled = !LevelSelector.GetComponent<Canvas>().enabled;

		if (LevelSelector.GetComponent<Canvas> ().enabled) {
			GameObject.FindObjectOfType<CampTechCamManager> ().returnToStart ();
		} 

	}

	public void toggleTechTree()
	{mySource.PlayOneShot (buttonPress);
		//Technology.enabled = !Technology.enabled;
		TechTree.enabled = !TechTree.enabled;
	}

	public void toggleUltTree()
	{mySource.PlayOneShot (buttonPress);
		//Technology.enabled = !Technology.enabled;
		CanvasGroup grouper = UltTree.GetComponent<CanvasGroup>();
		if (grouper.interactable) {
			grouper.interactable = false;
			grouper.alpha = 0;
			grouper.blocksRaycasts = false;
		
		} else {
			grouper.interactable = true;
			grouper.alpha = 1;
			grouper.blocksRaycasts = true;
		}
		UltTree.enabled = !UltTree.enabled;
	}

	public void setDifficulty(Dropdown i)
	{
		if (Time.timeSinceLevelLoad > 1) {
			mySource.PlayOneShot (buttonPress);
		}
		LevelData.setDifficulty (i.value + 1);
		//setDifficultyDropDowns (i.value);
	
	
	}


	public void changeMoney(int input)
	{

		LevelData.addMoney (input);
		foreach (Text t in creditDisplayers) {
			if (t) {
				t.text = "" + LevelData.getMoney ();
			}
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
