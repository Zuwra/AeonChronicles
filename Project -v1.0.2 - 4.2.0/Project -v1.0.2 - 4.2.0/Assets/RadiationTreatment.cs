using UnityEngine;
using System.Collections;


//THIS SCRIPT REQUIRES THERE TO BE A POPUPONHIT SCRIPT ON THE PROJECTILE IT IS MODIFYING
public class RadiationTreatment :  Ability, Notify {


	public float damageIncreaseAmount;
	float runningTotal;
	float lastDamageAmount;
	UnitManager lastTarget;
	UnitStats myStats;


	IWeapon myWeap;
	// Use this for initialization
	void Start () {
		myStats = GetComponent<UnitStats> ();
		lastDamageAmount = 0;
		myWeap = GetComponent<IWeapon> ();
		if (myWeap) {
			myWeap.addNotifyTrigger (this);
		}
		myType = type.passive;
	}


	public float trigger(GameObject source,GameObject proj,UnitManager target, float damage)
	{
		//Debug.Log ("Being triggered");
		if (lastTarget == target && myStats.veternStat.damageDone > lastDamageAmount) {
			runningTotal += damageIncreaseAmount;
			//proj.GetComponent<Projectile> ().damage += runningTotal;
			proj.GetComponent<PopUpOnHIt>().toShow = "-" +(proj.GetComponent<Projectile> ().damage+ runningTotal);

		} else {
			
			runningTotal = 0;
		
		}
		lastDamageAmount = myStats.veternStat.damageDone;
		lastTarget = target;
		return damage + runningTotal;
	}



	public override void setAutoCast(bool offOn){
	}


	override
	public continueOrder canActivate (bool showError)
	{

		continueOrder order = new continueOrder ();
		return order;
	}

	override
	public void Activate()
	{
		//return true;//next unit should also do this.
	}
}
