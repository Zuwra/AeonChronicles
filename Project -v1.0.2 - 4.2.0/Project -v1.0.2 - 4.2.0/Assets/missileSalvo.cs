using UnityEngine;
using System.Collections;

public class missileSalvo : Ability, Validator, Notify{


	public float numOfRockets;
	private IWeapon myweapon;
	public float maxRockets = 4;
	private UnitManager mymanager;
	// Use this for initialization
	void Start () {
		mymanager = GetComponent<UnitManager> ();
		myweapon = GetComponent<IWeapon> ();
		myweapon.triggers.Add (this);
		myweapon.validators.Add (this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	public bool validate(GameObject source, GameObject target)
	{
		if (numOfRockets > 0) {
			return true;
		}
		return false;
	}




	public void trigger(GameObject source, GameObject projectile,GameObject target)	{
		numOfRockets--;


	}

	override
	public bool canActivate()
	{return true;


	}


	override
	public bool Activate()
	{
		Debug.Log ("activating missile salvo");
		GameObject home = null;
		float distance = 100000;

		foreach (MissileArmer arm in Object.FindObjectsOfType<MissileArmer>()) {
			float temp = Vector3.Distance (arm.gameObject.transform.position, this.gameObject.transform.position);
			if (temp< distance) {
				distance = temp;
				home = arm.gameObject;
			}
		
		}

		if (home != null) {
			mymanager.GiveOrder (Orders.CreateMoveOrder (home.transform.position));
		}
		return true;

	}

}
