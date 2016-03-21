﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileArmer :Ability{

	public UnitManager manager;

	public bool missiles;
	public bool nitro;
	public bool repairs;

	public List<missileSalvo> missileList = new List<missileSalvo> ();
	public List<repairReturn> repairList = new List<repairReturn>();
	public List<StimPack> stimList = new List<StimPack>();

	private float nextActionTime;
	private int changeNum =0;
	// Use this for initialization
	void Start () {
		manager = GetComponent<UnitManager> ();
		nextActionTime = Time.time + 1;
	}


	// Update is called once per frame
	void Update () {


		if (nextActionTime < Time.time) {
			nextActionTime += 1;

			changeNum = 0;
			foreach (missileSalvo salv in missileList) {
				if (salv.chargeCount < salv.maxRockets) {
					salv.chargeCount++;
					changeNum++;
				}
			}

			foreach (repairReturn rep in repairList) {
				if (rep.chargeCount < rep.maxRepair) {
					rep.chargeCount += 100;
					if (rep.chargeCount < rep.maxRepair) {
						rep.chargeCount = rep.maxRepair;
						changeNum++;
					}
				}
			}

			foreach (StimPack stim in stimList) {
				if (stim.chargeCount < 3) {
					stim.chargeCount++;
					changeNum++;
				}
			}
			if (changeNum > 0) {
				RaceManager.upDateUI ();
			}
		}

	}

	public  override continueOrder canActivate(){
		return new continueOrder ();
	}
	public override void Activate(){
	}
	public  override void setAutoCast()
	{
	}


	void OnTriggerExit(Collider other)
	{
		if (other.isTrigger) {
			return;}


		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

		if (manage == null) {
			return;
		}

		if (manage.PlayerOwner == manager.PlayerOwner) {

			if (missiles) {
				missileSalvo salvo = other.gameObject.GetComponent<missileSalvo> ();
				if (salvo)
					missileList.Remove (salvo);
			
			}	

			if (repairs) {
				repairReturn repair = other.gameObject.GetComponent<repairReturn> ();
				if (repair) {
					repairList.Remove (repair);
				}

			}

			if (nitro) {


				StimPack stim = other.gameObject.GetComponent<StimPack> ();
				if (stim) {
					stimList.Remove (stim);}

			}

		}


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

			if (missiles) {

				//Debug.Log ("Adding missile " + other.gameObject);
				missileSalvo salvo = other.gameObject.GetComponent<missileSalvo> ();
				if(salvo)
				missileList.Add (salvo);
			}	

			if (repairs) {

				//Debug.Log ("Adding Repair " + other.gameObject);
				repairReturn repair = other.gameObject.GetComponent<repairReturn> ();
				if(repair)
				repairList.Add (repair);
			}

			if (nitro) {

				//Debug.Log ("Adding Nitro " + other.gameObject);
				StimPack stim = other.gameObject.GetComponent<StimPack> ();
				if(stim)
				stimList.Add (stim);


			}
		
			}



	}



}
