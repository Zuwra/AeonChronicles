﻿using UnityEngine;
using System.Collections;

public class AetherOvercharge : MonoBehaviour, Notify{

	[Tooltip("Perecent - between 0 and 1")]
	public float rechargeAmount;
	public float duration;
	private bool onTarget;
	private UnitManager myman;

	[Tooltip("Perecent - between 0 and 1")]
	public float attackSpeed;
	[Tooltip("Perecent - between 0 and 1")]
	public float attackDamage;

	private float nextActionTime;
	private float startTime;
	bool spellHasBegun;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (onTarget) {
			if (Time.time > startTime + duration) {
				endSpell ();
				Destroy (this);
			}
			if (spellHasBegun && Time.time > nextActionTime) {
	
				nextActionTime += 1;

				GetComponent<DayexaShield> ().stopRecharge ();
				myman.myStats.changeEnergy (-myman.myStats.MaxEnergy / duration);
				if(myman.myStats.currentEnergy <= 0)
				{
					endSpell();
				}
			}
		}
	}

	public void initialize(UnitManager unitman,float chargeAmount, float dur, float AS, float dam)
	{nextActionTime = Time.time + 1;
		attackSpeed = AS;
		attackDamage = dam;
		onTarget = true;
		myman = unitman;
		rechargeAmount = chargeAmount;
		startTime = Time.time;
		duration = dur;
		unitman.myStats.changeEnergy (unitman.myStats.MaxEnergy * rechargeAmount);

		foreach (IWeapon weap in unitman.myWeapon) {
			weap.triggers.Add (this);
			weap.changeAttack (attackDamage, 0, false, this);
			weap.changeAttackSpeed (attackSpeed, 0, false, this);
			GatlingGun gg = weap.GetComponent<GatlingGun> ();
			if (gg) {
				gg.MinimumPeriod *= -attackSpeed;
			}
		}

		Debug.Log ("Final speed is " + unitman.myWeapon[0].attackPeriod);

	}

	public void trigger(GameObject src, GameObject proj, GameObject target, float damage)
	{
		if (!spellHasBegun) {
			nextActionTime = Time.time + 1;
			spellHasBegun = true;
			startTime = Time.time;
			StartCoroutine (delayRemove());
		}
	}

	IEnumerator delayRemove()
	{
		yield return new WaitForSeconds (.1f);
		foreach (IWeapon weap in myman.myWeapon) {
			weap.triggers.Remove (this);
		}
	}



	public void endSpell()
	{
		Debug.Log ("Ending spell");
		GetComponent<DayexaShield> ().startRecharge ();

		foreach (IWeapon weap in myman.myWeapon) {
			if (weap) {
				weap.triggers.Remove (this);
				weap.removeAttackBuff (this);
				weap.removeAttackSpeedBuff (this);

				GatlingGun gg = weap.GetComponent<GatlingGun> ();
				if (gg) {
					gg.MinimumPeriod /= -attackSpeed;
				}
			}
		}


	}

	//Used in the casting object
	public void OnTriggerEnter(Collider other)
	{
		if (!onTarget) {
			UnitManager man = other.GetComponent<Collider>().gameObject.GetComponent<UnitManager> ();
			if (man && man.PlayerOwner == 1 && !man.myStats.isUnitType(UnitTypes.UnitTypeTag.Turret)) {
				if (man.gameObject.GetComponent<AetherOvercharge> ()) {
					return;}

				if (man.gameObject.GetComponent<DayexaShield> ()) {
					AetherOvercharge charge = man.gameObject.AddComponent<AetherOvercharge> ();
					charge.initialize (man, rechargeAmount, duration, attackSpeed, attackDamage);
				}
			}
		}


	}
}
