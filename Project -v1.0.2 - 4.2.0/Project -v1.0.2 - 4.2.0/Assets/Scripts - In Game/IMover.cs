using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IMover: MonoBehaviour {



	public float speed = 0;
	public float acceleration;
	public float MaxSpeed = 10;
	private float initialSpeed;

	public abstract bool move ();

	public 	abstract void resetMoveLocation (Vector3 location);
	public 	abstract void resetMoveLocation (Transform theTarget);

	private struct SpeedMod{
		public float perc;
		public float flat;
		public Object source;
	}

	private List<SpeedMod> ASMod = new List<SpeedMod>();



	void Start()
	{
		initialSpeed = MaxSpeed;

	}

	public void removeSpeedBuff(Object obj)
	{
		for (int i = 0; i < ASMod.Count; i++) {
			if (ASMod [i].source ==obj) {
				ASMod.RemoveAt (i);
			}
		}
		adjustSpeed ();

	}


	public void changeSpeed(float perc, float flat, bool perm, Object obj )
	{if (perm) {
			initialSpeed += flat;
			initialSpeed *= perc;
	} else {
		SpeedMod temp = new SpeedMod ();
		temp.flat = flat;
		temp.perc = perc;
		temp.source = obj;
		ASMod.Add (temp);
	}

	adjustSpeed ();

}

	private void adjustSpeed()
	{
		float speed = initialSpeed;
		foreach (SpeedMod a in ASMod) {
			speed += a.flat;
		}

		float percent = 1;
		foreach (SpeedMod a in ASMod) {
			percent += a.perc;
		}
		speed *= percent;
		if (speed < .01f) {
			speed = .01f;}
		else if(speed >10000) {
			speed = 10000;}
		MaxSpeed = speed;
	}

}
