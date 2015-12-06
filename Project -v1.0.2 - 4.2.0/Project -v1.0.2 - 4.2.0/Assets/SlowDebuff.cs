using UnityEngine;
using System.Collections;

public class SlowDebuff : MonoBehaviour, Notify {

	public bool OnTarget = false;
	customMover mover ;

	public float speedDecrease;
	public float duration;
	public bool stackable;
	public float percent;

	private float totalDecrease;
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
			mover = this.gameObject.GetComponent<customMover>();

			if (percent != 0) {

				speedDecrease = mover.MaxSpeed*percent*.01f;	
			}
			totalDecrease += speedDecrease;
			mover.MaxSpeed -= speedDecrease;
			if(mover.MaxSpeed < 0)
			{mover.MaxSpeed = 0;}
		}
	}

	public void resetTime()
	{nextActionTime = Time.time + duration;}
	
	// Update is called once per frame
	void Update () {
	
		if (OnTarget) {
			if (Time.time > nextActionTime) {
				Destroy (this);
			}
		}
	}


	public void OnDestroy()
	{	
		if (OnTarget) {



			mover.MaxSpeed += totalDecrease;
		}
	}

	public void trigger(GameObject source,GameObject proj, GameObject target)
	{SlowDebuff deb = target.GetComponent<SlowDebuff> ();
		if (deb) {
			if (stackable) {
				deb.initialize (duration, speedDecrease, percent);
			}
			else{
				deb.resetTime();
			}
		} else {
			target.AddComponent<SlowDebuff> ();
			deb = target.GetComponent<SlowDebuff> ();
			deb.initialize (duration, speedDecrease,percent);
		}



	}



}
