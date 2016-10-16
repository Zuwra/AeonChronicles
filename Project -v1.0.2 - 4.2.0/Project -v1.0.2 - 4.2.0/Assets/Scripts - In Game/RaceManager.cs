using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using Pathfinding;

public class RaceManager : MonoBehaviour, ManagerWatcher {



	public string OneName;
	public float ResourceOne;
	public string TwoName;
	public float ResourceTwo;
	public int playerNumber;

	public float supplyMax;

	public float currentSupply;
	public float supplyCap;

	public Ability UltOne;
	public Slider slideOne;
	public Button ultBOne;

	public Ability UltTwo;
	public Slider slideTwo;
	public Button ultBTwo;

	public Ability UltThree;
	public Slider slideThree;
	public Button ultBThree;

	public Ability UltFour;
	public Slider slideFour;
	public Button ultBFour;

	public RaceInfo.raceType myRace;
	public GameObject upgradeBall;

	public UIManager uiManage;

	private List<Upgrade> myUpgrades = new List<Upgrade> ();

	private SelectedManager selectedManager;


	private List<GameObject> resourceDropOffs = new List<GameObject> ();
   
	private List<ManagerWatcher> myWatchers = new List<ManagerWatcher>();

	public List<GameObject> unitList = new List<GameObject>();
	private MVPCalculator MVP = new MVPCalculator();
	//used for unit ability validation
	private Dictionary<string, int > unitTypeCount = new Dictionary<string, int>();



	private List<LethalDamageinterface> lethalTrigger = new List<LethalDamageinterface>();
	private List<LethalDamageinterface> deathTrigger = new List<LethalDamageinterface>();
	public RaceUIManager uiManager;


	private static SelectedManager statSelect;
	//Used for end level stats
	public int unitsLost;
	private float totalResOne;
	private float totalResTwo;

	//public TechTree myTech;

	// Use this for initialization
	void Awake () {
		selectedManager = GameObject.FindObjectOfType<SelectedManager> ();
		uiManager = FindObjectOfType <RaceUIManager>();
		uiManage = (UIManager)FindObjectOfType (typeof(UIManager));
	
	}

	// This is for optimization
	private int frameUpdate = 0;
	// Update is called once per frame
	void Update () {

		if (frameUpdate < 5) {
			frameUpdate++;
			return;
		}

		if (slideOne && UltOne.active) {
			slideOne.value = UltOne.myCost.cooldownProgress ();
			slideOne.gameObject.SetActive (slideOne.value < .99 && slideOne.value > .02f);
			if (slideOne.value < .99) {
				ultBOne.interactable = false;
			}
			else{
				ultBOne.interactable = true;
			}

		}

		if (slideTwo && UltTwo.active) {
			slideTwo.value = UltTwo.myCost.cooldownProgress ();
			slideTwo.gameObject.SetActive (slideTwo.value < .99 && slideTwo.value > .01f);
			if (slideTwo.value < .99) {
				ultBTwo.interactable = false;
			}
			else{
					ultBTwo.interactable = true;
				}

		}
		if (slideThree && UltThree.active) {
			slideThree.value = UltThree.myCost.cooldownProgress ();
			slideThree.gameObject.SetActive (slideThree.value < .99 && slideThree.value > .01f);
			if (slideThree.value < .99) {
				ultBThree.interactable = false;
			}
			else{
				ultBThree.interactable = true;
			}
		}
		if (slideFour && UltFour.active) {
			slideFour.value = UltFour.myCost.cooldownProgress ();
			slideFour.gameObject.SetActive (slideFour.value < .99 && slideFour.value > .01f);
			if (slideFour.value < .99) {
				ultBFour.interactable = false;
			}
			else{
				ultBFour.interactable = true;
			}
		}


		frameUpdate = 0;

	}


	public void commenceUpgrade(bool onOff, Upgrade upgrade, string unitname)
	{

		object[] temp = new object[3];
		temp [0] = onOff;
		temp [1] = upgrade;
		temp [2] = unitname;
		foreach (GameObject obj in unitList) {
			if (obj.GetComponent<UnitManager> ().UnitName == unitname) {


				obj.SendMessage ("commence", temp);

			}
		}
	}

	public void addUpgrade(Upgrade upgrade, string unitname)
	{
		
		Component temp = upgradeBall.AddComponent (upgrade.GetType ());



		foreach(FieldInfo f in temp.GetType().GetFields())
		{
			f.SetValue (temp, f.GetValue (upgrade));
		}

		myUpgrades.Add ((Upgrade)temp);


		foreach (GameObject obj in unitList) {
			upgrade.applyUpgrade (obj);

			if (obj.GetComponent<UnitManager> ().UnitName == unitname) {
				
				obj.SendMessage ("researched", upgrade);
			
			}
		}

		selectedManager.updateUI ();

	}

