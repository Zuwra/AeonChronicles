using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CampUpgradeManager : MonoBehaviour {

	public Canvas TurretMenu;
	public Canvas vehicleMenu;
	public Canvas StructureMenu;


	public List<Text> creditDisplayers = new List<Text> ();

	private GameObject currentMenu;

	[System.Serializable]
	public struct UnitsUnlocked{

		public string name;
		public bool unlocked;
		public GameObject UIPart;

	}

	public List<CampaignUpgrade.UpgradesPiece> myUpgrades= new List<CampaignUpgrade.UpgradesPiece>();

	public List<UnitsUnlocked> myUnits= new List<UnitsUnlocked>();


	// Use this for initialization
	void Start () {

		foreach (UnitsUnlocked unit in myUnits) {
			unit.UIPart.SetActive (unit.unlocked);
		
		}

		TurretMenu.enabled = false;
		StructureMenu.enabled = false;
		vehicleMenu.enabled = false;
		changeMoney (10);
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMenu(GameObject obj)
	{
		if (currentMenu != null) {
			currentMenu.SetActive (false);
		}
		currentMenu = obj;
		obj.SetActive (true);


	}

	public void changeMoney(int input)
	{
		
		LevelData.totalXP += input;
		foreach (Text t in creditDisplayers) {
			t.text = ""+LevelData.totalXP ;
		}

		Debug.Log ("money " + LevelData.totalXP);
	}



}
