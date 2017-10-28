using UnityEngine;
using System.Collections;

public class mortarPod : MonoBehaviour, Notify {




	public float totalShots;
	public float reloadRate;
	public float shotCount;
	private IWeapon weapon;

	public bool FireAll;

	Selected HealthD;

	// Use this for initialization

	void Awake ()
	{
		HealthD = GetComponentInChildren<Selected> ();
	}
	void Start () {

	
		shotCount = totalShots;
		weapon = this.gameObject.GetComponent<IWeapon> ();

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

	public void updateUI()
	{
		if (!HealthD) {
			HealthD = GetComponentInChildren<Selected> ();
		}
		HealthD.updateCoolDown (shotCount / totalShots);
	}

}
