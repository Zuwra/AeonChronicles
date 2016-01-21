using UnityEngine;
using System.Collections;

public class TurretPickUp : TargetAbility {

	TurretMount myMount;
	// Use this for initialization
	void Start () {
		myMount = this.gameObject.GetComponentInChildren<TurretMount> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public override void setAutoCast(){
	}


	override
	public continueOrder canActivate ()
	{continueOrder order = new continueOrder ();
		order.nextUnitCast = false;

		return order;

	}

	override
	public void Activate()
	{
		
		//return false;//next unit should also do this.
	}



	override
	public void Cast()
	{ 

		if (target) {
	

			if (myMount.turret != null) {
	
				target.GetComponentInChildren<TurretMount> ().placeTurret (myMount.turret);

				myMount.unPlaceTurret ();

			} else {
				if (target.GetComponentInChildren<TurretMount> ().turret != null) {

					myMount.placeTurret (target.GetComponentInChildren<TurretMount> ().turret);

					target.GetComponentInChildren<TurretMount> ().unPlaceTurret ();
				
				}
			}
		}

	
	}



}
