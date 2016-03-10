using UnityEngine;
using System.Collections;

public class repairReturn : Ability{


	public int maxRepair = 400;
	private UnitManager mymanager;

	public int hiddenAmount;
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


	public void removeTurret (){
		Debug.Log ("removing turret " );
		setHiddenAmount ();
		chargeCount = -1;
	}

	public void placeTurret()
	{Debug.Log ("Placing turret " + hiddenAmount);
		chargeCount = hiddenAmount;
	}

	public void setHiddenAmount ()
	{hiddenAmount = chargeCount;
		Debug.Log ("Setting hidden amount " + hiddenAmount);
	}





	public bool validate(GameObject source, GameObject target)
	{if (!active) {
			return false;}
		
		if (chargeCount < maxRepair) {
			return true;
		}
		return false;
	}




	public void trigger(GameObject source, GameObject projectile,GameObject target)	{



	}

	override
	public continueOrder canActivate()
	{continueOrder ord = new continueOrder ();
		if (!active) {
			ord.canCast = false;}

		return ord;


	}


	override
	public void Activate()
	{

		GameObject home = null;
		float distance = 100000;

		foreach (MissileArmer arm in Object.FindObjectsOfType<MissileArmer>()) {
			if (arm.repairs) {
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
