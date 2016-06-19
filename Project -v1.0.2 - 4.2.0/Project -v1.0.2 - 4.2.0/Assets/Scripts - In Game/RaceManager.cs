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

	private List<GameObject> unitList = new List<GameObject>();

	//used for unit ability validation
	private Dictionary<string, int > unitTypeCount = new Dictionary<string, int>();



	private List<LethalDamageinterface> lethalTrigger = new List<LethalDamageinterface>();
	private List<LethalDamageinterface> deathTrigger = new List<LethalDamageinterface>();
	public RaceUIManager uiManager;


	private static SelectedManager statSelect;
	//Used for end level stats
	private int unitsLost;
	private float totalResOne;
	private float totalResTwo;

	//public TechTree myTech;

	// Use this for initialization
	void Awake () {
		selectedManager = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
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
			if (slideOne.value < .99) {
				ultBOne.interactable = false;
			}
			else{
				ultBOne.interactable = true;
			}

		}

		if (slideTwo && UltTwo.active) {
			slideTwo.value = UltTwo.myCost.cooldownProgress ();
			if (slideTwo.value < .99) {
				ultBTwo.interactable = false;
			}
			else{
					ultBTwo.interactable = true;
				}

		}
		if (slideThree && UltThree.active) {
			slideThree.value = UltThree.myCost.cooldownProgress ();
			if (slideThree.value < .99) {
				ultBThree.interactable = false;
			}
			else{
				ultBThree.interactable = true;
			}
		}
		if (slideFour && UltFour.active) {
			slideFour.value = UltFour.myCost.cooldownProgress ();
			if (slideFour.value < .99) {
				ultBFour.interactable = false;
			}
			else{
				ultBFour.interactable = true;
			}
		}


		frameUpdate = 0;

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
			statSelect = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
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

		updateSupply(currentSupply, supplyMax);

	}

	public void UnitCreated(float supply)
	{
		if (supply < 0) {


			supplyMax -= supply;
		} else {
			currentSupply += supply;
		}
		
		updateSupply(currentSupply, supplyMax);
	}



	public void addDeathWatcher(LethalDamageinterface input)
	{
		lethalTrigger.Add (input);
	}

	public void buildUnit (float supply)
	{



	}



	public bool UnitDying(GameObject Unit, GameObject deathSource)
	{bool finishDeath = true;

		//Debug.Log ("starting triggers");

		foreach (LethalDamageinterface trigger in lethalTrigger) {


			if(trigger != null){
				if(trigger.lethalDamageTrigger(Unit, deathSource) == false)
				{
					finishDeath = false;
				}}
		}

		if (finishDeath) { 
			if (uiManager != null) {
				uiManager.production.GetComponent<ArmyUIManager> ().unitLost (Unit);
			}
		

			string unitName = Unit.GetComponent<UnitManager> ().UnitName;

			if (unitTypeCount.ContainsKey (unitName)) {
				unitTypeCount [unitName]--;


				// No Units of tis type, call update function on units abilities
				if (unitTypeCount [unitName] == 0) {
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
			unitsLost++;
			unitList.Remove(Unit);
			foreach (LethalDamageinterface trigger in deathTrigger) {
				trigger.lethalDamageTrigger (Unit, deathSource);

			}
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
		
		unitList.Add(obj);
		if (obj.GetComponent<UnitManager> ().myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
			if (uiManager != null) {
				uiManager.production.GetComponent<EconomyManager> ().updateWorker ();
			}
		}
		if (uiManager != null) {
			uiManager.production.GetComponent<ArmyUIManager> ().updateUnits (obj);
		}

		string unitName = obj.GetComponent<UnitManager> ().UnitName;

		if (unitTypeCount.ContainsKey (unitName)) {
			unitTypeCount [unitName]++;
		} else {
			unitTypeCount.Add (unitName, 1);
		}
		//Debug.Log ("Adding" + obj + "  " + playerNumber + "count " + unitTypeCount [unitName]);
		//Debug.Log ("STARTING");
	
		//apply all existing units built to new unit
		foreach (KeyValuePair<string, int> n in unitTypeCount) {
			
			if (n.Value > 0 ) {
	
				foreach (Ability ab in obj.GetComponent<UnitManager>().abilityList) {
					if (ab != null) {
						ab.newUnitCreated (n.Key);
					}
				}
			}
		}


		// new unit, call update function on units abilities
		if(unitTypeCount[unitName] ==1){

			foreach (GameObject o in unitList) {
				if (o != null) {
					foreach (Ability a in o.GetComponent<UnitManager>().abilityList) {
						if (a != null) {
							a.newUnitCreated (unitName);
						}
				
					}
				}
			}
		}




		//uiManager.changeUnits ();
	}

	public void updateResources(float resOne, float resTwo)
	{bool hasNull = false;
		ResourceOne += resOne;
		ResourceTwo += resTwo;
		totalResOne += resOne;
		totalResTwo += resTwo;


		foreach (ManagerWatcher watch in myWatchers) {
			if(watch != null){
				watch.updateResources(ResourceOne, ResourceTwo);}
			else{hasNull = true;}

		}
		if(hasNull){
			myWatchers.RemoveAll(item => item == null);}

		if (resOne >= 0 && resTwo >= 0) {
			uiManager.production.GetComponent<EconomyManager> ().updateMoney ((int)resOne, (int)resTwo);
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
	{return unitList;
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

		List<GameObject> foundUnits = new List<GameObject> ();
	
		foreach (GameObject obj in unitList) {
			Vector3 tempLocation = Camera.main.WorldToScreenPoint (obj.transform.position);

			if(tempLocation.x + obj.GetComponent<CharacterController>().radius*5  < upperLeft.x )
				{continue;}
			if(tempLocation.x - obj.GetComponent<CharacterController>().radius*5 > bottRight.x)
				{continue;}
			if(tempLocation.y - obj.GetComponent<CharacterController>().radius*5> upperLeft.y)
				{continue;}
			if( tempLocation.y + obj.GetComponent<CharacterController>().radius*5 < bottRight.y)
				{	continue;}
				
			if(!obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Structure))	
				{
			
				selectBuildings = false;}


				foundUnits.Add(obj);
		}


		if (!selectBuildings) {


			for(int i = foundUnits.Count -1; i >-1; i--)
			{
				if(foundUnits[i].GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Structure))
				{	
					foundUnits.Remove(foundUnits[i]);}
			}
		}

		return foundUnits;
	}



	public void addActualDeathWatcher(LethalDamageinterface input){
		deathTrigger.Add (input);
	}



	public void useAbilityOne()
	{if (UltOne != null) {
			if(UltOne.active &&UltOne.canActivate().canCast)
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltOne, 1);
		}
	}

	public void useAbilityTwo()
	{
		if (UltTwo != null) {
			if(UltTwo.active &&UltTwo.canActivate().canCast)
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltTwo, 1);
		}
	}

	public void useAbilityThree()
	{
		if (UltThree != null) {
			if(UltThree.active &&UltThree.canActivate().canCast)
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltThree, 1);
		}
	}

	public void useAbilityFour()
	{
		if (UltFour != null) {
			if(UltFour.active && UltFour.canActivate().canCast)
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
