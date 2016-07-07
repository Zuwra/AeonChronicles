using UnityEngine;
using System.Collections;

public class DayexaShield : Ability,Modifier {


	public float Absorbtion;
	public UnitStats myStats;

	private float rechargeTime;

	public float RechargeDelay;
	public float rechargeRate;
	private bool inCombat;

	public GameObject shieldEffect;

	// Use this for initialization
	void Start () {
		myStats.addModifier (this);

		//This makes it so all childed turrets get their incoming damage reduced by the tanks shields. 
		foreach (IWeapon obj in GetComponent<UnitManager>().myWeapon) {
			
			obj.gameObject.GetComponent<UnitManager> ().myStats.addModifier (this);

		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
	

		if (inCombat) {
			if (Time.time > rechargeTime) {
				inCombat = false;
				myStats.EnergyRegenPerSec = rechargeRate;
			}
		
		}
	}

	public void startRecharge()
	{inCombat = true;
	}
	public void stopRecharge()
	{inCombat = true;
		rechargeTime = Time.time + RechargeDelay;
	}

	public float modify(float amount, GameObject src)
	{
		float energyLost = Mathf.Min ( Absorbtion, myStats.currentEnergy);
		if (energyLost > amount) {
			energyLost = amount;
		}

		myStats.changeEnergy (-energyLost);
		myStats.EnergyRegenPerSec = 0;
		inCombat = true;
		rechargeTime = Time.time + RechargeDelay;
		if (shieldEffect && energyLost > 0) {
			GameObject obj = (GameObject)Instantiate (shieldEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
			obj.transform.SetParent (this.gameObject.transform);
		}
	
		return (amount - energyLost);
	}



	public override void setAutoCast(){
	}


	override
	public continueOrder canActivate ()
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
