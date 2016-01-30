﻿using UnityEngine;
using System.Collections;

public class AbilityFollowState  : UnitState {

	private GameObject target;
	private Vector3 location;

	public TargetAbility myAbility;

	private int refreshTime = 5;
	private int currentFrame = 0;
	private bool Follow;


	public AbilityFollowState(GameObject unit, Vector3 loc, TargetAbility abil)
	{
		
		location = loc;
		myAbility = abil;
		abil.target = unit;
		abil.location = loc;
	
		target = unit;

		if (target != null) {
			Follow = true;
		}




	}

	public override void initialize()
	{
		refreshTime = 30 - (int)myMover.MaxSpeed;
		if (refreshTime < 5) {
			refreshTime = 8;
		}
		if (target) {
			myMover.resetMoveLocation (target.transform.position);
		}
	}

	// Update is called once per frame
	override
	public void Update () {
		
		if (!target && Follow) {
			myManager.changeState(new DefaultState());
			return;
		}
		if (Follow) {
			currentFrame++;
			if (currentFrame > refreshTime) {
				currentFrame = 0;
				location = target.transform.position;
				myMover.resetMoveLocation (location);
			}
		}

	
		if (!myAbility.inRange (location)) {

	
			myMover.move ();
		} else {
			
			myAbility.Cast();
			myManager.changeState(new DefaultState());
			return;

		}
		//attack


	}

	override
	public void attackResponse(GameObject src)
	{
	}


}
