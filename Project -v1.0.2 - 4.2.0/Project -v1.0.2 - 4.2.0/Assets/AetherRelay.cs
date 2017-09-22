using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.SoundManagerNamespace;
public class AetherRelay : Ability{

	List<DayexaShield> shieldList = new List<DayexaShield> ();
	List<UnitStats> enemyStats = new List<UnitStats> ();
	public float energyChargeRate;
	UnitManager manager;

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


		InvokeRepeating ("UpdateAether", 1, 1);


		if (GetComponent<DayexaShield>().maxDamagePerSec > 0) {
			Descripton += " Maximum of " + GetComponent<DayexaShield>().maxDamagePerSec +" damage per second can be taken while field is active.";
		}
	}
	
	// Update is called once per frame
	void UpdateAether () {

			if (turnedOn) {
				if (manager.myStats.currentEnergy <= 20) {
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
					if (soundEffect) {
						SoundManager.PlayOneShotSound(audioSrc, soundEffect);
					}


					foreach (UnitStats us in enemyStats) {
						if (us) {
							if (Vector3.Distance (us.transform.position, this.transform.position) < 40) {
								float actual = us.TakeDamage (damageRate, this.gameObject, DamageTypes.DamageType.Regular);
								manager.myStats.veternStat.UpdamageDone (actual);
							}
						}
					}
				}

			} else {

			float total = 0;
				foreach (DayexaShield ds in shieldList) {
					if (ds) {
						if (ds.myStats.currentEnergy < ds.myStats.MaxEnergy) {
							float actual = ds.myStats.changeEnergy (energyChargeRate);
							Instantiate (chargeEffect, ds.transform.position, Quaternion.identity);
						total += actual;
							
						}
					}
				}
			manager.myStats.veternStat.UpEnergy(total);
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

		} else {
			
			myEffect.stopEffect ();
		
		}

		if (select.IsSelected) {
			RaceManager.upDateAutocast();
		}

		//return true;//next unit should also do this.
	}




}
