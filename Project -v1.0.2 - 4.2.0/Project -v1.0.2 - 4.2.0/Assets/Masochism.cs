using UnityEngine;
using System.Collections;

public class Masochism : Ability {


	private bool on;
	public float duration;
	public float speedBoost;

	private float timer;

	private float attackSpeedChange;
	private IWeapon myWeap;
	private IMover myMover;
	// Use this for initialization
	void Start () {description = "Uses life to give a short burst of speed";
		
		myWeap = GetComponent<IWeapon> ();
		myMover = GetComponent<UnitManager> ().cMover;
	}

	// Update is called once per frame
	void Update () {
		if (on && timer > 0) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				Deactivate ();
			}
		} 

	}

	public override void setAutoCast(bool offOn){
	}


	override
	public continueOrder canActivate (bool showError)
	{

		continueOrder order = new continueOrder ();

		if (chargeCount == 0) {
			order.canCast = false;
			return order;}
		if (!myCost.canActivate (this)) {
			order.canCast = false;
		}
		return order;
	}

	override
	public void Activate()
	{
		if (myCost.canActivate (this)) {

			if (!on) {


				if (GetComponent<SlowDebuff> () == null) {
					this.gameObject.AddComponent<SlowDebuff> ();
				}
				GetComponent<SlowDebuff> ().initialize (duration, -speedBoost, 0);

				myWeap.changeAttackSpeed(.5f, 0, false, this);
				myMover.changeSpeed(speedBoost, 0, false, this);


				myCost.payCost ();

				myWeap.fireTriggers (this.gameObject, null, GetComponent<UnitManager>(), 0);
			}

		}
		//return true;//next unit should also do this.
	}

	public void Deactivate()
	{on = false;
		timer = 0;

		myWeap.removeAttackSpeedBuff (this);
		myMover.removeSpeedBuff (this);

	}


}
