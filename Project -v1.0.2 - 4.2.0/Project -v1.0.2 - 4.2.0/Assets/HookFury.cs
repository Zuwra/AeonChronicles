﻿using UnityEngine;
using System.Collections;

public class HookFury : MonoBehaviour, Modifier, Notify {

	// Unit modifier that make them attack faster the less health they have
	private IWeapon myWeapon;

	private UnitStats myStats;
	// Use this for initialization
	void Start () {
		myWeapon = GetComponent<IWeapon> ();
	
		myStats = GetComponent<UnitStats> ();
		myStats.addModifier (this);
		myWeapon.triggers.Add (this);
	}
	



	public void trigger(GameObject source, GameObject projectile,UnitManager target, float damage)
	{
		myWeapon.removeAttackSpeedBuff (this);
		myWeapon.changeAttackSpeed (-(.5f - (myStats.health / myStats.Maxhealth) / 2), 0, false, this);


	}

	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{
		myWeapon.removeAttackSpeedBuff (this);
		myWeapon.changeAttackSpeed (-(.5f -  (myStats.health / myStats.Maxhealth) / 2), 0, false, this);
		return damage;
	}

}