	public static void  findSelectMan()
	{if (statSelect == null) {
			statSelect = GameObject.FindObjectOfType<SelectedManager> ();
		}
	}

	public static void upDateUI()
	{findSelectMan();
		statSelect.reImageUI ();
	}

	public static void upDateAutocast()
	{findSelectMan();
		statSelect.AutoCastUI();
	}

	public static void updateUIUnitcount()
	{findSelectMan();
		statSelect.updateUI();
	}

	public static void updateActivity()
	{findSelectMan();
		statSelect.updateUIActivity();
	}


	public static void removeUnitSelect(RTSObject man)
	{findSelectMan();
		statSelect.DeselectObject(man);}

	public static void AddUnitSelect(RTSObject man)
	{findSelectMan();
		statSelect.AddObject(man);}




	public void applyUpgrade(GameObject obj )
	{	foreach (Upgrade up in myUpgrades) {
			up.applyUpgrade (obj);
		}
	}


	public void addWatcher(ManagerWatcher input)
	{myWatchers.Add (input);}
	 

	public void buildingUnit(UnitProduction abil)
	{uiManager.production.GetComponent<ProductionManager>().updateUnits(abil);	}

	public void stopBuildingUnit(UnitProduction abil)
	{uiManager.production.GetComponent<ProductionManager>().unitLost(abil);		}

	public bool hasSupplyAvailable(float sup)
	{
		return (sup <= (Mathf.Min(supplyCap, supplyMax) - currentSupply));
		
	}

	public void UnitDied(float supply, GameObject obj)
	{
		if (unitList.Contains (obj)) {
			unitList.Remove (obj);

		}

	if (supply < 0) {

		
			supplyMax += supply;
		} else {
			currentSupply -= supply;
		}

	
			updateSupply (currentSupply,  Mathf.Min(supplyCap, supplyMax));
	

	}

	public void UnitCreated(float supply)
	{//Debug.Log ("Created " + supply);
		if (supply < 0) {

	
			supplyMax -= supply;
		} else {
			currentSupply += supply;
		}

		updateSupply (currentSupply,  Mathf.Min(supplyCap, supplyMax));

	}



	public void addDeathWatcher(LethalDamageinterface input)
	{
		lethalTrigger.Add (input);
	}

	public void buildUnit (float supply)
	{



	}


	//Truedeath applies to thing like summons and building placers. they aren't real units so they shouldnt be treated as such.
	public bool UnitDying(GameObject Unit, GameObject deathSource, bool trueDeath)
	{bool finishDeath = true;

		//Debug.Log ("starting triggers");

		if (trueDeath) {
			foreach (LethalDamageinterface trigger in lethalTrigger) {


				if (trigger != null) {
					if (trigger.lethalDamageTrigger (Unit, deathSource) == false) {
						finishDeath = false;
					}
				}
			}
		}

		if (finishDeath) { 
			if (uiManager != null) {
				if (playerNumber == 1) {
					uiManager.production.GetComponent<ArmyUIManager> ().unitLost (Unit);
				}
			}
		

			string unitName = Unit.GetComponent<UnitManager> ().UnitName;

			if (unitTypeCount.ContainsKey (unitName)) {
				unitTypeCount [unitName]--;


				// No Units of tis type, call update function on units abilities
				if (unitTypeCount [unitName] == 0 && trueDeath) {
					unitList.RemoveAll (item => item == null);
					foreach (GameObject o in unitList) {

						foreach (Ability a in o.GetComponent<UnitManager>().abilityList) {
							if(a != null)
							a.UnitDied (unitName);

						}
					}
				}

			} 
			if (Unit.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				//This rescans the Astar graph after the unit dies
				GraphUpdateObject b =new GraphUpdateObject(Unit.GetComponent<CharacterController>().bounds); 

				StartCoroutine (DeathRescan (b));
			}

			if (trueDeath) {
				if (Unit.GetComponent<UnitManager> ().myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
					//Debug.Log ("Is a worker");
					if (uiManager != null) {
						uiManager.production.GetComponent<EconomyManager> ().updateWorker (-1);
					}
				}
			}

			unitsLost++;


			unitList.Remove(Unit);
			if (trueDeath) {
				foreach (LethalDamageinterface trigger in deathTrigger) {
					trigger.lethalDamageTrigger (Unit, deathSource);
				}
			}
		}
		if (Unit.GetComponentInChildren<TurretMount> ()) {
			FButtonManager.main.updateTankNumber ();
		}
		if (playerNumber == 1) {
			FButtonManager.main.updateNumbers (unitList);
		}
		return finishDeath;
	}

	IEnumerator DeathRescan(GraphUpdateObject b)
	{	
		yield return new WaitForSeconds (.2f);

			AstarPath.active.UpdateGraphs (b);

	}


