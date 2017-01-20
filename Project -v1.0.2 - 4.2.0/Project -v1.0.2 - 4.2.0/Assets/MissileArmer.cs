using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileArmer :Ability{

	public UnitManager manager;

	public bool nitro;

	public bool shields;
	public float shieldRate;
	public GameObject shieldglobe;
	public GameObject OverchargeBoost;


	public List<StimPack> stimList = new List<StimPack>();
	public List<DayexaShield> shieldList = new List<DayexaShield> ();


	//private float nextActionTime;



	// Use this for initialization
	void Start () {

		manager = GetComponent<UnitManager> ();
		InvokeRepeating ("UpdateCharges", 1, 1.8f);
	}


	// Update is called once per frame
	void UpdateCharges () {

		if (!active) {
			return;}
		

	/*
			if (nitro) {
				stimList.RemoveAll (item => item == null);
				foreach (StimPack stim in stimList) {
					if (stim.chargeCount < 3) {
						GameObject obj = (GameObject)Instantiate (OverchargeBoost, this.transform.position, Quaternion.identity);
						if (stim) {
							
							obj.GetComponent<ShieldGlobe> ().setInfo (stim.gameObject, true);
						}
				
						break;
					}
				}
			}
*/
			if (shields) {
				if (shieldglobe) {


					foreach (DayexaShield ds in shieldList) {
					if (!ds) {
						continue;}
						if (ds.myStats.currentEnergy < ds.myStats.MaxEnergy) {
							GameObject obj = (GameObject)Instantiate (shieldglobe, this.transform.position, Quaternion.identity);
							if (ds) {
								obj.GetComponent<ShieldGlobe> ().setInfo (ds.gameObject, false);
							
							}
							break;
							
						}
			
					}
				}
			}

	}

	public  override continueOrder canActivate(bool showError){
		return new continueOrder ();
	}
	public override void Activate(){
	}
	public  override void setAutoCast(bool offOn)
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

		


			if (nitro) {


				StimPack stim = other.gameObject.GetComponent<StimPack> ();
				if (stim) {
					stimList.Remove (stim);
				}

			}

				DayexaShield s = other.gameObject.GetComponent<DayexaShield> ();
				if (s) {
					shieldList.Remove (s);
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

			shieldList.RemoveAll (item => item == null);

			if (nitro) {

				//Debug.Log ("Adding Nitro " + other.gameObject);
				StimPack stim = other.gameObject.GetComponent<StimPack> ();
				if (stim) {
					stimList.Add (stim);
				}


			}
			if (shields) {
				DayexaShield s = other.gameObject.GetComponent<DayexaShield> ();
				if (s) {
					shieldList.Add (s);
				}
			}
			}



	}



}
