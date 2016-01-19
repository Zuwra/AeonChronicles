using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
public class UiAbilityManager : MonoBehaviour {



	public List<GameObject> UIButtons = new List<GameObject>();


	private int currentX;
	private int currentY;

	public List<GameObject> Stats = new List<GameObject> ();
	private SelectedManager selectMan;

	// Use this for initialization
	void Start () {
	
		selectMan = GameObject.Find ("Manager").GetComponent<SelectedManager>();
	}
	
	// Update is called once per frame
	void Update () {




	
	}




	public void loadUI(Page uiPage)
	{
		foreach (GameObject obj in Stats) {
			obj.GetComponent<StatsUI> ().clear ();
		}

		int n = 0;

		foreach (GameObject obj in UIButtons) {
			obj.transform.FindChild ("QButton").gameObject.SetActive (false);
			obj.transform.FindChild ("WButton").gameObject.SetActive (false);
			obj.transform.FindChild ("EButton").gameObject.SetActive (false);
			obj.transform.FindChild ("RButton").gameObject.SetActive (false);
		}



		for(int j = 0; j < 3; j ++){
			if (uiPage.rows [j] == null) {
				continue;
			}

			n = uiPage.rows[j][0].AbilityStartingRow;

			//Sets the unit's stats and count
			UnitManager man = uiPage.rows[j][0].gameObject.GetComponent<UnitManager> ();
			Stats[n].GetComponent<StatsUI> ().loadUnit (uiPage.rows[j][0].gameObject.GetComponent<UnitStats> (), uiPage.rows[j][0].gameObject.GetComponent<IWeapon> (), 
				uiPage.rows[j].Count, man.UnitName);

			int AbilityX = 0;
	
			for (int m = 0; m < man.abilityList.Count / 4 +1; m++) {
				for (int i = 1; i < 5; i++) {

					if(man.abilityList.Count > AbilityX * 4){
						Transform trans = UIButtons [n].transform.FindChild ("QButton");
						trans.gameObject.SetActive (true);
						trans.GetComponent<Image> ().material = man.abilityList [0 + AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [0 + AbilityX * 4];

						Text charger = trans.FindChild ("Charge1").GetComponent<Text> ();
						if (man.abilityList [AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [0 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
						}
					}
			
					if(man.abilityList.Count >1+( AbilityX * 4)){
						Transform trans = UIButtons [n].transform.FindChild ("WButton");
						trans.gameObject.SetActive (true);
						trans.GetComponent<Image> ().material = man.abilityList [1 + AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [1 + AbilityX * 4];

						Text charger = trans.FindChild ("Charge2").GetComponent<Text> ();
						if (man.abilityList [1 + AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [1 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
						}

					}
					if(man.abilityList.Count > 2+(AbilityX * 4)){
						Transform trans = UIButtons [n].transform.FindChild ("EButton");
						trans.gameObject.SetActive (true);
						trans.GetComponent<Image> ().material = man.abilityList [2 + AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [2 + AbilityX * 4];

						Text charger = trans.FindChild ("Charge3").GetComponent<Text> ();
						if (man.abilityList [2 + AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [2 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
						}

					}

					if(man.abilityList.Count >3+( AbilityX * 4)){
						Transform trans = UIButtons [n].transform.FindChild ("RButton");
						trans.gameObject.SetActive (true);
						trans.GetComponent<Image> ().material = man.abilityList [3 + AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [3 + AbilityX * 4];

						Text charger = trans.FindChild ("Charge4").GetComponent<Text> ();
						if (man.abilityList [3 + AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [3 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
						}

					}
			
			
				}
				AbilityX++;
				n++;
			}
			}

		}



	public void updateUI(Page uiPage)
	{


		int n = 0;



		for(int j = 0; j < 3; j ++){
			if (uiPage.rows [j] == null) {
				continue;
			}

			n = uiPage.rows[j][0].AbilityStartingRow;

			//Sets the unit's stats and count
			UnitManager man = uiPage.rows[j][0].gameObject.GetComponent<UnitManager> ();
			Stats[n].GetComponent<StatsUI> ().loadUnit (uiPage.rows[j][0].gameObject.GetComponent<UnitStats> (), uiPage.rows[j][0].gameObject.GetComponent<IWeapon> (), 
				uiPage.rows[j].Count, man.UnitName);

			int AbilityX = 0;

			for (int m = 0; m < man.abilityList.Count / 4 +1; m++) {
				for (int i = 1; i < 5; i++) {

					if(man.abilityList.Count > AbilityX * 4){
						Transform trans = UIButtons [n].transform.FindChild ("QButton");

						trans.GetComponent<Image> ().material = man.abilityList [0 + AbilityX * 4].iconPic;
				

						Text charger = trans.FindChild ("Charge1").GetComponent<Text> ();
						if (man.abilityList [AbilityX * 4].chargeCount > -1) {
							
							charger.text =  ""+man.abilityList [ AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
						}
					}

					if(man.abilityList.Count >1+( AbilityX * 4)){
						Transform trans = UIButtons [n].transform.FindChild ("WButton");

						trans.GetComponent<Image> ().material = man.abilityList [1 + AbilityX * 4].iconPic;
					

						Text charger = trans.FindChild ("Charge2").GetComponent<Text> ();
						if (man.abilityList [1 + AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [1 + AbilityX * 4].chargeCount;

						} else {
							charger.text = "";
						}

					}
					if(man.abilityList.Count > 2+(AbilityX * 4)){
						Transform trans = UIButtons [n].transform.FindChild ("EButton");
					
						trans.GetComponent<Image> ().material = man.abilityList [2 + AbilityX * 4].iconPic;


						Text charger = trans.FindChild ("Charge3").GetComponent<Text> ();
						if (man.abilityList [2 + AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [2 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
						}

					}

					if(man.abilityList.Count >3+( AbilityX * 4)){
						Transform trans = UIButtons [n].transform.FindChild ("RButton");

						trans.GetComponent<Image> ().material = man.abilityList [3 + AbilityX * 4].iconPic;


						Text charger = trans.FindChild ("Charge4").GetComponent<Text> ();
						if (man.abilityList [3 + AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [3 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
						}

					}


				}
				AbilityX++;
				n++;
			}
		}


	}
		





	public void callAbilityOne()
	{selectMan.callAbility (0);}

	public void callAbilityTwo()
	{selectMan.callAbility (1);}


	public void callAbilityThree()
	{selectMan.callAbility (2);}


	public void callAbilityFour()
	{selectMan.callAbility (3);}

	public void callAbilityFive()
	{selectMan.callAbility (4);}

	public void callAbilitySix()
	{selectMan.callAbility (5);}

	public void callAbilitySeven()
	{selectMan.callAbility (6);}

	public void callAbilityEight()
	{selectMan.callAbility (7);}

	public void callAbilityNine()
	{selectMan.callAbility (8);}

	public void callAbilityTen()
	{selectMan.callAbility (9);}

	public void callAbilityEleven()
	{selectMan.callAbility (10);}

	public void callAbilityTwelve()
	{selectMan.callAbility (11);}




}
