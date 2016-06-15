using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public List<GameObject> levelIntros = new List<GameObject> ();
	public List<CampUpgradeManager> levelPresets = new List<CampUpgradeManager> ();

	public GameObject currentIntro;
	public Canvas Technology;
	public Canvas TechTree;

	public GameObject Vehicles;
	public GameObject Turrets;
	public GameObject Structures;

	private GameObject currentTech;
	// Use this for initialization
	void Awake () {
	
		foreach (GameObject obj in levelIntros) {
			obj.SetActive (false);
		}

		levelIntros [LevelData.currentLevel].SetActive (true);
		currentIntro = levelIntros [LevelData.currentLevel];
		currentTech = Vehicles;

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

}
