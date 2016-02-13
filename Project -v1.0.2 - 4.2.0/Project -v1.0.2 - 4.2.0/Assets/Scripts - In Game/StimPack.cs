using UnityEngine;
using System.Collections;

public class StimPack : Ability {


	private bool on;
	public float duration;
	public float speedBoost;

	private float timer;
	private Selected select;

	// Use this for initialization
	void Start () {description = "Uses life to give a short burst of speed";
		select = GetComponent<Selected> ();
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

	public override void setAutoCast(){
	}


	override
	public continueOrder canActivate ()
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
				this.gameObject.GetComponent<customMover> ().MaxSpeed += speedBoost;
			
			

				myCost.payCost ();
				on = true;
				timer = duration;
				chargeCount--;
				if (select.IsSelected) {
					RaceManager.upDateUI ();
				}
			
			}
		
		}
		//return true;//next unit should also do this.
	}

	public void Deactivate()
	{on = false;
		timer = 0;

		this.gameObject.GetComponent<customMover>().MaxSpeed -=speedBoost;
		this.gameObject.GetComponent<customMover>().speed -=speedBoost;
	}


}
