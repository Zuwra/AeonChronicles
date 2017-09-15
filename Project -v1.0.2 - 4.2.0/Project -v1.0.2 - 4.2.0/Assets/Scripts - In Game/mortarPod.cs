using UnityEngine;
using System.Collections;

public class mortarPod : MonoBehaviour, Notify { // Modifier { //, Notify { //Validator, Notify{//, Modifier {




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

		//weapon.validators.Add (this);
		weapon.triggers.Add (this);

		if (FireAll) {
			weapon.attackPeriod = .01f;
		}

	//	weapon.myManager.myStats.addDeathTrigger (this);

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextActionTime) {
			nextActionTime += reloadRate - .01f;
			if(shotCount < totalShots)
			{
				shotCount++;

				HealthD.updateCoolDown (shotCount / totalShots);
				if (shotCount > 1) {
					weapon.attackPeriod = .1f;
				}
			}
		
		}


	
	}

	public void toggleFireAll()
	{FireAll = ! FireAll;


	}



	public float trigger(GameObject source, GameObject proj, UnitManager target, float damage)
		{
		shotCount --;
		HealthD.updateCoolDown (shotCount / totalShots);

		if (shotCount <= 1) {
			weapon.attackPeriod = reloadRate;
		} else {
			weapon.attackPeriod = .1f;
		}

		return damage;

	}
	/*(

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
		*/
}
