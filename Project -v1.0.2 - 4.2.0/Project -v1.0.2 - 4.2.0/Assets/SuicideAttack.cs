using UnityEngine;
using System.Collections;

public class SuicideAttack : MonoBehaviour, Notify {

	void Start()
	{
		GetComponent<IWeapon> ().addNotifyTrigger (this);
	}

	public float trigger(GameObject source, GameObject projectile,UnitManager target, float damage)	{

		StartCoroutine (waitForFrame());
		return damage;
	}

	IEnumerator waitForFrame()
	{
		yield return new WaitForSeconds(.1f);
		GetComponent<UnitStats> ().kill (this.gameObject);
	}
}
