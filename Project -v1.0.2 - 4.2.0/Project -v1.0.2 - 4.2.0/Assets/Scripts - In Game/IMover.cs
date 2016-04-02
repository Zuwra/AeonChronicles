﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IMover: MonoBehaviour {



	public float speed = 0;
	public float acceleration;
	public float MaxSpeed = 10;
	public float initialSpeed;

	public abstract bool move ();

	public 	abstract void resetMoveLocation (Vector3 location);
	public 	abstract void resetMoveLocation (Transform theTarget);

	private struct SpeedMod{
		public float perc;
		public float flat;
		public Object source;
	}

	private List<SpeedMod> ASMod = new List<SpeedMod>();

	void Awake()
	{
		initialSpeed = MaxSpeed;

	}

	public float getMaxSpeed()
	{
		return MaxSpeed;
	}


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
		float tempspeed = initialSpeed;

		foreach (SpeedMod a in ASMod) {
			tempspeed += a.flat;
		
		}

		float percent = 1;
		foreach (SpeedMod a in ASMod) {
			percent += a.perc;
		}

		tempspeed *= percent;
		if (tempspeed < .01f) {
			tempspeed = .01f;}
		else if(tempspeed >1000) {
			tempspeed = 1000;}


		MaxSpeed = tempspeed;
		if (speed > MaxSpeed) {
			speed = MaxSpeed;}
	}

}
