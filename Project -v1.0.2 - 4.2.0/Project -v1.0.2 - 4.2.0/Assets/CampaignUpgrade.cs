

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CampaignUpgrade : MonoBehaviour {


	public Text theDescription;
	public List<UpgradesPiece> myUpgrades = new List<UpgradesPiece>();
	public Text Title;
	public bool unlocked = true;

	public List<Button> myButtons;
	public List<Image> myPic;

	public enum upgradeType{Shields, Barrels, Speed, Concussion, Siege, Mount, Construction, Recoil, Ability, Deployment, DuplexPlating, DoublePHD,General,Munition}

	public Material grayScale;
	SpecificUpgrade currentUpgrade;

	public List<upgradeType> myTypes = new List<upgradeType> ();

	public List<GameObject> unitsToUpgrade = new List<GameObject>();

	private bool justSetIndex;

	public int currentIndex;
	public List<StatDisplayer> myStatDisplayer;

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
		grayScale = Resources.Load<Material>("GrayScaleUI");
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
		for (int i = 0; i < options.Count; i++) {
			if ( options[i] == upGradeName) {
				currentIndex = i;
				setUpgrade (currentIndex);
			}
		}
		//myDropDown.value = PlayerPrefs.GetString(this.gameObject.ToString (), "Basic Engineering");
		SetImageDescript ();
		foreach (StatDisplayer stat in myStatDisplayer) {
			stat.SetText ();
		}
		this.gameObject.SetActive (false);
	}


	public void setUpgrade(int index)
	{//Debug.Log("UpgradeSet " + index + "  " +this.gameObject);
		if (myButtons.Count > 0) {

			myButtons [currentIndex].image.material = grayScale;
		}
		currentIndex = index;

		theDescription.text = myUpgrades [currentIndex].description;
		if (myButtons.Count > 0) {
			myButtons [currentIndex].image.material = null;
		}
		if (currentUpgrade) {
			foreach (GameObject o in unitsToUpgrade) {
				currentUpgrade.unitsToApply.Remove(o.GetComponent<UnitManager>().UnitName);
			}
		}



		PlayerPrefs.SetString (this.gameObject.ToString (), options[currentIndex]);
		LevelData.applyUpgrade (this.gameObject.ToString (), currentIndex);
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

		foreach (StatDisplayer stat in myStatDisplayer) {
			stat.SetText ();
		}


		GameObject.FindObjectOfType<TrueUpgradeManager> ().Unused ();			


	}


	public void upgradeBought()
	{
		setDropDownOptions ();

		string upGradeName = PlayerPrefs.GetString(this.gameObject.ToString (), "Basic Engineering");
		for (int i = 0; i < options.Count; i++) {
			if ( options[i] == upGradeName) {
				currentIndex = i;
				setUpgrade (currentIndex);
			}
		}
		GameObject.FindObjectOfType<TrueUpgradeManager> ().Unused ();	
	
		SetImageDescript ();
	}

	void OnDisable()
	{
		if (TrueUpgradeManager.instance) {
			TrueUpgradeManager.instance.Unused ();
		}

	}


	public void SetImageDescript()
	{
		theDescription.text = myUpgrades [currentIndex].description;
		if (myUpgrades.Count > 0) {
			Title.text = myUpgrades [currentIndex].name;
		}
		foreach (Image i in myPic) {
			i.sprite = myUpgrades [currentIndex].pic;

			CampTooltip tip = i.GetComponent<CampTooltip> ();
			if (tip) {
				tip.helpText = myUpgrades [currentIndex].description;
				tip.Title = myUpgrades [currentIndex].name;
			
			}
		}
		currentUpgrade = myUpgrades [currentIndex].pointer;
	}





	public void setInitialStuff()
	{
		setDropDownOptions ();

		SetImageDescript ();



		string upGradeName = PlayerPrefs.GetString(this.gameObject.ToString (), "Basic Engineering");
		for (int i = 0; i <options.Count; i++) {
			if( options [i] == upGradeName) {
				currentIndex = i;
				setUpgrade (currentIndex);

			}
		}

		this.gameObject.SetActive (false);

	}
	List<string> options = new List<string> ();
	public void setDropDownOptions()
	{
		myUpgrades.Clear ();
		options = new List<string> ();
		foreach (UpgradesPiece  up in GameObject.FindObjectOfType<TrueUpgradeManager>().myUpgrades) {

			if (up.isUnlocked() && myTypes.Contains (up.myType) && !myUpgrades.Contains(up)) {

				if (myButtons.Count > options.Count) {
			
					myButtons [options.Count].gameObject.SetActive (true);//.interactable = true;//.gameObject.SetActive (true);
					myButtons[options.Count].image.material = grayScale;
					myButtons [options.Count].image.sprite = up.pic;
				}
				myUpgrades.Add (up);
				options.Add (up.name);

			}

		}

		for (int i = options.Count; i < Mathf.Min( 6,myButtons.Count); i++) {
			myButtons[options.Count].image.material = grayScale;
			myButtons[i].gameObject.SetActive(false);
		}
	}


	public float changeText(string name, float f)
	{
		if (currentUpgrade) {
			return currentUpgrade.ChangeString (name, f);
		}
		return f;
	}

	public string addText(string name, string AddOn)
	{
		if (currentUpgrade) {
			return currentUpgrade.AddString (name, AddOn);
		}
		return AddOn;
	}


}
