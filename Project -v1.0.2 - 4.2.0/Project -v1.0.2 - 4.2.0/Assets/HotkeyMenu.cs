using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HotkeyMenu : MonoBehaviour {


	public GameObject raceInfo;

	public GameObject grid;

	public Text GroupOne;

	public Text GroupTwo;

	public Text GroupThree;

	public Text GroupFour;


	public List<Image> myPics;



	private List<GameObject> objectList;
	private SelectedManager selectMan;

	private List<GameObject> toggles = new List<GameObject> ();

//	private FButtonManager fManager;

	// Use this for initialization
	void Start () {

		selectMan = GameObject.FindObjectOfType<SelectedManager> ();
		this.gameObject.GetComponent<Canvas> ().enabled = true;
		StartCoroutine(MyCoroutine());
	
	}




	public void apply()
		{
		List<List<string>> selected = new List<List<string>> ();

		for (int i = 0; i < 4; i++) {
			selected.Add (new List<string> ());
		}

		int n = 0;
		foreach (GameObject obj in toggles) {
			
			if (obj.GetComponent<Toggle>().isOn) {

				selected [n % 4].Add (objectList[(int) n /4].GetComponent<UnitManager> ().UnitName);
				myPics [n % 4].sprite = objectList [(int)n / 4].GetComponent<UnitStats> ().Icon;
			
			}
			n++;
		}

		string toSave = "";
		GroupOne.text = "Select all:\n";
		foreach (string s in selected[0]) {
			GroupOne.text += s +"s, " ;
			toSave += s;
		}

		toSave +=";";
		GroupTwo.text = "Select all:\n";
		foreach (string s in selected[1]) {
			GroupTwo.text += s +"s, ";
			toSave += s;
		}
		toSave +=";";

		GroupThree.text = "Select all:\n";
		foreach (string s in selected[2]) {
			GroupThree.text += s +"s, ";
			toSave += s;
		}
		toSave +=";";

		GroupFour.text = "Select all:\n";
		foreach (string s in selected[3]) {
			GroupFour.text += s + "s, ";
			toSave += s;
		}
		toSave +=";";

		// Change this when future levels are added.
		PlayerPrefs.SetString ("FHotkey"+ Mathf.Min(3, VictoryTrigger.instance.levelNumber), toSave);

		selectMan.applyGlobalSelection(selected);


		}


	IEnumerator MyCoroutine ()
	{
		yield return new WaitForSeconds(.05f);

		char[] separator = {';'};
		//fManager = GameObject.Find ("F-Buttons").GetComponent<FButtonManager>();
		string loaded = PlayerPrefs.GetString("FHotkey"+ Mathf.Min(3, VictoryTrigger.instance.levelNumber), "");
		string[] separated = loaded.Split (separator,System.StringSplitOptions.RemoveEmptyEntries);

	
			selectMan = GameObject.FindObjectOfType<SelectedManager> ();
			foreach (RaceInfo info in raceInfo.GetComponents<RaceInfo>()) {

				if (info.race == GameManager.main.activePlayer.myRace) {

					objectList = new List<GameObject> ();


					foreach (GameObject obj in info.unitList) {
						objectList.Add (obj);
					}

					foreach (GameObject t in info.attachmentsList) {
						objectList.Add (t);
					}
					foreach (GameObject build in info.buildingList) {
						objectList.Add (build);
					}


				}
			}

			GameObject toggle = transform.Find ("UseIt").gameObject;
			GameObject name = transform.Find ("UnitName").gameObject;
			int n = 0;
			foreach (GameObject obj in objectList) {
				GameObject tempName = (GameObject)Instantiate (name, this.gameObject.transform.position, Quaternion.identity);
				tempName.transform.SetParent (grid.transform);
	
				tempName.GetComponent<Text> ().text = obj.GetComponent<UnitManager> ().UnitName;

				for (int i = 0; i < 4; i++) {

					GameObject tog = (GameObject)Instantiate (toggle, this.gameObject.transform.position, Quaternion.identity);
					tog.transform.SetParent (grid.transform);
					toggles.Add (tog);
				if (loaded == "" && n != i) {
					tog.GetComponent<Toggle> ().isOn = false;

				} else if (separated.Length == 4 && !separated [i].Contains (obj.GetComponent<UnitManager> ().UnitName)) {
					tog.GetComponent<Toggle> ().isOn = false;
				}
				}

				n++;
			}




		apply ();

		this.gameObject.GetComponent<Canvas> ().enabled = false;
	}

}
