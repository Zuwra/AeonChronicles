using UnityEngine;
using System.Collections;

public class mortarPod : MonoBehaviour, Validator, Notify, Modifier {




	public float totalShots;
	public float reloadRate;
	private float shotCount;
	private IWeapon weapon;

	public bool FireAll;

	private float nextActionTime;


	// Use this for initialization
	void Start () {


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

	public void trigger(GameObject source, GameObject proj, GameObject target, float damage)
		{
		shotCount --;
		if (FireAll) {
			weapon.attackPeriod = .01f;
		}


	}


	public float modify(float damage, GameObject source)
	{ 

		if (weapon.myManager.myWeapon == weapon) {
			foreach (TurretMount turr in transform.parent.GetComponentsInParent<TurretMount> ()) {

				if (turr.turret != null) {
					weapon.myManager.myWeapon = turr.turret.GetComponent<IWeapon> ();
					return 0 ;
				}

			}

		weapon.myManager.changeState (new DefaultState ());
	}
		return 0 ;
	}

}
