using UnityEngine;
using System.Collections;

public class mortarPod : MonoBehaviour, Notify {




	public float totalShots;
	public float reloadRate;
	private float shotCount;
	private IWeapon weapon;

	public bool FireAll;

	private float nextActionTime;
	Selected HealthD;

	// Use this for initialization
	void Start () {

		HealthD = GetComponentInChildren<Selected> ();

		shotCount = totalShots;
		weapon = this.gameObject.GetComponent<IWeapon> ();
		nextActionTime = Time.time;

		weapon.triggers.Add (this);

		if (FireAll) {
			weapon.attackPeriod = .01f;
		}


	}


	public void toggleFireAll()
	{FireAll = ! FireAll;


	}


	Coroutine loading;

	IEnumerator loadShots()
	{
		while (shotCount < totalShots) {
			yield return new WaitForSeconds (reloadRate - .01f);
			shotCount++;
			HealthD.updateCoolDown (shotCount / totalShots);
			if (shotCount > 1) {
				weapon.attackPeriod = .1f;
			}
		}
		loading = null;
	}

	public float trigger(GameObject source, GameObject proj, UnitManager target, float damage)
		{
		shotCount --;
		if (loading == null) {
			loading = StartCoroutine (loadShots ());
		}

		HealthD.updateCoolDown (shotCount / totalShots);

		if (shotCount <= 1) {
			weapon.attackPeriod = reloadRate;
		} else {
			weapon.attackPeriod = .1f;
		}

		return damage;

	}

}
