﻿using UnityEngine;
using System.Collections;

public class BuildStructure:  UnitProduction {


	private Selected mySelect;

	//private BuildingInteractor myInteractor;

	private UnitManager myManager;
	private RaceManager racer;


	private bool Morphing = false;
	private HealthDisplay HD;
	private BuildManager buildMan;
	Vector3 targetLocation;

	private UnitManager inConstruction;
	private BuildingInteractor builder;

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.building;
	}


	// Use this for initialization
	void Start () {
		buildMan = GetComponent<BuildManager> ();
		myManager = this.gameObject.GetComponent<UnitManager> ();

		mySelect = GetComponent<Selected> ();
	
		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ();
		HD = GetComponentInChildren<HealthDisplay>();
	}

	// Update is called once per frame
	void Update () {
		if (Morphing) {


			float percent = builder.construct (Time.deltaTime / buildTime);
			if (percent >= 1) {
				mySelect.updateCoolDown (0);
				HD.stopBuilding ();
				Morphing = false;
				createUnit ();


				RaycastHit hit;		

				if (Physics.Raycast ((this.gameObject.transform.position + Vector3.right * 14 ), Vector3.down,  out hit, Mathf.Infinity, ~(1 << 16))) {
					
						Vector3 attackMovePoint = hit.point;
						myManager.GiveOrder (Orders.CreateMoveOrder (attackMovePoint));
					}
			} else {
				mySelect.updateCoolDown (percent);

			}

			
		}

	} 
	// this only halts construction
	public void cancel()
	{Debug.Log ("Canceling the build");
		mySelect.updateCoolDown (0);
		HD.stopBuilding ();
		Morphing = false;
		myManager.setStun (false, this);

		myManager.changeState(new DefaultState());

		//builder.cancelBuilding ();
		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}
	}

	public override void setAutoCast(bool offOn){}

	public void setBuildSpot(Vector3 buildSpot)
	{targetLocation = buildSpot;
	}


	public override void DeQueueUnit()
	{Debug.Log ("Dequeing");
		myCost.refundCost ();
		PopUpMaker.CreateGlobalPopUp ("+" + myCost.ResourceOne, Color.white, this.transform.localPosition + Vector3.up * 8);

	}


	public override void cancelBuilding ()
	{	
		racer.stopBuildingUnit (this);
		HD.stopBuilding ();
		mySelect.updateCoolDown (0);

		Morphing = false;
		Debug.Log ("morphing is " + Morphing + "   " + this.gameObject) ;
		//myCost.refundCost ();
		//racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply, null);
		racer.stopBuildingUnit (this);
		myManager.setStun (false, this);
		myManager.changeState(new DefaultState());

		Destroy (inConstruction.gameObject);

		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}
	}

	public override float getProgress ()
	{return builder.getProgess();}

	override
	public continueOrder canActivate (bool showError)
	{

		continueOrder order = new continueOrder();
		if (Morphing) {
			Debug.Log (" I am morphing");

			order.nextUnitCast = true;
			order.canCast = false;
			return order;
		}

		if (!myCost.canActivate (this, order,showError)) {
			Debug.Log ("My cost is srong");
			order.canCast = false;
		}
		if (!active) {
			order.reasonList.Add (continueOrder.reason.requirement);
		}
		if (order.canCast) {
			order.nextUnitCast = false;
		}
		return order;
	}

	override
	public void Activate()
	{if (!Morphing) {
			HD.loadIMage (iconPic);

	
			//Debug.Log ("Activating");
			//myCost.payCost ();
			buildMan.buildUnit (this);
			myManager.cMover.stop ();

		//	Debug.Log ("Turnign on");
			Morphing = true;
			racer.buildingUnit (this);

			myManager.changeState (new ChannelState (),true,false);
			myManager.setStun (true, this);
			if (mySelect.IsSelected) {
				SelectedManager.main.updateUI ();
			}
			inConstruction = ((GameObject)Instantiate(unitToBuild, targetLocation + Vector3.up, Quaternion.identity)).GetComponent<UnitManager>();

			builder = inConstruction.GetComponent<BuildingInteractor> ();
			if (!builder) {
				builder = (BuildingInteractor)inConstruction.GetComponent<ArmoryInteractor> ();

			} 
				builder.startConstruction (unitToBuild, buildTime);

		
			inConstruction.setInteractor();
			inConstruction.interactor.initialize ();
			inConstruction.GetComponent<Selected> ().Initialize ();
			inConstruction.myStats.SetHealth (.02f);


		} 

		//return true;//next unit should also do this.
	}



	public override void startBuilding(){}

	public void createUnit()
	{HD.stopBuilding ();

	
		mySelect.updateCoolDown (0);
		//GameObject unit = (GameObject)Instantiate(unitToBuild, targetLocation, Quaternion.identity);

		//UnitManager tempManage = unit.GetComponent<UnitManager> ();
		//tempManage.setInteractor();
		//tempManage.interactor.initialize ();
		GameManager.main.playerList[myManager.PlayerOwner-1].UnitCreated(unitToBuild.GetComponent<UnitStats> ().supply);
		myManager.setStun (false, this);
		myManager.changeState(new DefaultState());
		racer.stopBuildingUnit (this);
		/*
		UnitManager template = unitToBuild.GetComponent<UnitManager> ();
		for (int i = 0; i < inConstruction.abilityList.Count; i++) {
	
			if (template.abilityList [i].active) {
				inConstruction.abilityList [i].active = true;
			}
			if (template.abilityList [i].enabled) {

			inConstruction.abilityList [i].enabled = true;
		}
		}*/
		//unit.GetComponent<Selected> ().Initialize ();


		buildMan.unitFinished (this);
		Morphing = false;
		if (inConstruction.GetComponent<Selected>().IsSelected || GetComponent<Selected>().IsSelected){
			SelectedManager.main.updateUI ();
		}
		inConstruction = null;

	}

	public void resumeBuilding(GameObject obj)
	{
		
		HD.loadIMage (iconPic);


		//Debug.Log ("Activating");

		buildMan.buildUnit (this);
		myManager.cMover.stop ();

		Debug.Log ("Turning on 2");
		Morphing = true;

		myManager.changeState (new ChannelState (), false, false);
		myManager.setStun (true, this);
		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}
		inConstruction = obj.GetComponent<UnitManager>();
		builder = inConstruction.GetComponent<BuildingInteractor> ();
	
	}



}
