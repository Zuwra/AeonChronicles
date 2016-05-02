

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CampaignUpgrade : MonoBehaviour {


	public Text theDescription;
	public Dropdown myDropDown;
	public List<UpgradesPiece> myUpgrades = new List<UpgradesPiece>();

	public Image myPic;

	public enum upgradeType{general, vehicle, tank, structure, turret, munition, coyote, tortoise, OreProcessor, Hornet, 
	gatling, rail, repair, mortar, construction, bunker, factory}


	public List<upgradeType> myTypes = new List<upgradeType> ();

	[System.Serializable]
	public struct UpgradesPiece{

		public string name;
		[TextArea(2,10)]
		public string description;
		public upgradeType myType;
		public bool unlocked;
		public Upgrade pointer;
		public Sprite pic;

	}


	private CampUpgradeManager myManager;

	public void setUpgrade()
	{
		if (myUpgrades.Count > 0) {
			theDescription.text = myUpgrades [myDropDown.value].description;
	
			myPic.sprite = myUpgrades [myDropDown.value].pic;

		}
	}


	// Use this for initialization
	void Start () {
		myManager = GameObject.FindObjectOfType<CampUpgradeManager> ();
		List<string> options = new List<string> ();
		foreach (UpgradesPiece up in myManager.myUpgrades) {
			if (up.unlocked && myTypes.Contains (up.myType)) {
				myUpgrades.Add (up);
				options.Add (up.name);
			
			}

		}

		myDropDown.AddOptions (options);
	
		setUpgrade ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
