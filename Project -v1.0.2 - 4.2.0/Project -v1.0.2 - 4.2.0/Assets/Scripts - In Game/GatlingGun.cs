using UnityEngine;
using System.Collections;

public class GatlingGun : MonoBehaviour, Notify, Validator, Modifier {


	public IWeapon myWeapon;
	private float intitalSpeed;

	public float speedIncrease = .1f;

	public float MinimumPeriod = .1f;

	private float nextActionTime;
	private float heatLevel = 2;
	public float totalHeat = 2;
	private float lastFired;

	private Selected healthD;
	private bool cooldown = false; // weapon shuts down;


	// Use this for initialization
	void Start () {
		if (myWeapon == null) {

			myWeapon = this.gameObject.GetComponent<IWeapon> ();
		}
		intitalSpeed = myWeapon.attackPeriod;

		healthD = GetComponent<Selected> ();
	
		myWeapon.triggers.Add (this);
		myWeapon.validators.Add (this);
		nextActionTime = Time.time;

		myWeapon.myManager.myStats.addDeathTrigger (this);
	
	}



	// Update is called once per frame
	void Update () {	

		if (myWeapon) {
			if (cooldown) {
				if (heatLevel < totalHeat / 2) {
					cooldown = false;
				}

			}
			if (Time.time > nextActionTime) {
				nextActionTime += .5f;
				heatLevel -= .12f;
				if (heatLevel < 0) {
					heatLevel = 0;
				
				}
				healthD.updateCoolDown (0);
				if (Time.time - lastFired > 1.5) {
					myWeapon.attackPeriod += speedIncrease;
					if (myWeapon.attackPeriod > intitalSpeed) {
						myWeapon.attackPeriod = intitalSpeed;
					}
				}


			}

		}


	
	}

	public void trigger(GameObject source, GameObject proj, GameObject target)
	{IncreaseSpeed (0, source);
	}

	public void IncreaseSpeed(float damage, GameObject source)
	{
	

		cooldown = false;
		lastFired = Time.time;
	
		heatLevel += .08f;
		if((heatLevel / totalHeat) > .15f)
		{healthD.updateCoolDown (heatLevel/totalHeat);}

		myWeapon.attackPeriod *= (1 - speedIncrease);
		if (myWeapon.attackPeriod < MinimumPeriod) {
			myWeapon.attackPeriod = MinimumPeriod;
		}
		if (heatLevel > totalHeat) {
			cooldown = true;
		}

	}


	public bool validate(GameObject source, GameObject target)
		{
		if (cooldown) {
		
			return false;}
		return true;

	}		



	public float modify(float damage, GameObject source)
	{ 

		if (myWeapon.myManager.myWeapon == myWeapon) {
			foreach (TurretMount turr in transform.parent.GetComponentsInParent<TurretMount> ()) {

				if (turr.turret != null) {
					myWeapon.myManager.myWeapon = turr.turret.GetComponent<IWeapon> ();
					return 0 ;
				}
		
			}
				
			myWeapon.myManager.changeState (new DefaultState ());
		}
		return 0 ;

	}

}
