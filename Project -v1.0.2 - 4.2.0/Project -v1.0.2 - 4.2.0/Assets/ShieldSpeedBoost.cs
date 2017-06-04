using UnityEngine;
using System.Collections;

public class ShieldSpeedBoost : Buff,Modifier {
	

	public float speedBoost = .5f;

	UnitStats myStats;
	IMover myMover;

	private bool ShieldsDown = false;
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
		

}
