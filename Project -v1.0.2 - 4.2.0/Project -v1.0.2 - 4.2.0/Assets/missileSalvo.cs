using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class missileSalvo : Ability, Validator, Notify{


	private IWeapon myweapon;
	public int  maxRockets = 2;
	private UnitManager mymanager;
	private Selected mySelect;

	public List<GameObject> MissileModels = new List<GameObject> ();
	Vector3 padSpot;
	private float nextCheckTime;

	float fillHerUp = 1;
	float flierheight;
	public HarpyLandingPad home;

	float lastDistance;

	// Use this for initialization
	void Start () {
		flierheight = GetComponent<airmover> ().flyerHeight;
		mymanager = GetComponent<UnitManager> ();
		myweapon = GetComponent<IWeapon> ();
		myweapon.triggers.Add (this);
		myweapon.validators.Add (this);
		myType = type.activated;
		mySelect = GetComponent<Selected> ();
		StartCoroutine (delayedUpdate());
		InvokeRepeating ("CheckForReservation", 1, .4f);

	}

	void CheckForReservation()
	{if(home)
		{
		
			float temp = Vector3.Distance (transform.position, home.transform.position);
			if (temp > lastDistance) {
				home.finished (this.gameObject);
				home = null;

			}
			lastDistance = temp;
		}
	}


	IEnumerator delayedUpdate()
	{
		yield return new WaitForSeconds (.1f);
		mySelect.updateCoolDown (chargeCount / maxRockets);
		fillHerUp = 1;
	}


	public void upRockets ()
	{if(chargeCount < maxRockets){
		chargeCount++;
		StartCoroutine (fillUpBar(.5f));
		
		
		if (mySelect.IsSelected) {
			
			RaceManager.upDateUI ();
		}
			if(MissileModels.Count > chargeCount-1 &&chargeCount-1 >=0 ){
			MissileModels [chargeCount-1].SetActive (true);
	}}
	}

	public override void setAutoCast(bool offOn){
		autocast = offOn;

	}


	IEnumerator fillUpBar(float amount)
	{
		for (int i = 0; i < 12; i++) {
			yield return new WaitForSeconds (.07f);
			fillHerUp += amount/12;
			mySelect.updateCoolDown (fillHerUp + .05f);
		
		}

	}

		

	public bool validate(GameObject source, GameObject target)
	{
		if (chargeCount > 0) {
			return true;
		}
		if (autocast && chargeCount <= 0) {
			Activate ();
		}
		return false;
	}




	public void trigger(GameObject source, GameObject projectile,UnitManager target, float damage)	{
		
		chargeCount--;

		StartCoroutine (fillUpBar(-.5f));
	
		if (mySelect.IsSelected ) {
			RaceManager.upDateUI ();
		}
		if (autocast && chargeCount <= 0) {
			StartCoroutine (checkForLandingPad ());
			Activate ();
		}
		if(MissileModels.Count > chargeCount && chargeCount >= 0){
			MissileModels [chargeCount].SetActive (false);
		}

	}

	override
	public continueOrder canActivate(bool showError)
	{
		return new continueOrder ();

	}


	override
	public void Activate()
	{//Debug.Log ("activating " + chargeCount);
		if (chargeCount < maxRockets) {
			if (home) {
				home.finished (this.gameObject);}
			home = null;
			padSpot = Vector3.zero;
			float distance = 100000;

			foreach (HarpyLandingPad arm in Object.FindObjectsOfType<HarpyLandingPad>()) {

				if (arm.hasAvailable ()) {
					float temp = Vector3.Distance (arm.gameObject.transform.position, this.gameObject.transform.position);
					if (temp < distance) {
						distance = temp;
						home = arm;
					}
				}
		
			}

			if (home) {
				mymanager.GiveOrder (Orders.CreateMoveOrder (home.transform.position));
			}
			home = null;
		}
	}


	public void Dying()
	{
		if (home != null) {
			home.finished (this.gameObject);
		}

	}

	IEnumerator loadingMissile()
	{
		home.startLanding (this.gameObject);
		yield return new WaitForSeconds (1);
		mymanager.StunForTime (this, 4);
		yield return new WaitForSeconds (1.5f);
		upRockets ();
		yield return new WaitForSeconds (2f);
		upRockets ();
		yield return new WaitForSeconds (1.2f);
		mymanager.setStun (false, this);
		GetComponent<airmover> ().flyerHeight = flierheight;
		mymanager.GiveOrder (Orders.CreateMoveOrder (this.transform.position+ transform.forward *10) );
		if (home) {
			home.finished (this.gameObject);
		}
		home = null;

	}

	IEnumerator checkForLandingPad()
	{
		
		yield return new WaitForSeconds (.3f);


		float distance;

		while (chargeCount < maxRockets ) {
		
			if (!(mymanager.getState () is AttackMoveState)) {

				if (home == null) {
					distance = 100000;
					foreach (HarpyLandingPad arm in nearbyPads) {
						if (arm.hasAvailable ()) {
							float temp = Vector3.Distance (arm.gameObject.transform.position, this.gameObject.transform.position);
							if (temp < distance) {
								distance = temp;
								home = arm;
							}
						}
					}
			
					if (home != null) {
						Vector3 temp = home.requestLanding (this.gameObject);
						if (temp != Vector3.zero) {
							padSpot = temp;
							mymanager.GiveOrder (Orders.CreateMoveOrder (padSpot));
						}
					}

				}
				if (home != null) {

					if (padSpot != Vector3.zero) {
				

						if (mymanager.getState () is DefaultState && Vector3.Distance (transform.position, padSpot) < 20) {
							GetComponent<airmover> ().flyerHeight = 4;
							mymanager.GiveOrder (Orders.CreateMoveOrder (padSpot + transform.forward * .3f));
							StartCoroutine (loadingMissile ());


						}

					}
				}
			}

			yield return new WaitForSeconds (1f);
		}
		GetComponent<airmover> ().flyerHeight = flierheight;

	}


		List<HarpyLandingPad> nearbyPads = new List<HarpyLandingPad> ();

		void OnTriggerEnter(Collider other)
		{
		//Fix this if the enemy ever has harpies
		HarpyLandingPad pad = other.GetComponent<HarpyLandingPad>();
		if (pad) {
			nearbyPads.Add (pad);
		
		}
		}

	void OnTriggerExit(Collider other)
	{
		//Fix this if the enemy ever has harpies
		HarpyLandingPad pad = other.GetComponent<HarpyLandingPad>();
		if (pad) {
			nearbyPads.Remove(pad);

		}
	}


}
