using UnityEngine;
using System.Collections;

public class HookFury : MonoBehaviour, Modifier, Notify {

	// Unit modifier that make them attack faster the less health they have
	private IWeapon myWeapon;

	private float initialAttackSpeed;
	private UnitStats myStats;
	// Use this for initialization
	void Start () {
		myWeapon = GetComponent<IWeapon> ();
		initialAttackSpeed = myWeapon.attackPeriod;
		myStats = GetComponent<UnitStats> ();
		myStats.addModifier (this);
		myWeapon.triggers.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public void trigger(GameObject source, GameObject projectile,GameObject target, float damage)
	{
		myWeapon.attackPeriod = (initialAttackSpeed -.2f) * (myStats.health / myStats.Maxhealth) + .2f;
		if (myWeapon.attackPeriod > initialAttackSpeed) {
			myWeapon.attackPeriod = initialAttackSpeed;
		}

	}

	public float modify(float damage, GameObject source)
	{

		myWeapon.attackPeriod = (initialAttackSpeed -.2f) * (myStats.health / myStats.Maxhealth) + .2f;
		if (myWeapon.attackPeriod > initialAttackSpeed) {
			myWeapon.attackPeriod = initialAttackSpeed;
		}
		return damage;
	}

}
