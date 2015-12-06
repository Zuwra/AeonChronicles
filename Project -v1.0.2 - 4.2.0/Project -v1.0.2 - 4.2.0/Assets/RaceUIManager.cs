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





	// Use this for initialization
	void Start () {

		if (raceManager == null) {

			raceManager = GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>();
		}
		raceManager.addWatcher (this);
		OneName = raceManager.OneName;
		TwoName = raceManager.TwoName;

	
		resourceOne = this.gameObject.transform.FindChild ("Resources").FindChild("ResourceOne").GetComponent<Text>();
		resourceTwo = this.gameObject.transform.FindChild ("Resources").FindChild("ResourceTwo").GetComponent<Text>();
		supply = this.gameObject.transform.FindChild ("Resources").FindChild("Supply").GetComponent<Text>();

		resourceOne.text = OneName + "  " + raceManager.ResourceOne;
		resourceTwo.text = TwoName + "  " + raceManager.ResourceTwo;
		supply.text =  raceManager.currentSupply + "/" + raceManager.supplyMax;
	
	}
	
	// Update is called once per frame
	void Update () {
	
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




}
