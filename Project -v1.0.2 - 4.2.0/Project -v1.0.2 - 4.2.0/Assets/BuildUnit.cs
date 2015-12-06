﻿using UnityEngine;
using System.Collections;

public class BuildUnit :  Ability {



	public GameObject unitToBuild;

	public float buildTime;

	private float timer =0;
	private bool buildingUnit = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (buildingUnit) {

			timer -= Time.deltaTime;
			if(timer <=0)
			{
				buildingUnit = false;
				createUnit();
			}
		}
	
	}

	public void cancelBuild ()
		{
		timer = 0;
		buildingUnit = false;
		myCost.refundCost ();
		GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().UnitDied(unitToBuild.GetComponent<UnitStats>().supply);
	}



	override
		public bool canActivate ()
	{
		if (buildingUnit) {

			return false;}
	
		return myCost.canActivate ();

	}
	
	override
		public bool Activate()
	{
		if (myCost.canActivate ()) {

			myCost.payCost();


			timer = buildTime;
			GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().UnitCreated(unitToBuild.GetComponent<UnitStats>().supply);
			buildingUnit = true;
		}
		return true;//next unit should also do this.
	}


	public void createUnit()
	{
		
		Vector3 location = new Vector3(this.gameObject.transform.position.x + 25,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z);
		
		Instantiate(unitToBuild, location, Quaternion.identity);
		

		buildingUnit = false;
	}
}
