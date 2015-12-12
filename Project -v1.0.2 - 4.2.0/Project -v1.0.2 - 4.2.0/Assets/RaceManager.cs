﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaceManager : MonoBehaviour, ManagerWatcher {



	public string OneName;
	public float ResourceOne;
	public string TwoName;
	public float ResourceTwo;
	public int playerNumber;

	public float supplyMax;
	public float currentSupply;


	private List<GameObject> resourceDropOffs = new List<GameObject> ();


	private List<ManagerWatcher> myWatchers = new List<ManagerWatcher>();

	private HashSet<GameObject> unitList = new HashSet<GameObject>();

	private List<LethalDamageinterface> deathTrigger = new List<LethalDamageinterface>();

	//public TechTree myTech;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addWatcher(ManagerWatcher input)
	{myWatchers.Add (input);}




	public bool UnitDying(GameObject Unit, GameObject deathSource)
	{bool finishDeath = true;

		//Debug.Log ("starting triggers");

		foreach (LethalDamageinterface trigger in deathTrigger) {


		if(trigger != null){
			if(trigger.lethalDamageTrigger(Unit, deathSource) == false)
				{Debug.Log ("triggering");
				finishDeath = false;
				}}
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
	}

	public void UnitCreated(float supply)
	{
		if (supply > 0) {


			supplyMax += supply;
		} else {
			currentSupply -= supply;
		}
		
		updateSupply(currentSupply, supplyMax);
	}



	public void addDeathWatcher(LethalDamageinterface input)
	{
		deathTrigger.Add (input);
	}

	public void buildUnit (float resOne, float resTwo)
	{


			ResourceOne -= resOne;
			ResourceTwo -= resTwo;

		updateResources (ResourceOne, ResourceTwo);

	}


	public void addUnit(GameObject obj )
	{//Debug.Log (StackTraceUtility.ExtractStackTrace() );
		//Debug.Log (obj.name);
		unitList.Add(obj);
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


	public HashSet<GameObject> getUnitList()
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
	{
		bool selectBuildings = true;

		List<GameObject> foundUnits = new List<GameObject> ();
	
		foreach (GameObject obj in unitList) {
			//Debug.Log("Unit " + obj.name);
			if(obj.transform.position.x + obj.GetComponent<CharacterController>().radius  < upperLeft.x )
				{continue;}
			if(obj.transform.position.x - obj.GetComponent<CharacterController>().radius > bottRight.x)
				{continue;}
			if(obj.transform.position.z - obj.GetComponent<CharacterController>().radius> upperLeft.z)
				{continue;}
			if( obj.transform.position.z + obj.GetComponent<CharacterController>().radius < bottRight.z)
				{	continue;}
				
			if(!obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.structure))	
				{
			
				selectBuildings = false;}


				foundUnits.Add(obj);
		}

		Debug.Log (foundUnits.Count );

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



}
