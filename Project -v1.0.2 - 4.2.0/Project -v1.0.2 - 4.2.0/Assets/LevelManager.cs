using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public List<GameObject> levelIntros = new List<GameObject> ();
	public List<CampUpgradeManager> levelPresets = new List<CampUpgradeManager> ();

	public GameObject currentIntro;
	public Canvas Technology;
	public Canvas TechTree;
	public Canvas UltTree;

	public Button techButton;
	public Button UltButton;
	public GameObject Vehicles;
	public GameObject Turrets;
	public GameObject Structures;

	private GameObject currentTech;
	// Use this for initialization
	void Awake () {

		foreach (GameObject obj in levelIntros) {
			obj.SetActive (false);
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
			techButton.interactable = false;
			UltButton.interactable = false;
		}
		if (!LevelData.ComingFromLevel) {
			LevelData.totalXP = PlayerPrefs.GetInt ("TechAmount");
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleVehicle()
	{currentTech.SetActive (false);
		currentTech = Vehicles;
		Vehicles.SetActive (true);
	}

	public void ToggleStruct()
	{currentTech.SetActive (false);
		currentTech = Structures;
		Structures.SetActive (true);
	}

	public void ToggleTurret()
	{currentTech.SetActive (false);
		currentTech = Turrets;
		Turrets.SetActive (true);
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
