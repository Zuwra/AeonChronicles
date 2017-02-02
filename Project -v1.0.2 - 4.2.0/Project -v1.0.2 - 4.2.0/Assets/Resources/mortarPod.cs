using UnityEngine;
using System.Collections;

public class mortarPod : MonoBehaviour, Validator, Notify, Modifier {




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

		weapon.validators.Add (this);
		weapon.triggers.Add (this);
		if (FireAll) {
			weapon.attackPeriod = .01f;
		}

		weapon.myManager.myStats.addDeathTrigger (this);

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextActionTime) {
			nextActionTime += reloadRate;
			if(shotCount < totalShots)
			{
				shotCount++;

				HealthD.updateCoolDown (shotCount / totalShots);
			}
		
		}


	
	}

	public void toggleFireAll()
	{FireAll = ! FireAll;


	}




	public bool validate(GameObject source, GameObject target)
		{if (shotCount > 0) {
			return true;
		
		}
		return false;
	}

	public void trigger(GameObject source, GameObject proj, UnitManager target, float damage)
		{
		shotCount --;
		HealthD.updateCoolDown (shotCount / totalShots);
		if (FireAll) {
			weapon.attackPeriod = .1f;
		}


	}


	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{ 

		if (weapon.myManager.myWeapon.Contains(weapon)) {
			foreach (TurretMount turr in transform.parent.GetComponentsInParent<TurretMount> ()) {

				if (turr.turret != null) {
					weapon.myManager.myWeapon.Contains(turr.turret.GetComponent<IWeapon> ());
					return 0 ;
				}

			}

		weapon.myManager.changeState (new DefaultState ());
	}
		return 0 ;
	}

}
