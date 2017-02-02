using UnityEngine;
using System.Collections;

public class ShieldSpeedBoost : Ability,Modifier {
	

	public float speedBoost = .5f;

	UnitStats myStats;
	IMover myMover;

	private bool ShieldsDown = false;
	public MultiShotParticle BoostEffect;
	UnitManager mymanager;
	private Selected select;

	private float nextActionTime;



	void Awake()
	{
		myType = type.passive;
	}


	// Use this for initialization
	void Start () {
		if (!BoostEffect) {
			BoostEffect = transform.FindChild ("BoostEffect").GetComponent<MultiShotParticle> ();
		}
		select = GetComponent<Selected> ();
		mymanager = GetComponent<UnitManager> ();
		myStats = GetComponent<UnitStats> ();
		myStats.addEnergyModifier(this);


		myMover = GetComponent<airmover> ();
		if (!myMover) {
			myMover = GetComponent<CustomRVO> ();
		}

	}



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

	void activateSPeedBoost()
	{
		ShieldsDown = true;
		mymanager.cMover.changeSpeed (speedBoost,0,false,this);
		BoostEffect.continueEffect ();

		if (select.IsSelected) {
			RaceManager.upDateSingleCard ();
		}
		
	}

	void DeactivateSpeedBoost()
	{
		mymanager.cMover.removeSpeedBuff (this);
		ShieldsDown = false;
		BoostEffect.stopEffect ();
		if (select.IsSelected) {
			RaceManager.upDateSingleCard ();
		}
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
