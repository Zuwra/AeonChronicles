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

	public List<Upgrade> myUpgrades = new List<Upgrade> ();

	private SelectedManager selectedManager;


	private List<GameObject> resourceDropOffs = new List<GameObject> ();
   
	private List<ManagerWatcher> myWatchers = new List<ManagerWatcher>();

	//public List<GameObject> unitList = new List<GameObject>();
	Dictionary<string, List<UnitManager>> unitRoster =  new Dictionary<string, List<UnitManager>>();

	Dictionary<string, List<Ability>> UnitBuildTrigger =  new Dictionary<string, List<Ability>>();

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

		if (playerNumber == 1) {
			InvokeRepeating ("UltUpdate", .2f, .2f);
		}

		if (UltTwo &&  !UltTwo.myCost.StartsRefreshed && UltTwo.active) {
			StartCoroutine (UltTwoNotif ());}
		if (UltFour && !UltFour.myCost.StartsRefreshed && UltFour.active) {
			StartCoroutine (UltFourNotif ());}
	
	}
		

	public void UltUpdate()
	{
		if (slideOne && UltOne.active ) {
			slideOne.value = UltOne.myCost.cooldownProgress ();
			slideOne.gameObject.SetActive (slideOne.value < .99 && slideOne.value > .02f);
			ultBOne.interactable = (slideOne.value  > .99);

		}

		if (slideTwo && UltTwo.active) {
			slideTwo.value = UltTwo.myCost.cooldownProgress ();
			slideTwo.gameObject.SetActive (slideTwo.value < .99 && slideTwo.value > .01f);
			ultBTwo.interactable = (slideTwo.value > .99);

		
		}
		if (slideThree && UltThree.active) {
			slideThree.value = UltThree.myCost.cooldownProgress ();
			slideThree.gameObject.SetActive (slideThree.value < .99 && slideThree.value > .01f);
			ultBThree.interactable = (slideThree.value > .99);
		
		}
		if (slideFour && UltFour.active) {
			slideFour.value = UltFour.myCost.cooldownProgress ();
			slideFour.gameObject.SetActive (slideFour.value < .99 && slideFour.value > .01f);
			ultBFour.interactable = (slideFour.value > .99);

		}
	
	}


	public void commenceUpgrade(bool onOff, Upgrade upgrade, string unitname)
	{

		object[] temp = new object[2];
		temp [0] = onOff;
		temp [1] = upgrade;
		//temp [2] = unitname;

		foreach (KeyValuePair<string, List<UnitManager>> pair in unitRoster) {
			foreach (UnitManager tempMan  in pair.Value) {
				if (tempMan == null) {
					continue;}

				if (tempMan.UnitName == unitname) {

					tempMan.gameObject.SendMessage ("commence", temp);

				}
			}
		}
	}

	public void addBuildTrigger(string unitName, Ability a)
	{
		List<Ability> ab;
		if (UnitBuildTrigger.ContainsKey (unitName)) {
			ab = UnitBuildTrigger [unitName];
		} else {
			ab = new List<Ability> ();
			UnitBuildTrigger.Add (unitName, ab);
		}
		ab.Add (a);

		
	}



	public void addUpgrade(Upgrade upgrade, string unitname)
	{
		
		Component temp = upgradeBall.AddComponent (upgrade.GetType ());

		foreach(FieldInfo f in temp.GetType().GetFields())
		{
			f.SetValue (temp, f.GetValue (upgrade));
		}

		myUpgrades.Add ((Upgrade)temp);



		foreach (KeyValuePair<string, List<UnitManager>> pair in unitRoster) {
			foreach (UnitManager tempMan  in pair.Value) {
				if(tempMan){
					upgrade.ApplySkin (tempMan.gameObject);
					upgrade.applyUpgrade (tempMan.gameObject);

					if (tempMan.UnitName == unitname) {
				
						tempMan.gameObject.SendMessage ("researched", upgrade);
					}
				}
			}
		}
		if (Time.timeSinceLevelLoad > 1) {
			selectedManager.updateUI ();
		}
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

	public static void upDateSingleCard()
	{findSelectMan();
		statSelect.RedoSingle();
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




	public void applyUpgrade(UnitManager obj )
	{	foreach (Upgrade up in myUpgrades) {
			up.applyUpgrade (obj.gameObject);
			up.ApplySkin (obj.gameObject);
			obj.SendMessage ("researched", up,SendMessageOptions.DontRequireReceiver);
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

	public void UnitDied(float supply, UnitManager obj) 
	{
		if (obj) {
			if (unitRoster.ContainsKey (obj.UnitName)) {
				try {
					unitRoster [obj.UnitName].Remove (obj);
				} catch (SystemException) {
					Debug.Log ("Unit does Not exist in unit roster");
					return;
				}
			}
		}
		if (supply < 0) {


			supplyMax += supply;
		} else {
			currentSupply -= supply;
		}


		updateSupply (currentSupply,  Mathf.Min(supplyCap, supplyMax));
	}

	/*
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
*/
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
	public bool UnitDying(UnitManager Unit, GameObject deathSource, bool trueDeath)
	{bool finishDeath = true;

	
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
					foreach (ArmyUIManager uiMan in uiManager.production.GetComponents<ArmyUIManager> ()) {
						uiMan.unitLost (Unit);
					}
				}
			}
		
		
			string unitName = Unit.UnitName;

			if (unitTypeCount.ContainsKey (unitName)) {
				unitTypeCount [unitName]--;


				// No Units of tis type, call update function on units abilities

				if (unitTypeCount [unitName] == 0 && trueDeath) {
					//unitList.RemoveAll (item => item == null);

					unitRoster[unitName].RemoveAll (item => item == null);
					if(UnitBuildTrigger.ContainsKey(unitName)){
						UnitBuildTrigger [unitName].RemoveAll (item => item == null);
						foreach (Ability a in UnitBuildTrigger[unitName]){
							a.UnitDied (unitName);

						}
					}
				}

			} 
			if (Unit.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
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

			try{
				unitRoster [unitName].Remove (Unit);}
			catch{
			}

			//unitList.Remove(Unit);
			if (trueDeath) {
				foreach (LethalDamageinterface trigger in deathTrigger) {
					trigger.lethalDamageTrigger (Unit, deathSource);
				}
			}
		}
		//if (Unit.GetComponentInChildren<TurretMount> ()) {
		//	FButtonManager.main.updateTankNumber ();
		//}
		if (playerNumber == 1) {
			FButtonManager.main.updateNumbers (unitRoster);
		}
		return finishDeath;
	}

	IEnumerator DeathRescan(GraphUpdateObject b)
	{	
		yield return new WaitForSeconds (.2f);

			AstarPath.active.UpdateGraphs (b);

	}


	public void addUnit(UnitManager obj )
	{
		
		if (!unitRoster.ContainsKey (obj.UnitName)) {
			unitRoster.Add(obj.UnitName, new List<UnitManager>());
		}
		unitRoster [obj.UnitName].Add (obj);
		if (playerNumber == 1) {
			if (FButtonManager.main == null) {
				FButtonManager.main = GameObject.FindObjectOfType<FButtonManager> ();
			}
			FButtonManager.main.updateNumbers (unitRoster);


			if (obj.myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {

				if (uiManager != null) {
					uiManager.production.GetComponent<EconomyManager> ().updateWorker (1);
				}
			}

			if (uiManager != null) {
				foreach (ArmyUIManager uiMan in uiManager.production.GetComponents<ArmyUIManager> ()) {
					uiMan.updateUnits (obj);
				}
					
			}

		}

	



		if (obj.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			//This rescans the Astar graph after the unit dies
			GraphUpdateObject b =new GraphUpdateObject(obj.GetComponent<CharacterController>().bounds); 
			StartCoroutine (DeathRescan (b));
		}

	

		string unitName = obj.UnitName;

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

				if (n.Value < 1) {
					continue;
				}

				foreach (Ability ab in obj.abilityList) {
					if (ab != null) {
						//Debug.Log ("checking against a " + n.Key);
						ab.newUnitCreated (n.Key);
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
		if (unitTypeCount [unitName] == 1) {


			if(UnitBuildTrigger.ContainsKey(unitName)){
				UnitBuildTrigger [unitName].RemoveAll (item => item == null);
				foreach (Ability a in UnitBuildTrigger[unitName] ) {

						a.newUnitCreated (unitName);
				
					}
				}
			}



		applyUpgrade (obj);


		//uiManager.changeUnits ();
	}

	//Set income to false if you dont want it to count towards your income per minute.
	public void updateResources(float resOne, float resTwo, bool income)
	{bool hasNull = false;
		ResourceOne += resOne;
		ResourceTwo += resTwo;



		foreach (ManagerWatcher watch in myWatchers) {
			if(watch != null){
			//	Debug.Log ("Checking " + watch);
				watch.updateResources(ResourceOne, ResourceTwo, income);}
			else{hasNull = true;}

		}
		if(hasNull){
			myWatchers.RemoveAll(item => item == null);}

		if (income) {
			if (resOne >= 0 && resTwo >= 0) {


				totalResOne += resOne;
				totalResTwo += resTwo;
				uiManager.production.GetComponent<EconomyManager> ().updateMoney ((int)resOne, (int)resTwo);
			}
		}
	}
	
	
	public void updateSupply( float current, float max){
		bool hasNull= false;
		foreach (ManagerWatcher watch in myWatchers) {
		//	Debug.Log ("Manager watcher "+watch);
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


	public Dictionary<string, List<UnitManager>> getUnitList()
	{
		cleanUnitRoster ();
		return unitRoster;
	}

	public Dictionary<string, List<UnitManager>> getFastUnitList()
	{
		return unitRoster;
	}

	public void cleanUnitRoster()
	{
		foreach (KeyValuePair<string, List<UnitManager>> pair in unitRoster) {

			pair.Value.RemoveAll(item => item == null);

		}

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


//Get all units in a given selection rectangle
	public List<UnitManager> getUnitSelection(Vector3 upperLeft, Vector3 bottRight)
	{cleanUnitRoster ();
		bool selectBuildings = true;
		bool bDown = Input.GetKey (KeyCode.B);

		List<UnitManager> foundUnits = new List<UnitManager> ();

		foreach (KeyValuePair<string, List<UnitManager>> pair in unitRoster) {
			pair.Value.RemoveAll (item => item == null);
			if(pair.Value.Count >0){
				if (pair.Value [0].getUnitStats ().isUnitType(UnitTypes.UnitTypeTag.Turret)) {
					continue;
				}
				}
			foreach (UnitManager obj in pair.Value) {
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
					if (!obj.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
						selectBuildings = false;
					}

				} else {
					if (obj.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
						selectBuildings = false;
					}
				}
			}
		}
			for (int i = foundUnits.Count - 1; i > -1; i--) {
				if (!bDown ) {
				if (!selectBuildings && foundUnits [i].myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {	
						foundUnits.Remove (foundUnits [i]);
					}
				} else {
				
				if (!selectBuildings && !foundUnits [i].myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {	
						foundUnits.Remove (foundUnits [i]);
					}

			}
		}

		return foundUnits;
	}


	public List<UnitManager> getUnitOnScreen(bool select, string Unitname)
	{
		cleanUnitRoster ();

		List<UnitManager> foundUnits = new List<UnitManager> ();

		if (!unitRoster.ContainsKey (Unitname)) {
			return foundUnits;
		}

		foreach (UnitManager obj in unitRoster[Unitname]) {

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


	public List<UnitManager> getAllUnitsOnScreen()
	{
		cleanUnitRoster ();

		List<UnitManager> foundUnits = new List<UnitManager> ();

		foreach (KeyValuePair<string, List<UnitManager>> pair in unitRoster) {
			foreach (UnitManager tempMan  in pair.Value) {
				
				if (tempMan.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure) || tempMan.myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
					continue;
				}

				Vector3 tempLocation = Camera.main.WorldToScreenPoint (tempMan.transform.position);
				//Debug.Log ("Checking " + tempLocation + "   within  "+ upperLeft + " bot " + bottRight);
				if (tempLocation.x + tempMan.GetComponent<CharacterController> ().radius * 5 < 0) {
					continue;
				}
				if (tempLocation.x -tempMan.GetComponent<CharacterController> ().radius * 5 > Screen.width) {
					continue;
				}
				if (tempLocation.y - tempMan.GetComponent<CharacterController> ().radius * 5 > Screen.height) {
					continue;
				}
				if (tempLocation.y + tempMan.GetComponent<CharacterController> ().radius * 5 < 0) {
					continue;
				}

				foundUnits.Add (tempMan);

			}
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

	public List<VeteranStats> getVeteranStats()
	{
		return MVP.UnitStats ();
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
		
			if (UltOne.active && UltOne.canActivate (true).canCast) {

				uiManage.SwitchMode (Mode.globalAbility);
				uiManage.setAbility (UltOne, 1, "");
			}
		}
	}

	public void useAbilityTwo()
	{
		if (UltTwo != null) {
			if (UltTwo.active && UltTwo.canActivate (true).canCast) {
				uiManage.SwitchMode (Mode.globalAbility);
				uiManage.setAbility (UltTwo, 1, "");
			}
		}
	}

	public void useAbilityThree()
	{
		if (UltThree != null) {
			if (UltThree.active && UltThree.canActivate (true).canCast) {
				uiManage.SwitchMode (Mode.globalAbility);
				uiManage.setAbility (UltThree, 1, "");
			}
		}
	}

	public void useAbilityFour()
	{
		if (UltFour != null) {
			if (UltFour.active && UltFour.canActivate (true).canCast) {
				uiManage.SwitchMode (Mode.globalAbility);
				uiManage.setAbility (UltFour, 1, "");
			}
		}
	}

	public void castedGlobal(TargetAbility ult)
	{
		if (ult == UltFour) {
			StartCoroutine (UltFourNotif());
		} else if (ult == UltTwo) {
			StartCoroutine (UltTwoNotif());
		
		}
	}

	IEnumerator UltTwoNotif()
	{
		yield return new WaitForSeconds (UltTwo.myCost.cooldown);
		ErrorPrompt.instance.UltTwoDone ();
	}

	IEnumerator UltFourNotif()
	{
		yield return new WaitForSeconds (UltFour.myCost.cooldown);
		ErrorPrompt.instance.UltFourDone ();
	}


	public int UnitsLost()
	{return unitsLost;}

	public int totalResO()
	{return (int)totalResOne;}

	public int totalResT()
	{return (int)totalResTwo;
	}

	public int getArmyCount()
	{int total = 0;
		cleanUnitRoster ();

		foreach (KeyValuePair<string, List<UnitManager>> pair in unitRoster) {
			if (pair.Value.Count == 0 || pair.Value [0].myStats.isUnitType (UnitTypes.UnitTypeTag.Structure) || pair.Value [0].myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
				continue;}
			total += pair.Value.Count;

		}
		return total;
	}

}
