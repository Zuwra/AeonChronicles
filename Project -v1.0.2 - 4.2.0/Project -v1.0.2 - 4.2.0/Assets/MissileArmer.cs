using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileArmer :Ability{

	public UnitManager manager;

	public bool missiles;
	public bool nitro;

	public bool shields;
	public float shieldRate;
	public GameObject shieldglobe;
	public GameObject OverchargeBoost;

	public List<missileSalvo> missileList = new List<missileSalvo> ();

	public List<StimPack> stimList = new List<StimPack>();
	public List<DayexaShield> shieldList = new List<DayexaShield> ();

	private float nextActionTime;



	// Use this for initialization
	void Start () {

		manager = GetComponent<UnitManager> ();
		nextActionTime = Time.time + 1.7f;
	}


	// Update is called once per frame
	void Update () {

		if (!active) {
			return;}
		
		if (nextActionTime < Time.time) {
			nextActionTime = Time.time + 1.7f;

			if (missiles) {
				missileList.RemoveAll (item => item == null);
				foreach (missileSalvo salv in missileList) {
					if (salv.chargeCount < salv.maxRockets) {
						salv.chargeCount++;
				
					}
				}
			}
	
			if (nitro) {
				stimList.RemoveAll (item => item == null);
				foreach (StimPack stim in stimList) {
					if (stim.chargeCount < 3) {
						GameObject obj = (GameObject)Instantiate (OverchargeBoost, this.transform.position, Quaternion.identity);
						if (stim) {
							obj.GetComponent<ShieldGlobe> ().target = stim.gameObject;
							obj.GetComponent<ShieldGlobe> ().isOverCharge = true;
						}
				

					}
				}
			}

			if (shields) {
				if (shieldglobe) {
					shieldList.RemoveAll (item => item == null);
					foreach (DayexaShield ds in shieldList) {
				
						if (ds.myStats.currentEnergy < ds.myStats.MaxEnergy) {
							GameObject obj = (GameObject)Instantiate (shieldglobe, this.transform.position, Quaternion.identity);
							if (ds) {
								obj.GetComponent<ShieldGlobe> ().target = ds.gameObject;
							}
							//ds.myStats.changeEnergy (shieldRate);
						}
			
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
				if (salvo) {
					missileList.Remove (salvo);
				}
			
			}	


			if (nitro) {


				StimPack stim = other.gameObject.GetComponent<StimPack> ();
				if (stim) {
					stimList.Remove (stim);
				}

			}
			if (shields) {
				DayexaShield s = other.gameObject.GetComponent<DayexaShield> ();
				if (s) {
					shieldList.Remove (s);
				}

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
				if (salvo) {
					missileList.Add (salvo);
				}
			}	


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
