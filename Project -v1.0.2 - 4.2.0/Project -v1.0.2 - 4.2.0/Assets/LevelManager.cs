using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public List<GameObject> levelIntros = new List<GameObject> ();
	public List<GameObject> Expositions = new List<GameObject> ();
	public List<CampUpgradeManager> levelPresets = new List<CampUpgradeManager> ();

	public GameObject currentIntro;
	public Canvas Technology;
	public Canvas TechTree;
	public Canvas UltTree;

	public Button techButton;
	public Button UltButton;
	public Canvas Vehicles;
	public Canvas Turrets;
	public Canvas Structures;

	private Canvas currentTech;

	public static LevelManager main;
	// Use this for initialization
	void Awake () {
		main = this;
		foreach (GameObject obj in levelIntros) {
			obj.SetActive (false);
		}

		foreach (GameObject ob in Expositions) {
			ob.SetActive (false);
		}


		levelPresets [LevelData.currentLevel].enabled = true;


		levelIntros [LevelData.currentLevel].SetActive (true);
		currentIntro = levelIntros [LevelData.currentLevel];
		currentTech = Vehicles;
		if (LevelData.currentLevel == 0) {
			techButton.interactable = false;
			UltButton.interactable = false;
		} else if (LevelData.currentLevel == 1) {
			techButton.interactable = true;
			UltButton.interactable = false;
		} else {
			techButton.interactable = true;
			UltButton.interactable = true;
		}
		if (!LevelData.ComingFromLevel) {
			LevelData.totalXP = PlayerPrefs.GetInt ("TechAmount");
		}



		if (!LevelData.ComingFromLevel) {
			nextLevel ();
			//Debug.Log ("calling this");
		}


	}
	public void nextLevel()
	{
		
		//Debug.Log ("Being called");
		Expositions [LevelData.currentLevel].SetActive (true);

	}


	// Update is called once per frame
	void Update () {
	


	}

	public void ToggleVehicle()
	{currentTech.enabled = false;
		currentTech = Vehicles;
		Vehicles.enabled = true;
	}

	public void ToggleStruct()
	{currentTech.enabled = false;
		currentTech = Structures;
		Structures.enabled = true;
	}

	public void ToggleTurret()
	{currentTech.enabled = false;
		currentTech = Turrets;
		Turrets.enabled = true;
	}


	public void showMainScreen()
	{


	}


	public void ToggleTech()
	{
		Technology.enabled = !Technology.enabled;
		currentIntro.SetActive (!currentIntro.activeSelf );
	}

	public void toggleTechTree()
	{
		Technology.enabled = !Technology.enabled;
		TechTree.enabled = !TechTree.enabled;
	}

	public void toggleUltTree()
	{
		Technology.enabled = !Technology.enabled;
		UltTree.enabled = !UltTree.enabled;
	}

	public void setDifficulty(Dropdown i)
	{if(i.value == 0)
		{LevelData.easyMode = true;}
		else
	{LevelData.easyMode = false;}

	}

}
