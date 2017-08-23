using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathInjector :MonoBehaviour,  Notify {

	// Unit modifier that make them attack faster the less health they have

	public bool onTarget;
	public float DamageTime =15;
	public float DamageAmount =15;
	private UnitStats myStats;

	public string toSpawn;
	// Use this for initialization
	void Start () {

		myStats = GetComponent<UnitStats> ();

		if (!onTarget) {

			foreach (IWeapon weap in GetComponents<IWeapon> ()) {
				weap.addNotifyTrigger (this);
			}
		}
	}




	public float trigger(GameObject source, GameObject projectile,UnitManager target, float damage)
	{
		//Need to fix this if this script is on both allied and enemy units
		DeathInjector inject = target.gameObject.GetComponent<DeathInjector> ();
		if (!inject) {
			inject = target.gameObject.AddComponent<DeathInjector> ();

		}
		inject.toSpawn = toSpawn;
		inject.DamageOverTime (DamageAmount,DamageTime);

		return damage;
	}

	public void DamageOverTime(float damagePerSecond, float duration)
	{
		DamageTime = duration;
		DamageAmount = damagePerSecond;
		onTarget = true;
		if (currentDamager != null) {
			StopCoroutine (currentDamager);}
		currentDamager = StartCoroutine (OverTime());
	}

	Coroutine currentDamager;

	IEnumerator OverTime()
	{
		for (float i = 0; i < DamageTime; i++) {
			yield return new WaitForSeconds (1);
			myStats.TakeDamage (DamageAmount, null, DamageTypes.DamageType.True);
		}
		Destroy (this);
	}

	public void Dying()
	{
		//Debug.Log ("Dying " + onTarget + "  -" + toSpawn);
		if (onTarget ) {
			Instantiate (Resources.Load<GameObject>(toSpawn), this.transform.position + Vector3.up* 3, Quaternion.identity);
		}
	}

}
