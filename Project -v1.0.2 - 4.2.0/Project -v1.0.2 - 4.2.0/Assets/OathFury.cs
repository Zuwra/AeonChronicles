﻿using UnityEngine;
using System.Collections;

public class OathFury : Ability, Modifier, Notify {
	// Unit modifier that make them move Life Steal more the less health they have
	private IWeapon myWeapon;

	private float initialLifeSteal;
	private UnitStats myStats;

	private LifeSteal myStealer;

	// Use this for initialization
	void Start () {
		myWeapon = GetComponent<IWeapon> ();

		myStats = GetComponent<UnitStats> ();
		myStats.addModifier (this);
		myWeapon.triggers.Add (this);
		myStealer = GetComponent<LifeSteal> ();
		initialLifeSteal = myStealer.percentage;
	}

	// Update is called once per frame
	void Update () {

	}

	public override continueOrder canActivate(bool showError){
		return new continueOrder ();
	}
	public  override void Activate(){
	}  // returns whether or not the next unit in the same group should also cast it
	public  override void setAutoCast(){}





	public void trigger(GameObject source, GameObject projectile,GameObject target, float damage)
	{
		myStealer.percentage = initialLifeSteal + (1 - (myStats.health / myStats.Maxhealth));


	}

	public float modify(float damage, GameObject source)
	{

		myStealer.percentage = initialLifeSteal + (1 - (myStats.health / myStats.Maxhealth));


		return damage;
	}

}