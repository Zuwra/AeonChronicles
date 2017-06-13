

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CampaignUpgrade : MonoBehaviour {


	public Text theDescription;
	public Dropdown myDropDown;
	public List<UpgradesPiece> myUpgrades = new List<UpgradesPiece>();

	public List<Image> myPic;

	public enum upgradeType{Shields, Barrels, Speed, Concussion, Siege, Mount, Construction, Recoil, Ability, Deployment, DuplexPlating, DoublePHD,General,Munition}


	SpecificUpgrade currentUpgrade;

	public List<upgradeType> myTypes = new List<upgradeType> ();

	public List<GameObject> unitsToUpgrade = new List<GameObject>();

	private bool justSetIndex;

	[System.Serializable]
	public class UpgradesPiece{

		public string name;
		[TextArea(2,10)]
		public string description;
		public upgradeType myType;
		public bool unlocked;
		public SpecificUpgrade pointer;
		public Sprite pic;

		public void unlock()
		{unlocked = true;
			
		}

		public bool isUnlocked()
		{return unlocked;}

	}

	// Use this for initialization
	void Start () {

		StartCoroutine (delayInit());
	}


	IEnumerator delayInit()
	{

		if (myTypes.Contains(upgradeType.Munition)) {
			yield return new WaitForSeconds (.1f);
		} else {
			yield return new WaitForSeconds (.2f);
		}
	
		setDropDownOptions ();

		string upGradeName = PlayerPrefs.GetString(this.gameObject.ToString (), "Basic Engineering");
		for (int i = 0; i < myDropDown.options.Count; i++) {
			if (myDropDown.options [i].text == upGradeName) {
				myDropDown.value = i;
			}
		}
		//myDropDown.value = PlayerPrefs.GetString(this.gameObject.ToString (), "Basic Engineering");
		SetImageDescript ();
		this.gameObject.SetActive (false);
	}


	public void setUpgrade()
	{

		if (currentUpgrade) {
			foreach (GameObject o in unitsToUpgrade) {
				currentUpgrade.unitsToApply.Remove(o.GetComponent<UnitManager>().UnitName);
			}
		}


		PlayerPrefs.SetString (this.gameObject.ToString (), myDropDown.options[myDropDown.value].text);
		LevelData.applyUpgrade (this.gameObject.ToString (), myDropDown.value);
		GameObject.FindObjectOfType<TrueUpgradeManager> ().playSound ();
		GameObject.FindObjectOfType<CampTechCamManager> ().AssignTechEffect ();

		SetImageDescript ();
		if (currentUpgrade) {
			foreach (GameObject o in unitsToUpgrade) {
				if (o.GetComponent<UnitManager> ()) {
					currentUpgrade.unitsToApply.Add (o.GetComponent<UnitManager> ().UnitName);
				}
			}
		}
			
	}



	public void SetImageDescript()
	{
		theDescription.text = myUpgrades [myDropDown.value].description;

		foreach (Image i in myPic) {
			i.sprite = myUpgrades [myDropDown.value].pic;

			CampTooltip tip = i.GetComponent<CampTooltip> ();
			if (tip) {
				tip.helpText = myUpgrades [myDropDown.value].description;
				tip.Title = myUpgrades [myDropDown.value].name;
			
			}
		}
		currentUpgrade = myUpgrades [myDropDown.value].pointer;
	}


	public void reInitialize()
	{
		setDropDownOptions ();

	}


	public void setInitialStuff()
	{
		setDropDownOptions ();

		SetImageDescript ();



		string upGradeName = PlayerPrefs.GetString(this.gameObject.ToString (), "Basic Engineering");
		for (int i = 0; i < myDropDown.options.Count; i++) {
			if (myDropDown.options [i].text == upGradeName) {
				myDropDown.value = i;
			}
		}
		myDropDown.Select ();
		myDropDown.RefreshShownValue ();


		this.gameObject.SetActive (false);

	}

	public void setDropDownOptions()
	{

		
		List<string> options = new List<string> ();
		foreach (UpgradesPiece  up in GameObject.FindObjectOfType<TrueUpgradeManager>().myUpgrades) {

			if (up.isUnlocked() && myTypes.Contains (up.myType) && !myUpgrades.Contains(up)) {

				myUpgrades.Add (up);
				options.Add (up.name);

			}

		}

		myDropDown.AddOptions (options);
		myDropDown.RefreshShownValue ();
	}
}