	public void addUnit(GameObject obj )
	{
		//Debug.Log ("Adding " + obj.name + "   " + playerNumber);
		if (!unitList.Contains (obj)) {
			unitList.Add (obj);
		}
		if (playerNumber == 1) {
			if (FButtonManager.main == null) {
				FButtonManager.main = GameObject.FindObjectOfType<FButtonManager> ();
			}
			FButtonManager.main.updateNumbers (unitList);


			if (obj.GetComponent<UnitManager> ().myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {

				if (uiManager != null) {
					uiManager.production.GetComponent<EconomyManager> ().updateWorker (1);
				}
			}

			if (uiManager != null) {
				uiManager.production.GetComponent<ArmyUIManager> ().updateUnits (obj);
			}

		}

	



		if (obj.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			//This rescans the Astar graph after the unit dies
			GraphUpdateObject b =new GraphUpdateObject(obj.GetComponent<CharacterController>().bounds); 
			StartCoroutine (DeathRescan (b));
		}

	

		string unitName = obj.GetComponent<UnitManager> ().UnitName;

		if (unitTypeCount.ContainsKey (unitName)) {
			
			unitTypeCount [unitName]++;
			if (unitTypeCount [unitName] < 1) {
				unitTypeCount [unitName] = 1;
			}
		} else {
			
			unitTypeCount.Add (unitName, 1);
		}

			//apply all existing units built to new unit
			foreach (KeyValuePair<string, int> n in unitTypeCount) {

				if (n.Value > 0 ) {

					foreach (Ability ab in obj.GetComponent<UnitManager>().abilityList) {
						if (ab != null) {
							//Debug.Log ("checking against a " + n.Key);
							ab.newUnitCreated (n.Key);
						}
					}
				}
			}



		//Debug.Log ("Adding" + obj + "  " + playerNumber + "count " + unitTypeCount [unitName]);
		//Debug.Log ("STARTING");
	

		foreach (BuildUnitObjective objective in GameObject.FindObjectsOfType<BuildUnitObjective>()) {
			objective.buildUnit (obj);
		}
		//Debug.Log ("Just built a " + unitName + "    " + unitTypeCount[unitName]);
		// new unit, call update function on units abilities
		if(unitTypeCount[unitName] ==1){

			foreach (GameObject o in unitList) {
				if (o != null) {
					foreach (Ability a in o.GetComponent<UnitManager>().abilityList) {
						if (a != null) {
							//Debug.Log ("checking a " + a.Name + "   " + unitName);
							a.newUnitCreated (unitName);
						}
				
					}
				}
			}
		}




		//uiManager.changeUnits ();
	}

	//Set income to false if you dont want it to count towards your income per minute.
	public void updateResources(float resOne, float resTwo, bool income)
	{bool hasNull = false;
		ResourceOne += resOne;
		ResourceTwo += resTwo;
		totalResOne += resOne;
		totalResTwo += resTwo;


		foreach (ManagerWatcher watch in myWatchers) {
			if(watch != null){
				Debug.Log ("Checking " + watch);
				watch.updateResources(ResourceOne, ResourceTwo, income);}
			else{hasNull = true;}

		}
		if(hasNull){
			myWatchers.RemoveAll(item => item == null);}

		if (income) {
			if (resOne >= 0 && resTwo >= 0) {
				uiManager.production.GetComponent<EconomyManager> ().updateMoney ((int)resOne, (int)resTwo);
			}
		}
	}
	
	
	public void updateSupply( float current, float max){
		bool hasNull= false;
		foreach (ManagerWatcher watch in myWatchers) {
			if(watch != null){
				watch.updateSupply(current, max);}
			else{hasNull = true;}

		}
		if(hasNull){
			myWatchers.RemoveAll(item => item == null);}
	}
	
	public void updateUpgrades(){
		bool hasNull= false;
		foreach (ManagerWatcher watch in myWatchers) {
			if(watch != null){
				watch.updateUpgrades();}
			else{hasNull = true;}
			if(hasNull){
				myWatchers.RemoveAll(item => item == null);}
		}


	}


	public List<GameObject> getUnitList()
	{
		unitList.RemoveAll(item => item == null);
		return unitList;
	}

	public void addDropOff(GameObject obj)
	{resourceDropOffs.Add (obj);}



	public GameObject getNearestDropOff(GameObject worker)
		{
		float closest = 10000000;
		resourceDropOffs.RemoveAll(item => item == null);
		if (resourceDropOffs.Count == 0) {
			return null;}
		GameObject nearest = resourceDropOffs[0];

		foreach (GameObject obj in resourceDropOffs) {
			float distance = Vector3.Distance(obj.transform.position,worker.transform.position);
			if(distance < closest)
			
			{closest = distance;
				nearest = obj;}

		}
		return nearest;
	}

	public List<GameObject> getUnitSelection(Vector3 upperLeft, Vector3 bottRight)
	{unitList.RemoveAll(item => item == null);
		bool selectBuildings = true;
		bool bDown = Input.GetKey (KeyCode.B);

		List<GameObject> foundUnits = new List<GameObject> ();
	
		foreach (GameObject obj in unitList) {
			Vector3 tempLocation = Camera.main.WorldToScreenPoint (obj.transform.position);
			//Debug.Log ("Checking " + tempLocation + "   within  "+ upperLeft + " bot " + bottRight);
			if (tempLocation.x + obj.GetComponent<CharacterController> ().radius * 5 < upperLeft.x) {
				continue;
			}
			if (tempLocation.x - obj.GetComponent<CharacterController> ().radius * 5 > bottRight.x) {
				continue;
			}
			if (tempLocation.y - obj.GetComponent<CharacterController> ().radius * 5 > upperLeft.y) {
				continue;
			}
			if (tempLocation.y + obj.GetComponent<CharacterController> ().radius * 5 < bottRight.y) {
				continue;
			}
				
			foundUnits.Add (obj);
			if (!bDown) {
				if (!obj.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.Structure)) {
					selectBuildings = false;
				}



			} else {
				if (obj.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.Structure)) {
					selectBuildings = false;
				}
			}
		}

