

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CampaignUpgrade : MonoBehaviour {


	public Text theDescription;
	public Dropdown myDropDown;
	public List<UpgradesPiece> myUpgrades = new List<UpgradesPiece>();

	public List<Image> myPic;

	public enum upgradeType{general, vehicle, tank, structure, turret, munition, coyote, tortoise, OreProcessor, Hornet, 
	gatling, rail, repair, mortar, construction, bunker, factory, Ult, DoubleUpgrade}


	SpecificUpgrade currentUpgrade;

	public List<upgradeType> myTypes = new List<upgradeType> ();

	public List<GameObject> unitsToUpgrade = new List<GameObject>();

	private bool justSetIndex;
	//private int myIndex = 0;

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


	//private CampUpgradeManager myManager;

	public void setUpgrade()
	{
		
		LevelData.applyUpgrade (this.gameObject.ToString (), myDropDown.value);
			
		GameObject.FindObjectOfType<TrueUpgradeManager> ().playSound ();
		GameObject.FindObjectOfType<CampTechCamManager> ().AssignTechEffect ();
		//Debug.Log ("Getting set " + this.gameObject.ToString() + "  "+ myDropDown.value +"  size  " + myUpgrades.Count);

		if (myUpgrades.Count > myDropDown.value) {
			theDescription.text = myUpgrades [myDropDown.value].description;


			foreach (Image i in myPic) {
				i.sprite = myUpgrades [myDropDown.value].pic;
			}

			if (currentUpgrade) {
				foreach (GameObject o in unitsToUpgrade) {
					//Debug.Log ("Removing +" + o);
					currentUpgrade.unitsToApply.Remove(o.GetComponent<UnitManager>().UnitName);
					//currentUpgrade.unApplyUpgrade (o);
				}
			}
			currentUpgrade = myUpgrades [myDropDown.value].pointer;

			//Debug.Log ("Setting description to " + theDescription.text + "   " + currentUpgrade + "   "  + unitsToUpgrade.Count);
			if (currentUpgrade) {
				foreach (GameObject o in unitsToUpgrade) {
					//Debug.Log ("Adding " + o + " to " + currentUpgrade);
					//currentUpgrade.applyUpgrade (o);
					currentUpgrade.unitsToApply.Add(o.GetComponent<UnitManager>().UnitName);
				}
			}
		}
	}

	public void setUpgrade(int n)
	{
		LevelData.applyUpgrade (this.gameObject.ToString (),n);

	}




	// Use this for initialization
	void Start () {

	
		List<string> options = new List<string> ();
		foreach (UpgradesPiece  up in GameObject.FindObjectOfType<TrueUpgradeManager>().myUpgrades) {

			if (up.isUnlocked() && myTypes.Contains (up.myType) && !myUpgrades.Contains(up)) {
		
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

		foreach (Image i in myPic) {
			i.sprite = myUpgrades [myDropDown.value].pic;
		}
		currentUpgrade = myUpgrades [myDropDown.value].pointer;

	

		foreach (LevelData.keyValue kv in LevelData.getsaveInfo().appliedUpgrades) {
	
			if (kv.theName == this.gameObject.ToString ()) {
		
					myDropDown.value = kv.index;
					/*foreach(GameObject o in unitsToUpgrade)
						{if (currentUpgrade) {
						Debug.Log ("removing " + o + "   from " + currentUpgrade);
						//currentUpgrade.unitsToApply.Remove (o.GetComponent<UnitManager>().UnitName);
							//currentUpgrade.unApplyUpgrade (o);
						}
					}*/


				}

			//setUpgrade (i);
		}
	
		//this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void reInitialize()
	{
		List<string> options = new List<string> ();
		foreach (UpgradesPiece up in GameObject.FindObjectOfType<TrueUpgradeManager>().myUpgrades) {
		//	Debug.Log ("Checking " + up.name);
			if (myTypes.Contains (up.myType) && !myUpgrades.Contains(up) && !options.Contains(up.name)) {//up.unlocked &&
				myUpgrades.Add (up);
				options.Add (up.name);

			}

		}

		myDropDown.AddOptions (options);

		//setUpgrade ();

	}


	public void setInitialStuff()
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


		theDescription.text = myUpgrades [myDropDown.value].description;

		foreach (Image i in myPic) {
			i.sprite = myUpgrades [myDropDown.value].pic;
		}
		currentUpgrade = myUpgrades [myDropDown.value].pointer;



		foreach (LevelData.keyValue kv in LevelData.getsaveInfo().appliedUpgrades) {

			if (kv.theName == this.gameObject.ToString ()) {

				myDropDown.value = kv.index;
				/*foreach(GameObject o in unitsToUpgrade)
						{if (currentUpgrade) {
						Debug.Log ("removing " + o + "   from " + currentUpgrade);
						//currentUpgrade.unitsToApply.Remove (o.GetComponent<UnitManager>().UnitName);
							//currentUpgrade.unApplyUpgrade (o);
						}
					}*/


			}

			//setUpgrade (i);
		}

		this.gameObject.SetActive (false);

	}
}
