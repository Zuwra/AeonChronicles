

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
	gatling, rail, repair, mortar, construction, bunker, factory, Ult}


	Upgrade currentUpgrade;

	public List<upgradeType> myTypes = new List<upgradeType> ();

	public List<GameObject> unitsToUpgrade = new List<GameObject>();

	private bool justSetIndex;
	private int myIndex = 0;

	[System.Serializable]
	public struct UpgradesPiece{

		public string name;
		[TextArea(2,10)]
		public string description;
		public upgradeType myType;
		public bool unlocked;
		public Upgrade pointer;
		public Sprite pic;

		public void unlock()
		{unlocked = true;
			
		}

		public bool isUnlocked()
		{return unlocked;}

	}


	private CampUpgradeManager myManager;

	public void setUpgrade()
	{
			LevelData.applyUpgrade (this.gameObject.ToString (), myDropDown.value);
			

		//Debug.Log ("Getting set " + this.gameObject.ToString() + "  "+ myDropDown.value +"  size  " + myUpgrades.Count);
		theDescription.text = myUpgrades [myDropDown.value].description;
	

		myPic.sprite = myUpgrades [myDropDown.value].pic;

			if (currentUpgrade) {
				foreach(GameObject o in unitsToUpgrade)
				{
					currentUpgrade.unApplyUpgrade (o);
				}
			}
		currentUpgrade = myUpgrades [myDropDown.value].pointer;
			if (currentUpgrade) {
				foreach(GameObject o in unitsToUpgrade)
				{
					currentUpgrade.applyUpgrade (o);
				}
		}

	}

	public void setUpgrade(int n)
	{
		LevelData.applyUpgrade (this.gameObject.ToString (),n);



	}




	// Use this for initialization
	void Start () {
		myManager = GameObject.FindObjectOfType<LevelManager> ().levelPresets[LevelData.currentLevel];
		List<string> options = new List<string> ();
		foreach (UpgradesPiece  up in GameObject.FindObjectOfType<TrueUpgradeManager>().myUpgrades) {
			if (up.isUnlocked() && myTypes.Contains (up.myType)) {
				
				myUpgrades.Add (up);
				options.Add (up.name);
			
			}

		}
	
		myDropDown.AddOptions (options);
	

		myDropDown.RefreshShownValue ();
		//setUpgrade ();
		StartCoroutine (delayInit());

	}


	IEnumerator delayInit()
	{yield return new WaitForSeconds (.1f);


		theDescription.text = myUpgrades [myDropDown.value].description;

		myPic.sprite = myUpgrades [myDropDown.value].pic;

		currentUpgrade = myUpgrades [myDropDown.value].pointer;


	
		if (LevelData.appliedUpgrades != null) {

			List<string > keyList = new List<string > (LevelData.appliedUpgrades.Keys);
			foreach (string kv in keyList) {


				if (kv == this.gameObject.ToString ()) {
					//Debug.Log ("Setting the thing " + LevelData.appliedUpgrades[kv] + "   real size " + myUpgrades.Count);
					//i = LevelData.appliedUpgrades [kv];
					//myIndex = LevelData.appliedUpgrades [kv];
					myDropDown.value = LevelData.appliedUpgrades[kv];
					foreach(GameObject o in unitsToUpgrade)
					{
						currentUpgrade.unApplyUpgrade (o);
					}


				}
			}
			//setUpgrade (i);
		}
	
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void reInitialize()
	{
		List<string> options = new List<string> ();
		foreach (UpgradesPiece up in GameObject.FindObjectOfType<TrueUpgradeManager>().myUpgrades) {
			//Debug.Log ("Checking " + up.name);
			if (up.isUnlocked() && myTypes.Contains (up.myType) && !myUpgrades.Contains(up) && !options.Contains(up.name)) {//up.unlocked &&
				myUpgrades.Add (up);
				options.Add (up.name);

			}

		}

		myDropDown.AddOptions (options);

		//setUpgrade ();

	}
}
