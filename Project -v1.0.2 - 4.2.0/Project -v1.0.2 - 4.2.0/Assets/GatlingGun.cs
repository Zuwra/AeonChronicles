using UnityEngine;
using System.Collections;

public class GatlingGun : MonoBehaviour, Notify, Validator {


	public IWeapon myWeapon;
	private float intitalSpeed;

	public float speedIncrease;

	public float MinimumPeriod;

	private float nextActionTime;
	public float heatLevel;
	public float totalHeat;
	private float lastFired;

	private bool cooldown = false; // weapon shuts down;


	// Use this for initialization
	void Start () {
		if (myWeapon == null) {

			//myWeapon = this.gameObject.GetComponent<IWeapon> ();
		}
		intitalSpeed = myWeapon.attackPeriod;


	
		myWeapon.triggers.Add (this);
		myWeapon.validators.Add (this);
		nextActionTime = Time.time;
	
	}



	// Update is called once per frame
	void Update () {	
		if (cooldown) {
			if(heatLevel < totalHeat/2)
			{cooldown = false;}

		}
		if (Time.time > nextActionTime) {
			nextActionTime += .5f;
			heatLevel -=.12f;
			if(heatLevel < 0)
			{
				heatLevel = 0;
			}

			if(Time.time -lastFired >1.5){
				myWeapon.attackPeriod += speedIncrease;
				if(myWeapon.attackPeriod > intitalSpeed)
				{
					myWeapon.attackPeriod = intitalSpeed;
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

		myWeapon.attackPeriod -= speedIncrease;
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

}
