using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CampUpgradeManager : MonoBehaviour {

	public GameObject TurretMenu;
	public GameObject vehicleMenu;
	public GameObject StructureMenu;
	public int creditAmount;
	public Text creditText;
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

		TurretMenu.SetActive (false);
		StructureMenu.SetActive (false);
		creditText.text = ""+creditAmount;
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
		creditAmount += input;
		creditText.text = ""+creditAmount;
	}



}
