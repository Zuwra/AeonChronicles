using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DeployTurret  : TargetAbility {


	public GameObject BloodMistObj;


	override
	public continueOrder canActivate(bool showError){

		continueOrder order = new continueOrder ();

		if (!myCost.canActivate (this)) {
			order.canCast = false;
		} else {
			order.nextUnitCast = false;
		}
		return order;
	}

	override
	public void Activate()
	{
	}  // returns whether or not the next unit in the same group should also cast it


	override
	public  void setAutoCast(bool offOn){}

	public override bool isValidTarget (GameObject target, Vector3 location){



		return true;

	}


	override
	public  bool Cast(GameObject target, Vector3 location)
	{

		if (target) {


		} else {
			myCost.payCost ();

			Vector3 pos = location;
			pos.y += 5;
			Instantiate (BloodMistObj, pos, Quaternion.identity);
		}

		return false;

	}
	override
	public void Cast(){



		if (target) {


		} else {
			myCost.payCost ();

			Vector3 pos = location;
			pos.y += 5;
			Instantiate (BloodMistObj, pos, Quaternion.identity);
		}
	}





	UnitManager manager;


	public GameObject UnitToBuild;
	protected float timer =0;
	protected bool buildingUnit = false;

	public float BuildTime;
	protected Selected mySelect;
	protected HealthDisplay HD;
	protected List<TurretMount> turretMounts = new List<TurretMount>();

	public GameObject PlaceEffect;

	public bool rapidArms;
	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.target;
	}


	// Use this for initialization
	void Start () {

		manager = GetComponent<UnitManager> ();
		mySelect = GetComponent<Selected> ();

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

			timer -= Time.deltaTime;
			mySelect.updateCoolDown (1 - timer/BuildTime);
			if (timer <= 0) {
				HD.stopBuilding ();
				mySelect.updateCoolDown (0);
				buildingUnit = false;

		

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




	public void turnOffAutoCast()
		{autocast = false;	}



	void OnTriggerEnter(Collider other)
	{
		
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

	public GameObject createUnit()
	{

		Vector3 location = new Vector3(this.gameObject.transform.position.x + 25,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z);
		chargeCount--;
		if (mySelect.IsSelected) {
			RaceManager.upDateUI ();
		}
		GameObject tur = (GameObject)Instantiate(UnitToBuild, location, Quaternion.identity);


		return tur;
	}


	public void changeCharge(int n)
	{
		chargeCount += n;
		if (mySelect.IsSelected) {
			RaceManager.upDateUI ();
		}
	}


}
