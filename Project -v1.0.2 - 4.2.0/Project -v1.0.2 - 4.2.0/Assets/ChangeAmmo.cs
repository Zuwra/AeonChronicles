using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeAmmo : Ability {



	private Selected select;

	UnitManager myManager;
	public GameObject myAmmo;
	public IWeapon myWeapon;
	public float range;
	public float attackPeriod;
	public float attackDamage;
	public List<IWeapon.bonusDamage> bonus = new List<IWeapon.bonusDamage>();
	// Use this for initialization
	public List<UnitTypes.UnitTypeTag> cantAttackTypes = new List<UnitTypes.UnitTypeTag> ();
	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.activated;
	}

	void Start () {
		
		select = GetComponent<Selected> ();
		myManager = GetComponent<UnitManager> ();
		if (autocast) {
			Activate ();
		}
	}



	public override void setAutoCast(bool offOn){
		Activate ();
	}


	override
	public continueOrder canActivate (bool showError)
	{

		return  new continueOrder ();
	}

	override
	public void Activate()
	{
		autocast = true;
		myWeapon.projectile = myAmmo;
		myWeapon.baseDamage = attackDamage;
		myWeapon.range = range;

		myWeapon.extraDamage = bonus;
		myWeapon.cantAttackTypes = cantAttackTypes;
		foreach (ChangeAmmo ca in GetComponents<ChangeAmmo>()) {
			if (ca != this) {
				ca.autocast = false;
			}
		}
		if (select.IsSelected) {
			
				RaceManager.upDateAutocast ();
			}

	

	}



}
