using UnityEngine;
using System.Collections;

public class OathFury : MonoBehaviour, Modifier, Notify {
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