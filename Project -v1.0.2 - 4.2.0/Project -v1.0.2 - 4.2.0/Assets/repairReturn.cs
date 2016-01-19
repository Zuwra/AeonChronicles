using UnityEngine;
using System.Collections;

public class repairReturn : Ability{


	public int maxRepair = 400;
	private UnitManager mymanager;

	private UnitStats TargetHealth;
	private GameObject target;
	// Use this for initialization
	void Start () {
		mymanager = GetComponent<UnitManager> ();


	}

	public override void setAutoCast(){
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




	public void trigger(GameObject source, GameObject projectile,GameObject target)	{



	}

	override
	public bool canActivate()
	{return true;


	}


	override
	public bool Activate()
	{

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
