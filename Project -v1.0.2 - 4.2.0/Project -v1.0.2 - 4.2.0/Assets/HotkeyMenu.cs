using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class HotkeyMenu : MonoBehaviour {


	public GameObject raceInfo;

	public GameObject grid;



	private List<GameObject> objectList;
	private SelectedManager selectMan;

	private List<GameObject> toggles = new List<GameObject> ();

	private FButtonManager fManager;

	// Use this for initialization
	void Start () {

		this.gameObject.SetActive (true);
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
			
			}
			n++;
		}



		if (selected [0].Count > 0) {
			fManager.Ffive.text = "(F5) " + selected [0] [0];
		} else {
			fManager.Ffive.text = "(F5) ";
		}

		if (selected [1].Count > 0) {
			fManager.Fsix.text = "(F6) " + selected [1] [0];
		} else {
			fManager.Fsix.text = "(F6) ";
		}

		if (selected [2].Count > 0) {
			fManager.fSeven.text = "(F7) " + selected [2] [0];
		} else {
			fManager.fSeven.text = "(F7) ";
		}

		if (selected [3].Count > 0) {
			fManager.fEight.text = "(F8) " + selected [3] [0];
		} else {
			fManager.fEight.text = "(F8) ";
		}
	



		selectMan.applyGlobalSelection(selected);


		}




	IEnumerator MyCoroutine ()
	{
		yield return new WaitForSeconds(.1f);


		fManager = GameObject.Find ("F-Buttons").GetComponent<FButtonManager>();

		selectMan = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
		foreach (RaceInfo info in raceInfo.GetComponents<RaceInfo>()) {

			if (info.race == GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.myRace) {

				objectList = new List<GameObject> ();


				foreach (GameObject obj in info.unitList) {
					objectList.Add (obj);
				}
				foreach (GameObject build in info.buildingList) {
					objectList.Add (build);
				}

			}
		}

		GameObject toggle = transform.FindChild ("UseIt").gameObject;
		GameObject name = transform.FindChild ("UnitName").gameObject;
		int n = 0;
		foreach (GameObject obj in objectList) {
			GameObject tempName = (GameObject)Instantiate (name, this.gameObject.transform.position, Quaternion.identity);
			tempName.transform.SetParent (grid.transform);
			tempName.GetComponent<Text> ().text = obj.GetComponent<UnitManager> ().UnitName;

			for (int i = 0; i < 4; i++) {

				GameObject tog = (GameObject)Instantiate (toggle, this.gameObject.transform.position, Quaternion.identity);
				tog.transform.SetParent (grid.transform);
				toggles.Add (tog);
				if (n != i) {
					tog.GetComponent<Toggle> ().isOn = false;

				}
			}

			n++;
		}




		apply ();
		this.gameObject.GetComponent<Canvas> ().enabled = false;
	}
	// Update is called once per frame
	void Update () {
	
	}

}
