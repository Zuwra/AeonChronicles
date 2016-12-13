using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CampUpgradeManager : MonoBehaviour {



	private GameObject currentMenu;

	[System.Serializable]
	public struct UnitsUnlocked{

		public string name;
		public bool unlocked;
		public GameObject UIPart;

	}


	public List<UnitsUnlocked> myUnits= new List<UnitsUnlocked>();


	// Use this for initialization
	void Start () {

		foreach (UnitsUnlocked unit in myUnits) {
			unit.UIPart.SetActive (unit.unlocked);
		
		}



	}
	


	public void SetMenu(GameObject obj)
	{
		if (currentMenu != null) {
			currentMenu.SetActive (false);
		}
		currentMenu = obj;
		obj.SetActive (true);


	}


}
