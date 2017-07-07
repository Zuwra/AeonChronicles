using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thornmail : MonoBehaviour, Modifier {

	public float damagePerHit;
	private UnitStats mystats;

	private UnitManager manage;
	public bool canHitAir;
	public float Range;
	public float AllyDamageRatio = .5f;

	// Use this for initialization
	void Start () {
		manage = GetComponent<UnitManager> ();

		mystats = GetComponent<UnitStats> ();
		mystats.addModifier (this);
	}




	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{

		float damageDone = 0;
		foreach (UnitManager man in manage.enemies) {
			if (man) {
				if (!man.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
					if (canHitAir || !man.myStats.isUnitType (UnitTypes.UnitTypeTag.Air)) {
						if (man.myStats.isUnitType (UnitTypes.UnitTypeTag.Turret)) {
							continue;}
				
						if (Vector3.Distance (transform.position, manage.transform.position) <= Range) {
							damageDone += man.myStats.TakeDamage (damagePerHit, this.gameObject, DamageTypes.DamageType.Regular);		
						}
					}
				}
			}
		}
		mystats.veteranDamage (damageDone);

		foreach (UnitManager man in manage.allies) {
			if (man) {
				if (!man.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
					if (canHitAir || !man.myStats.isUnitType (UnitTypes.UnitTypeTag.Air)) {
						if (man.myStats.isUnitType (UnitTypes.UnitTypeTag.Turret)) {
							continue;}

						if (Vector3.Distance (transform.position, manage.transform.position) <= Range) {
							man.myStats.TakeDamage (damagePerHit * AllyDamageRatio, this.gameObject, DamageTypes.DamageType.Regular);		
						}
					}
				}
			}
		}

		return damage;
	}
}
