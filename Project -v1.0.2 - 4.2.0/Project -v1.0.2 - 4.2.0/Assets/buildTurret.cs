using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class buildTurret :UnitProduction{



	public UnitManager manager;



	protected RaceManager racer;
	protected float timer =0;
	protected bool buildingUnit = false;
	public enum turretType
	{
		one, two
	}

	public turretType myTurretType;
	protected Selected mySelect;
	protected HealthDisplay HD;
	protected List<TurretMount> turretMounts = new List<TurretMount>();
	protected List<TurretMountTwo> turretTwoMounts = new List<TurretMountTwo>();
	protected BuildManager buildMan;


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
	}

	// Update is called once per frame
	void Update () {
		if (buildingUnit) {

			timer -= Time.deltaTime;
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
				RaceManager.upDateUI ();

			}
		}
		if(autocast){
			if ( myTurretType ==  turretType.one) { 
				if (turretMounts.Count > 0) {
					foreach (TurretMount obj in turretMounts) {
						if (chargeCount == 0) {
							return;
						}

					
						if (obj.enabled == false) {
							return;}

						if (obj.gameObject.GetComponentInParent<TurretPickUp> ()) {

							if (!obj.gameObject.GetComponentInParent<TurretPickUp> ().autocast) {
								
								return;
							}
						}


						if (obj.turret == null ) {
							if (soundEffect) {
								audioSrc.PlayOneShot (soundEffect);
							}
							obj.placeTurret (createUnit ());

				
						}
					}
	
				}
			} else {

				if (turretTwoMounts.Count > 0) {
					foreach (TurretMountTwo obj in turretTwoMounts) {
						if (chargeCount == 0) {
							return;
						}
						if (obj.enabled == false) {
							return;}
						
						if (obj.turret == null) {
							if (soundEffect) {
								audioSrc.PlayOneShot (soundEffect);
							}
							obj.placeTurret (createUnit ());


						}
					}

				}
			
			}


		}
	}

	public override void DeQueueUnit()
	{
		myCost.refundCost ();

	}

	public override float getProgress ()
	{return (1 - timer/buildTime);}


	public void turnOffAutoCast()
	{autocast = false;
		
	}

	public override void setAutoCast()
	{autocast = !autocast;
		
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
			if ( myTurretType ==  turretType.one) {

				foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
					if (mount) {
		
						turretMounts.Add (mount);
					}
				}
			} else {
				foreach (TurretMountTwo mount in other.gameObject.GetComponentsInChildren<TurretMountTwo> ()) {
					if (mount) {

						turretTwoMounts.Add (mount);
					}
				}
			}
			

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
			if ( myTurretType ==  turretType.one) {

				foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
					if (mount) {

						turretMounts.Remove(mount);
					}
				}
			} else {
				foreach (TurretMountTwo mount in other.gameObject.GetComponentsInChildren<TurretMountTwo> ()) {
					if (mount) {

						turretTwoMounts.Remove(mount);
					}
				}
			}


		}
	}







	public override void cancelBuilding ()
	{HD.stopBuilding ();
		timer = 0;
		buildingUnit = false;
		myCost.refundCost ();
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


	public GameObject createUnit()
	{

		Vector3 location = new Vector3(this.gameObject.transform.position.x + 25,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z);
		chargeCount--;
		RaceManager.upDateUI ();
		GameObject tur = (GameObject)Instantiate(unitToBuild, location, Quaternion.identity);

		racer.applyUpgrade (tur);
		//buildingUnit = false;

	
	return tur;
	}
}