			for (int i = foundUnits.Count - 1; i > -1; i--) {
				if (!bDown ) {
				if (!selectBuildings && foundUnits [i].GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.Structure)) {	
						foundUnits.Remove (foundUnits [i]);
					}
				} else {
				
				if (!selectBuildings && !foundUnits [i].GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.Structure)) {	
						foundUnits.Remove (foundUnits [i]);
					}

			}
		}

		return foundUnits;
	}


	public List<GameObject> getUnitOnScreen(bool select, string Unitname)
	{
		unitList.RemoveAll(item => item == null);

		List<GameObject> foundUnits = new List<GameObject> ();

		foreach (GameObject obj in unitList) {
			UnitManager tempMan = obj.GetComponent<UnitManager> ();
			if (tempMan.UnitName != Unitname) {
				continue;
			}

			Vector3 tempLocation = Camera.main.WorldToScreenPoint (obj.transform.position);
			//Debug.Log ("Checking " + tempLocation + "   within  "+ upperLeft + " bot " + bottRight);
			if (tempLocation.x + obj.GetComponent<CharacterController> ().radius * 5 < 0) {
				continue;
			}
			if (tempLocation.x - obj.GetComponent<CharacterController> ().radius * 5 > Screen.width) {
				continue;
			}
			if (tempLocation.y - obj.GetComponent<CharacterController> ().radius * 5 > Screen.height) {
				continue;
			}
			if (tempLocation.y + obj.GetComponent<CharacterController> ().radius * 5 < 0) {
				continue;
			}

			foundUnits.Add (obj);
		
		}

	

		return foundUnits; 
	}


	public void addVeteranStat(VeteranStats input)
	{if (MVP == null) {
			MVP = new MVPCalculator ();}
		MVP.addVet (input);
	}

	public void getMVPScore()
	{

		Debug.Log (MVP.getMVP());
	}

	public List<VeteranStats> getUnitStats()
	{
		List<VeteranStats> toReturn = new List<VeteranStats> ();

		foreach (VeteranStats vs in MVP.UnitStats()) {
			if (vs.isWarrior) {

					toReturn.Add (vs);

			}
		
		}
		toReturn.Sort ();


		List<VeteranStats> realReturn = new List<VeteranStats> ();
		for (int i = 0; i < Mathf.Min (10, toReturn.Count); i++) {
			realReturn.Add (toReturn [i]);
		}

		return realReturn;

	}


	public void addActualDeathWatcher(LethalDamageinterface input){
		deathTrigger.Add (input);
	}



	public void useAbilityOne()
	{if (UltOne != null) {
			if(UltOne.active &&UltOne.canActivate(true).canCast)
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltOne, 1);
		}
	}

	public void useAbilityTwo()
	{
		if (UltTwo != null) {
			if(UltTwo.active &&UltTwo.canActivate(true).canCast)
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltTwo, 1);
		}
	}

	public void useAbilityThree()
	{
		if (UltThree != null) {
			if(UltThree.active &&UltThree.canActivate(true).canCast)
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltThree, 1);
		}
	}

	public void useAbilityFour()
	{
		if (UltFour != null) {
			if(UltFour.active && UltFour.canActivate(true).canCast)
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltFour, 1);
		}
	}

	public int UnitsLost()
	{return unitsLost;}

	public int totalResO()
	{return (int)totalResOne;}

	public int totalResT()
	{return (int)totalResTwo;
	}

}
