using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaceUIManager : MonoBehaviour , ManagerWatcher{

	public RaceManager raceManager;

	Text resourceOne;
	Text resourceTwo;
	Text supply;
	string OneName;
	string TwoName;

	SelectedManager selectManager;
	




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

		resourceOne.text = OneName + "" + raceManager.ResourceOne;
		resourceTwo.text = TwoName + "" + raceManager.ResourceTwo;
		supply.text =  raceManager.currentSupply + "/" + raceManager.supplyMax;
	
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





	
	public void updateResources(float resOne, float resTwo){
		resourceOne.text = OneName + "   "+ resOne;
		resourceTwo.text = TwoName + "   "+ resTwo;
	}
	
	
	public void updateSupply( float current, float max){
		supply.text = current + "/" + max;
	}
	
	public void updateUpgrades(){

	}

	public void fOne()
	{Debug.Log ("Im being clicked");
		selectManager.selectAllArmy ();}
	
	public void fTwo()
	{
		selectManager.selectAllUnbound ();
	}
	
	public void fThree()
	{selectManager.selectIdleWorker ();}
	
	public void fFour()
	{}
	
	public void fFive()
	{selectManager.selectUnitOne ();}
	
	public void fSix()
	{selectManager.selectUnitTwo ();}
	
	public void fSeven()
	{selectManager.selectUnitThree ();}
	
	public void fEight()
	{selectManager.selectUnitFour ();}
	
	public void fNine()
	{}
	
	public void fTen()
	{}
	
	public void fEleven()
	{}
	
	public void fTwelve()
	{}



}
