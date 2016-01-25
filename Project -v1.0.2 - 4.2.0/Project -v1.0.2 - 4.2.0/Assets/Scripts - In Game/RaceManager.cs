


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;


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


	private List<Upgrade> myUpgrades = new List<Upgrade> ();

	private SelectedManager selectedManager;


	private List<GameObject> resourceDropOffs = new List<GameObject> ();
    private List<IResource> knownGatherPoints = new List<IResource>();

	private List<ManagerWatcher> myWatchers = new List<ManagerWatcher>();

	private List<GameObject> unitList = new List<GameObject>();

	private List<LethalDamageinterface> deathTrigger = new List<LethalDamageinterface>();
	public RaceUIManager uiManager;


	//public TechTree myTech;

	// Use this for initialization
	void Awake () {
		selectedManager = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
		uiManager = FindObjectOfType <RaceUIManager>();

	
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

	public static void upDateUI()
	{GameObject.Find ("Manager").GetComponent<SelectedManager> ().reImageUI ();
	}

	public static void upDateAutocast()
	{GameObject.Find ("Manager").GetComponent<SelectedManager> ().AutoCastUI();
	}

	public void applyUpgrade(GameObject obj )
	{	foreach (Upgrade up in myUpgrades) {
			up.applyUpgrade (obj);
		}
	}


	public void addWatcher(ManagerWatcher input)
	{myWatchers.Add (input);}

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
				uiManager.production.GetComponent<ArmyUIManager> ().unitLost(Unit);
			}
		}
		return finishDeath;

	}

	public void UnitDied(float supply)
	{
	if (supply > 0) {


			supplyMax -= supply;
		} else {
			currentSupply += supply;
		}

		updateSupply(currentSupply, supplyMax);


		unitList.RemoveAll(item => item == null);


		//uiManager.dropdowns[].changeUnits ();
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

		uiManager.production.GetComponent<EconomyManager> ().updateMoney((int)resOne, (int)resTwo);

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
		}
	}

	public void useAbilityTwo()
	{
		if (UltTwo != null) {
		}
	}

	public void useAbilityThree()
	{
		if (UltThree != null) {
		}
	}

	public void useAbilityFour()
	{
		if (UltFour != null) {
		}
	}


}
