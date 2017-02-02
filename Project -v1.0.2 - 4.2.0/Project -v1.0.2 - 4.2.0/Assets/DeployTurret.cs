﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DeployTurret  : TargetAbility {


	UnitManager manager;

	public GameObject UnitToBuild;

	protected float timer =0;
	protected bool buildingUnit = false;


	protected Selected mySelect;
	protected HealthDisplay HD;
	protected List<TurretMount> turretMounts = new List<TurretMount>();
	public float ReplicationTime;
	public GameObject PlaceEffect;

	public TurretMount myMount;
	GameObject currentTurret;

	public List<Sprite> turretIcons;

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.target;

	}


	// Use this for initialization
	void Start () {

		manager = GetComponent<UnitManager> ();
		mySelect = GetComponent<Selected> ();

		HD = GetComponentInChildren<HealthDisplay>();

		InvokeRepeating ("AutoCasting", 1, .25f);
	}

	// Update is called once per frame
	void AutoCasting () {

		if(autocast){

			if (turretMounts.Count > 0) {


				foreach (TurretMount obj in turretMounts) {
					if (!obj) {
						continue;}

					//You may place a turret and run out halfway through this loop
					if (chargeCount == 0) {
						return;
					}


					if (obj.enabled == false) {
						return;}
			

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

	Coroutine currentReplciation;

	public void TurretPlaced()
	{active = true;
		
		currentTurret = myMount.turret;
		IWeapon weap = currentTurret.GetComponent<IWeapon> ();
		if(weap)
		{manager.removeWeapon (weap);}
		UnitManager turrManage = currentTurret.GetComponent<UnitManager> ();


		turrManage.enabled = false;

		currentReplciation = StartCoroutine (replicateTurrets());


		if (turrManage.UnitName.Contains("Mini")) {
			iconPic = turretIcons [1];
		}
		else if (turrManage.UnitName.Contains("Imperio")) {
			iconPic = turretIcons [2];
		}
		else if (turrManage.UnitName.Contains("Repair")) {
			iconPic = turretIcons [3];
		}
		else if (turrManage.UnitName.Contains( "Fire")) {
			iconPic = turretIcons [4];
		}
		changeCharge (0);
	}

	public void TurretRemoved()
	{
		active = false;

		currentTurret = null;
		if (currentReplciation != null) {
			StopCoroutine (currentReplciation);
		}
		iconPic = turretIcons [0];
		changeCharge (0);
	}

	IEnumerator replicateTurrets()
	{
		if (chargeCount == 0) {
			myCost.startCooldown ();
		}
		yield return new WaitForSeconds (ReplicationTime);
		changeCharge (1);
		if (chargeCount < 2) {
			StartCoroutine (replicateTurrets());
		}
	}


	public override bool isValidTarget (GameObject target, Vector3 location){

		if (!target ||  target.name == "Terrain") {
			return true;
		}
		return false;

	}


	public void turnOffAutoCast()
	{autocast = false;

	}

	public override void setAutoCast(bool offOn)
	{autocast = offOn;

	}

	void OnTriggerEnter(Collider other)
	{
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








	override
	public continueOrder canActivate (bool showError)
	{continueOrder order = new continueOrder ();


		order.nextUnitCast = false;

		if (chargeCount == 0) {
			order.canCast = false;}

		if (!active) {
			order.reasonList.Add (continueOrder.reason.requirement);
		}

		return order;

	}

	override
	public void Activate()
	{
	}

	override
	public  bool Cast(GameObject target, Vector3 location)
	{

		if (chargeCount == 2) {
			currentReplciation = StartCoroutine (replicateTurrets ());
		} else if (chargeCount == 1) {
			myCost.startCooldown ();
		}

		changeCharge (-1);

		Vector3 pos = location;

		GameObject proj = (GameObject)Instantiate (UnitToBuild, pos+ Vector3.up * .5f, Quaternion.identity);
		Instantiate (PlaceEffect,  pos+ Vector3.up * .5f, Quaternion.identity);
		currentTurret.GetComponent<UnitManager> ().enabled = true;
		GameObject newTurret =  (GameObject)Instantiate (currentTurret, pos + Vector3.up *2f, Quaternion.identity);
		newTurret.transform.SetParent (proj.transform);
	
		UnitManager turrManage = newTurret.GetComponent<UnitManager> ();

		turrManage.setInteractor( newTurret.AddComponent<StandardInteract> ());
		turrManage.changeState(new turretState(turrManage));
		newTurret.GetComponent<UnitStats> ().otherTags.Add (UnitTypes.UnitTypeTag.Static_Defense);
		currentTurret.GetComponent<UnitManager> ().enabled = false;
		return false;

	}
	override
	public void Cast(){

		if (chargeCount == 2) {
			currentReplciation =  StartCoroutine (replicateTurrets());
		}
		else if (chargeCount == 1) {
			myCost.startCooldown ();
		}

		changeCharge (-1);

		Vector3 pos = location;
	
		GameObject proj = (GameObject)Instantiate (UnitToBuild, pos+ Vector3.up * .5f, Quaternion.identity);
		Instantiate (PlaceEffect,  pos+ Vector3.up * .5f, Quaternion.identity);
		currentTurret.GetComponent<UnitManager> ().enabled = true;
		GameObject newTurret =  (GameObject)Instantiate (currentTurret, pos + Vector3.up *2f, Quaternion.identity);
		newTurret.transform.SetParent (proj.transform);
	
		UnitManager turrManage = newTurret.GetComponent<UnitManager> ();

		turrManage.setInteractor( newTurret.AddComponent<StandardInteract> ());
		turrManage.changeState(new turretState(turrManage));
		newTurret.GetComponent<UnitStats> ().otherTags.Add (UnitTypes.UnitTypeTag.Static_Defense);
		currentTurret.GetComponent<UnitManager> ().enabled = false;


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
		changeCharge (-1);

		GameObject tur = (GameObject)Instantiate(currentTurret, location, Quaternion.identity);

		if (chargeCount == 2) {
			currentReplciation = StartCoroutine (replicateTurrets ());
		} else if (chargeCount == 1) {
			myCost.startCooldown ();
		}

		changeCharge (-1);

		return tur;
	}
}
