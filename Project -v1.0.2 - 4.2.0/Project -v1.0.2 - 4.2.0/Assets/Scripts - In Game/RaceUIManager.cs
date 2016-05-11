using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaceUIManager : MonoBehaviour , ManagerWatcher{

	public RaceManager raceManager;

	Text resourceOne;
	Text resourceTwo;
	Text supply;
	string OneName;
	string TwoName;

	SelectedManager selectManager;
	public Dropdown production;


	private GameObject currentProdManager;
	public List<GameObject> dropdowns = new List<GameObject>();




	// Use this for initialization
	void Start () {
		selectManager = GameObject.Find ("Manager").GetComponent<SelectedManager>();

		if (raceManager == null) {

			raceManager = GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<GameManager>().activePlayer;
		}
		raceManager.addWatcher (this);
		OneName = raceManager.OneName;
		TwoName = raceManager.TwoName;

	
		resourceOne = this.gameObject.transform.FindChild ("Resources").FindChild("ResourceOne").GetComponent<Text>();
		resourceTwo = this.gameObject.transform.FindChild ("Resources").FindChild("ResourceTwo").GetComponent<Text>();
		supply = this.gameObject.transform.FindChild ("Resources").FindChild("Supply").GetComponent<Text>();

		if (OneName != "") {
			resourceOne.text = OneName + "  " + raceManager.ResourceOne;
		} else {
			resourceOne.text = "";
		}

		if (TwoName != "") {
			resourceTwo.text = TwoName + "  " + raceManager.ResourceTwo;
		}
		else {
			resourceTwo.text = "";
		}

		if ( raceManager.currentSupply < raceManager.supplyMax - 5 ||  raceManager.supplyMax == raceManager.supplyCap) {
			supply.color = Color.green;
		} else if ( raceManager.currentSupply >= raceManager.supplyMax - 1) {
			supply.color = Color.red;
		} else {
			supply.color = Color.yellow;
		}
		supply.text =  raceManager.currentSupply + "/" + raceManager.supplyMax;
		currentProdManager = dropdowns [1];
		chanageDropDown ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.F1)) {
			fOne();
		}
		else if (Input.GetKeyDown (KeyCode.F2)) {
			fTwo ();	
		}
		else if (Input.GetKeyDown (KeyCode.F3)) {
			fThree();
		}
		else if (Input.GetKeyDown (KeyCode.F4)) {
			fFour();
		}
		else if (Input.GetKeyDown (KeyCode.F5)) {
			fFive();
		}
		else if (Input.GetKeyDown (KeyCode.F6)) {
			fSix();
		}
		else if (Input.GetKeyDown (KeyCode.F7)) {
			fSeven();
		}
		else if (Input.GetKeyDown (KeyCode.F8)) {
			fEight ();
		}
		
		else if (Input.GetKeyDown (KeyCode.F9)) {
			fNine();
		}
		else if (Input.GetKeyDown (KeyCode.F10)) {
			fTen();
		}
		else if (Input.GetKeyDown (KeyCode.F11)) {
			fEleven();
		}
		else if (Input.GetKeyDown (KeyCode.F12)) {
			fTwelve();
		}

	
	}



	public void chanageDropDown()
	{
		if (production.value == 0) {
			currentProdManager.SetActive (false);
			currentProdManager = dropdowns [0];
			currentProdManager.SetActive (true);
		}
		else if (production.value == 1) {
			currentProdManager.SetActive (false);
			currentProdManager = dropdowns [1];
			currentProdManager.SetActive (true);

		}
		else if (production.value == 2) {
			currentProdManager.SetActive (false);
			currentProdManager = dropdowns [2];
			currentProdManager.SetActive (true);

		}
		else{
			currentProdManager.SetActive (false);
		}
	}

	public void changeUnits()
	{
	}

	public void changeEconomy()
	{
	}


	
	public void updateResources(float resOne, float resTwo){

		if (OneName != "") {
			resourceOne.text = OneName + "  " + resOne;
		}
		if (TwoName != "") {
			
			resourceTwo.text = TwoName + "  " + resTwo;
		}
	}
	
	
	public void updateSupply( float current, float max){
		if (current < max - 5||  raceManager.supplyMax == raceManager.supplyCap) {
			supply.color = Color.green;
		} else if (current >= max - 1) {
			supply.color = Color.red;
		} else {
			supply.color = Color.yellow;
		}
		supply.text = current + "/" + max;
	}
	
	public void updateUpgrades(){

	}

	public void fOne()
	{selectManager.globalSelect(0);}
	
	public void fTwo()
	{selectManager.globalSelect(1);}
	
	public void fThree()
	{selectManager.globalSelect(2);}
	
	public void fFour()
	{selectManager.globalSelect(3);}
	
	public void fFive()
	{selectManager.selectAllArmy ();}
	
	public void fSix()
	{selectManager.selectIdleWorker ();}
	
	public void fSeven()
	{selectManager.selectAllBuildings ();}
	
	public void fEight()
	{selectManager.selectAllUnbound ();}
	
	public void fNine()
	{raceManager.useAbilityOne ();}
	
	public void fTen()
	{raceManager.useAbilityTwo ();}
	
	public void fEleven()
	{raceManager.useAbilityThree ();}
	
	public void fTwelve()
	{raceManager.useAbilityFour ();}



}
