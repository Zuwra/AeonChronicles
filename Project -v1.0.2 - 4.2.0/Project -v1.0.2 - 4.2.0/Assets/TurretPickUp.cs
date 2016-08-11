using UnityEngine;
using System.Collections;

public class TurretPickUp : TargetAbility {

	TurretMount myMount;
	public bool onSwallow;
	// Use this for initialization
	void Start () {
		myMount = this.gameObject.GetComponentInChildren<TurretMount> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public override void setAutoCast(bool offOn){
		autocast = offOn;
	}


	override
	public continueOrder canActivate (bool showError)
	{continueOrder order = new continueOrder ();
		order.nextUnitCast = false;

		return order;

	}

	override
	public void Activate()
	{
		
		//return false;//next unit should also do this.
	}

	public override bool isValidTarget (GameObject target, Vector3 location){

		return true;

	}




	override
	public void Cast()
	{ 
		if (onSwallow) {
			
			if (target) {
				if (soundEffect) {
					audioSrc.PlayOneShot (soundEffect);
				}

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

	override
	public  bool Cast(GameObject target, Vector3 location){
		return false;
	} 


}
