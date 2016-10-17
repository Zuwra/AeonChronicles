using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AetherRelay : Ability{

	List<DayexaShield> shieldList = new List<DayexaShield> ();
	List<UnitStats> enemyStats = new List<UnitStats> ();
	public float energyChargeRate;
	UnitManager manager;
	float nextActionTime;
	private Selected select;
	public MultiShotParticle myEffect;
	public GameObject chargeEffect;

	public float damageRate;
	bool turnedOn;
	// Use this for initialization
	void Start () {
		myType = type.activated;
		select = GetComponent<Selected> ();
		manager = GetComponent<UnitManager> ();
		nextActionTime = Time.time + 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextActionTime ) {

			nextActionTime = Time.time + 1f;
		

			if (turnedOn) {
				if (manager.myStats.currentEnergy <= 0) {
					turnedOn = !turnedOn;
					autocast = false;
					myEffect.stopEffect ();
					if (select.IsSelected) {
						RaceManager.upDateAutocast();
					}

				}
				else{
					manager.getUnitStats ().TakeDamage (.1f, this.gameObject,DamageTypes.DamageType.Regular);
					manager.myStats.changeEnergy (-19.9f);



					foreach (UnitStats us in enemyStats) {
						if (us) {
							float actual = us.TakeDamage (damageRate, this.gameObject, DamageTypes.DamageType.Regular);
							manager.myStats.veternStat.UpdamageDone (actual);
						}
					}
				}

			} else {

				foreach (DayexaShield ds in shieldList) {
					if (ds) {
						if (ds.myStats.currentEnergy < ds.myStats.MaxEnergy) {
							float actual = ds.myStats.changeEnergy (energyChargeRate);
							Instantiate (chargeEffect, ds.transform.position, Quaternion.identity);
							manager.myStats.veternStat.energyGained += actual;
						}
					}
				}
			}

		

		}
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

			DayexaShield s = other.gameObject.GetComponent<DayexaShield> ();
			if (s) {
				shieldList.Remove (s);
			}


		} else if (enemyStats.Contains (manage.getUnitStats ())) {
			enemyStats.Remove (manage.getUnitStats ());
		
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

				DayexaShield s = other.gameObject.GetComponent<DayexaShield> ();
				if (s) {
					shieldList.Add (s);
				}
		}
		else if (!enemyStats.Contains (manage.getUnitStats ())) {
			enemyStats.Add (manage.getUnitStats ());

		}

	}

	public override void setAutoCast(bool offOn){
	}


	override
	public continueOrder canActivate (bool showError)
	{

		continueOrder order = new continueOrder ();


		if (manager.myStats.currentEnergy < 20) {
			order.canCast = false;
			return order;}
		
		return order;
	}

	override
	public void Activate()
	{
		turnedOn = !turnedOn;
		autocast = turnedOn;
		if (turnedOn) {
			manager.getUnitStats ().TakeDamage (.1f, this.gameObject, DamageTypes.DamageType.Regular);
			manager.getUnitStats ().changeEnergy (.1f);
			myEffect.continueEffect ();
			myCost.payCost ();
			nextActionTime = Time.time + .02f;
		} else {
			
			myEffect.stopEffect ();
		
		}

		if (select.IsSelected) {
			RaceManager.upDateAutocast();
		}

		//return true;//next unit should also do this.
	}




}
