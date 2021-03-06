﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class buildTurret :UnitProduction{



	public UnitManager manager;



	protected RaceManager racer;
	protected float timer =0;
	protected bool buildingUnit = false;


	protected Selected mySelect;
	protected HealthDisplay HD;
	public List<TurretMount> turretMounts = new List<TurretMount>();

	protected BuildManager buildMan;
	public GameObject PlaceEffect;

	public bool rapidArms;
	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.activated;
	}


	// Use this for initialization
	void Start () {
		buildMan = GetComponent<BuildManager> ();
		manager = GetComponent<UnitManager> ();
		mySelect = GetComponent<Selected> ();

		myCost.cooldown = buildTime;
		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ();
		HD = GetComponentInChildren<HealthDisplay>();

		if (rapidArms) {
			foreach (TurretMount tm in GameObject.FindObjectsOfType<TurretMount>()) {
				if (!turretMounts.Contains (tm)) {
					turretMounts.Add (tm);
				}
			
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (buildingUnit) {

			timer -= Time.deltaTime * buildRate;
			mySelect.updateCoolDown (1 - timer/buildTime);
			if (timer <= 0) {
				HD.stopBuilding ();
				mySelect.updateCoolDown (0);
				buildingUnit = false;
				buildMan.unitFinished (this);
				racer.stopBuildingUnit (this);

				foreach (Transform obj in this.transform) {

					obj.SendMessage ("DeactivateAnimation",SendMessageOptions.DontRequireReceiver);
				}

				chargeCount++;
				if (mySelect.IsSelected) {
					RaceManager.upDateUI ();
				}

			}
		}
		if(autocast){

				if (turretMounts.Count > 0) {

					turretMounts.RemoveAll (item => item == null);
					foreach (TurretMount obj in turretMounts) {

					//You may place a turret and run out halfway through this loop
						if (chargeCount == 0) {
							return;
						}

					
						if (obj.enabled == false) {
							return;}
					//Uncomment this if There is ever a unit that can carry turrets but not fire them
						//if (obj.gameObject.GetComponentInParent<TurretPickUp> ()) {

							//if (!obj.gameObject.GetComponentInParent<TurretPickUp> ().autocast) {
								
								//return;
							//}
						//}


					if (obj.turret == null && obj.lastUnPlaceTime < Time.time -2) {
							if (soundEffect) {
								audioSrc.PlayOneShot (soundEffect);
							}
							obj.placeTurret (createUnit ());
						if (PlaceEffect) {
							Instantiate (PlaceEffect, obj.transform.position, Quaternion.identity, obj.transform);
						}
				
						}
					}
	
				}
			


		}
	}

	public override void DeQueueUnit()
	{
		myCost.refundCost ();
		PopUpMaker.CreateGlobalPopUp ("+" + myCost.ResourceOne, Color.white, this.transform.localPosition + Vector3.up * 8);

	}

	public override float getProgress ()
	{return (1 - timer/buildTime);}


	public void turnOffAutoCast()
	{autocast = false;
		
	}

	public override void setAutoCast(bool offOn)
	{autocast = offOn;
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (rapidArms) {
			return;}
		//need to set up calls to listener components
		//this will need to be refactored for team games
		if (other.isTrigger) {
				return;}


		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

			if (manage == null) {

		
			return;
		}

			if (manage.PlayerOwner == manager.PlayerOwner) {

				turretMounts.RemoveAll (item => item == null);
				foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
					if (mount) {
		
						turretMounts.Add (mount);
					}
				}

			

		}
	}

	public void addMount(TurretMount tm)
	{if (!turretMounts.Contains (tm)) {
			turretMounts.Add (tm);
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (rapidArms) {
			return;}

		//need to set up calls to listener components
		//this will need to be refactored for team games
		if (other.isTrigger) {
			return;}

		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

		if (manage == null) {
			return;
		}

		if (manage.PlayerOwner == manager.PlayerOwner) {
			
				foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
					if (mount) {

						turretMounts.Remove(mount);
					}
				}
			


		}
	}







	public override void cancelBuilding ()
	{HD.stopBuilding ();
		timer = 0;
		buildingUnit = false;
		//myCost.refundCost ();
		GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().UnitDied(unitToBuild.GetComponent<UnitStats>().supply, null);
		mySelect.updateCoolDown (0);
		racer.stopBuildingUnit (this);

		foreach (Transform obj in this.transform) {

			obj.SendMessage ("DeactivateAnimation",SendMessageOptions.DontRequireReceiver);
		}
	}



	override
	public continueOrder canActivate (bool showError)
	{continueOrder order = new continueOrder ();
		

		order.nextUnitCast = false;

		if (!myCost.canActivate (this, order,showError)) {
			order.canCast = false;
		}

		if (!active) {
			order.reasonList.Add (continueOrder.reason.requirement);
		}

		return order;

	}

	override
	public void Activate()
	{
		if (myCost.canActivate (this)) {

			if (buildMan.buildUnit (this)) {
				myCost.payCost ();
				myCost.resetCoolDown ();
				PopUpMaker.CreateGlobalPopUp ("-" + myCost.ResourceOne, Color.white, this.transform.localPosition + Vector3.up * 8);

			}
		}
		//return true;//next unit should also do this.
	}



	override
	public void startBuilding()
	{
		foreach (Transform obj in this.transform) {

			obj.SendMessage ("ActivateAnimation",SendMessageOptions.DontRequireReceiver);
		}

		timer = buildTime;
		GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().UnitCreated(unitToBuild.GetComponent<UnitStats>().supply);
		buildingUnit = true;
		racer.buildingUnit (this);
		HD.loadIMage(unitToBuild.GetComponent<UnitStats> ().Icon);
	}

	public void changeCharge(int n)
	{
		chargeCount += n;
		if (mySelect.IsSelected) {
			RaceManager.upDateUI ();
		}
	}

	public GameObject createUnit()
	{

		Vector3 location = new Vector3(this.gameObject.transform.position.x + 25,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z);
		chargeCount--;
		if (mySelect.IsSelected) {
			RaceManager.upDateUI ();
		}
		GameObject tur = (GameObject)Instantiate(unitToBuild, location, Quaternion.identity);
	

		//racer.applyUpgrade (tur);
		//buildingUnit = false;

	
	return tur;
	}
}
