﻿using UnityEngine;
using System.Collections;

public class BountyHunter : MonoBehaviour, KillModifier {
	//Class used to boost unit stats when it gets a kill

	public float health;
	public float attackPeriodDecrease;
	public float armor;
	public float damage;
	public float cooldownDecrease;

	private UnitStats myStats;
	private IWeapon myWeap;
	private UnitManager manage;

	// Use this for initialization
	void Start () {
		manage = GetComponent<UnitManager> ();
		myStats = GetComponent<UnitStats> ();
		myWeap = GetComponent<IWeapon> ();
		myStats.killMods.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void incKill()
	{
		myStats.Maxhealth += health;
		myStats.heal (health);

		myStats.changeArmor (armor);
		myWeap.changeDamage (damage);
		myWeap.changeAttackSpeed (attackPeriodDecrease);

		foreach (Ability ab in manage.abilityList) {
			ab.myCost.cooldown -= cooldownDecrease;
		}
	}


}
