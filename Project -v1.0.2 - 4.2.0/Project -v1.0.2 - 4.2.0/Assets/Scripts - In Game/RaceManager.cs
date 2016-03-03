﻿using UnityEngine;
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

	public Ability UltOne;
	public Ability UltTwo;
	public Ability UltThree;
	public Ability UltFour;
	public RaceInfo.raceType myRace;
	public GameObject upgradeBall;

	public UIManager uiManage;

	private List<Upgrade> myUpgrades = new List<Upgrade> ();

	private SelectedManager selectedManager;


	private List<GameObject> resourceDropOffs = new List<GameObject> ();
    private List<IResource> knownGatherPoints = new List<IResource>();

	private List<ManagerWatcher> myWatchers = new List<ManagerWatcher>();

	private List<GameObject> unitList = new List<GameObject>();

	//used for unit ability validation
	private Dictionary<string, int > unitTypeCount = new Dictionary<string, int>();



	private List<LethalDamageinterface> deathTrigger = new List<LethalDamageinterface>();
	public RaceUIManager uiManager;


	private static SelectedManager statSelect;

	//public TechTree myTech;

	// Use this for initialization
	void Awake () {
		selectedManager = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
		uiManager = FindObjectOfType <RaceUIManager>();
		uiManage = (UIManager)FindObjectOfType (typeof(UIManager));
	
	}
	
	// Update is called once per frame
	void Update () {


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





	public void UnitDied(float supply)
	{
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
		deathTrigger.Add (input);
	}

	public void buildUnit (float supply)
	{



	}



	public bool UnitDying(GameObject Unit, GameObject deathSource)
	{bool finishDeath = true;

		//Debug.Log ("starting triggers");

		foreach (LethalDamageinterface trigger in deathTrigger) {


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
							a.UnitDied (unitName);

						}
					}
				}

			} 
			if (Unit.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.structure)) {
				GraphUpdateObject b =new GraphUpdateObject(Unit.GetComponent<CharacterController>().bounds); 

				StartCoroutine (DeathRescan (b));
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
		if (obj.GetComponent<UnitManager> ().myStats.isUnitType (UnitTypes.UnitTypeTag.worker)) {
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
		//Debug.Log ("STARTING");
	
		//apply all existing units built to new unit
		foreach (KeyValuePair<string, int> n in unitTypeCount) {
			
			if (n.Value > 0 ) {
	
				foreach (Ability ab in obj.GetComponent<UnitManager>().abilityList) {
					ab.newUnitCreated (n.Key);

				}
			}
		}


		// new unit, call update function on units abilities
		if(unitTypeCount[unitName] ==1){

			foreach (GameObject o in unitList) {
				if (o != null) {
					foreach (Ability a in o.GetComponent<UnitManager>().abilityList) {
						a.newUnitCreated (unitName);
				
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
				
			if(!obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.structure))	
				{
			
				selectBuildings = false;}


				foundUnits.Add(obj);
		}


		if (!selectBuildings) {


			for(int i = foundUnits.Count -1; i >-1; i--)
			{
				if(foundUnits[i].GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.structure))
				{	
					foundUnits.Remove(foundUnits[i]);}
			}
		}

		return foundUnits;
	}


    public void updateGatherLocations(IResource resource)
    {
        if (!knownGatherPoints.Contains(resource))
        {
            knownGatherPoints.Add(resource);
            resource.known = true;
        }
    }





	public void useAbilityOne()
	{if (UltOne != null) {
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltOne, 1);
		}
	}

	public void useAbilityTwo()
	{
		if (UltTwo != null) {
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltTwo, 1);
		}
	}

	public void useAbilityThree()
	{
		if (UltThree != null) {
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltThree, 1);
		}
	}

	public void useAbilityFour()
	{
		if (UltFour != null) {
			uiManage.SwitchMode (Mode.globalAbility);
			uiManage.setAbility (	UltFour, 1);
		}
	}


}
