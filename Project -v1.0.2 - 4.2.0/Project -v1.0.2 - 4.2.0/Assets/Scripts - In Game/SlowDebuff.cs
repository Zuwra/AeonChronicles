using UnityEngine;
using System.Collections;

public class SlowDebuff : Behavior, Notify {

	public bool OnTarget = false;
	IMover mover ;

	public float speedDecrease;
	public float duration;
	public bool stackable;
	public float percent;

	private float nextActionTime;
	// Use this for initialization
	void Start () {
		if (this.gameObject.GetComponent<Projectile> ()) {
			this.gameObject.GetComponent<Projectile> ().triggers.Add (this);
		}
	
	}

	public void initialize(float dur, float speed, float percentdec)
	{

		OnTarget = true;
		duration = dur;
		speedDecrease = speed;
		percent = percentdec;
		nextActionTime = Time.time + duration;
		if (OnTarget) {
			mover = this.gameObject.GetComponent<UnitManager>().cMover;

			if (percent != 0) {
				mover.changeSpeed (percent, speedDecrease, false, this);

			}
		

			setBuffStuff (Behavior.buffType.movement, true);
			applyBuffUI ();
		}
	}

	public void resetTime()
	{nextActionTime = Time.time + duration;}
	
	// Update is called once per frame
	void Update () {
	
		if (OnTarget) {

			if (Time.time > nextActionTime) {
				
				unApplybuffUI ();
				Destroy (this);
			}
		}
	}


	public void OnDestroy()
	{	
		if (OnTarget) {
			mover.removeSpeedBuff (this);
	
		}
	}

	public void trigger(GameObject source,GameObject proj, GameObject target, float damage)
	{SlowDebuff deb = target.GetComponent<SlowDebuff> ();
		if (deb) {
			if (stackable) {
				deb.initialize (duration, speedDecrease, percent);
			}
			else{
				deb.resetTime();
			}
		} else {
			if(target.GetComponent<customMover>() != null){
			target.AddComponent<SlowDebuff> ();
			deb = target.GetComponent<SlowDebuff> ();
			deb.initialize (duration, speedDecrease,percent);}
		}



	}



}
