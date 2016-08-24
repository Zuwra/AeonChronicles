using UnityEngine;
using System.Collections;

public class missileSalvo : Ability, Validator, Notify{


	private IWeapon myweapon;
	public int  maxRockets = 4;
	private UnitManager mymanager;
	private Selected mySelect;

	private float nextCheckTime;
	// Use this for initialization
	void Start () {
		mymanager = GetComponent<UnitManager> ();
		myweapon = GetComponent<IWeapon> ();
		myweapon.triggers.Add (this);
		myweapon.validators.Add (this);
		myType = type.activated;
		mySelect = GetComponent<Selected> ();
	
	}

	public void upRockets ()
	{
		chargeCount++;
		if (mySelect.IsSelected) {
			RaceManager.upDateUI ();
		}
	}

	public override void setAutoCast(bool offOn){
		autocast = offOn;

	}

	
	// Update is called once per frame
	void Update () {
		


	}
		

	public bool validate(GameObject source, GameObject target)
	{
		if (chargeCount > 0) {
			return true;
		}
		return false;
	}




	public void trigger(GameObject source, GameObject projectile,GameObject target, float damage)	{
		chargeCount--;
		RaceManager.upDateUI ();
		if (autocast && chargeCount == 0) {
			Activate ();
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
