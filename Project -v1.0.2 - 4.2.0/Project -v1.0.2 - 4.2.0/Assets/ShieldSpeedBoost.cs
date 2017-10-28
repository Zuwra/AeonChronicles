using UnityEngine;
using System.Collections;

public class ShieldSpeedBoost : Buff,Modifier,Notify {
	

	public float speedBoost = .5f;

	UnitStats myStats;
	IMover myMover;

	//private bool ShieldsDown = false;
	public MultiShotParticle BoostEffect;
	UnitManager mymanager;
	private Selected select;

	private float nextActionTime;


	void OnEnable()
	{
		Invoke ("DelayedAwake", .3f);
	}

	void DelayedAwake()
	{
		GetComponent<UnitStats> ().addBuff (this, false);	
	}


	// Use this for initialization
	void Start () {
		if (!BoostEffect) {try {
			BoostEffect = transform.Find ("BoostEffect").GetComponent<MultiShotParticle> ();
			}
			catch(System.Exception)
			{Destroy (this);}
		}
		select = GetComponent<Selected> ();
		mymanager = GetComponent<UnitManager> ();
		myStats = GetComponent<UnitStats> ();

		myStats.addModifier (this);// ADDED
		//myStats.addEnergyModifier(this);
		mymanager.addNotify(this);

		myMover = GetComponent<airmover> ();
		if (!myMover) {
			myMover = GetComponent<CustomRVO> ();
		}
		activateSPeedBoost ();
	}

	Coroutine inCombat = null;
	float lastCombatTime;

	public float modify(float amount, GameObject src, DamageTypes.DamageType theType)
	{
		checkCombat ();
		return amount;
	}

	public float trigger(GameObject source, GameObject projectile,UnitManager target, float damage)
	{
		checkCombat ();
		return damage;
	}

	void checkCombat()
	{
		lastCombatTime = Time.time;
		if (inCombat == null) {
			inCombat = StartCoroutine (InCombatCheck ());
		} 

	}

	IEnumerator InCombatCheck()
	{
		yield return 0;
		DeactivateSpeedBoost ();
		while(lastCombatTime > Time.time -4 )
		{
			yield return new WaitForSeconds (.3f);
		}
		activateSPeedBoost ();

		inCombat = null;
	}




	/*

	public float modify(float amount, GameObject src, DamageTypes.DamageType theType)
{
		StartCoroutine (delayedEnergyCheck ());

		return amount;
	}

	IEnumerator delayedEnergyCheck()
	{
		yield return 0;
		if (ShieldsDown) {
			if (myStats.currentEnergy / myStats.MaxEnergy > .2f) {
				DeactivateSpeedBoost ();
			}
		} else if (myStats.currentEnergy / myStats.MaxEnergy < .1f) {
			activateSPeedBoost ();
		}
		
	}
	*/

	void activateSPeedBoost()
	{
		//ShieldsDown = true;
		mymanager.cMover.changeSpeed (speedBoost,0,false,this);
		BoostEffect.continueEffect ();

		if (select.IsSelected) {
			RaceManager.upDateSingleCard ();
		}
		
	}

	void DeactivateSpeedBoost()
	{
		mymanager.cMover.removeSpeedBuff (this);
		//ShieldsDown = false;
		BoostEffect.stopEffect ();
		if (select.IsSelected) {
			RaceManager.upDateSingleCard ();
		}
	}
		

}
