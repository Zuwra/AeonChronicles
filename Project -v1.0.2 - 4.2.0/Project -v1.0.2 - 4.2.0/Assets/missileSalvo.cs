using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class missileSalvo : Ability, Validator, Notify{


	private IWeapon myweapon;
	public int  maxRockets = 2;
	private UnitManager mymanager;
	private Selected mySelect;

	public List<GameObject> MissileModels = new List<GameObject> ();

	private float nextCheckTime;

	float fillHerUp = 1;
	// Use this for initialization
	void Start () {
		mymanager = GetComponent<UnitManager> ();
		myweapon = GetComponent<IWeapon> ();
		myweapon.triggers.Add (this);
		myweapon.validators.Add (this);
		myType = type.activated;
		mySelect = GetComponent<Selected> ();
		StartCoroutine (delayedUpdate());
	

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
		for (int i = 0; i < 10; i++) {
			yield return new WaitForSeconds (.08f);
			fillHerUp += amount/10;
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
	
		RaceManager.upDateUI ();
		if (autocast && chargeCount <= 0) {
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
	{

		GameObject home = null;
		float distance = 100000;

		foreach (MissileArmer arm in Object.FindObjectsOfType<MissileArmer>()) {
			if(arm.missiles){
			float temp = Vector3.Distance (arm.gameObject.transform.position, this.gameObject.transform.position);
				if (temp < distance) {
					distance = temp;
					home = arm.gameObject;
				}
			}
		
		}

		if (home != null) {
			mymanager.GiveOrder (Orders.CreateMoveOrder (home.transform.position));
		}
		//return true;

	}

}
