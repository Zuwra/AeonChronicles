﻿using UnityEngine;
using System.Collections;

public class MiningState : UnitState {

	public float interactionDistance;

	private GameObject target;
	private GameObject dropoff;
	private GameObject hook;
	private enum miningState
	{
		traveling, mining, returning
	}

	private float timer;
	private float miningTime;

	private miningState state;

	private float resourceOneAmount;
	private float resourceTwoAmount;

	private OreDispenser currentlyMining;

	public MiningState(GameObject unit, UnitManager man,  float mineTime, float resourceOne, float resourceTwo, GameObject hooky)
	{
		myManager = man;

		miningTime = mineTime;
		resourceOneAmount = resourceOne;
		resourceTwoAmount= resourceTwo;
		hook = hooky;
		target = unit;
		//myMover.resetMoveLocation (target.transform.position);
		currentlyMining = target.GetComponent<OreDispenser> ();
	

	


	}

	public override void initialize()
	{
		if (target.GetComponent<OreDispenser> ().requestWork (myManager.gameObject)) {
			currentlyMining = target.GetComponent<OreDispenser> ();
			myManager.cMover.resetMoveLocation (target.transform.position);
		}

		dropoff = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.getNearestDropOff (target);
		state = miningState.traveling;
	}

	// Update is called once per frame
	override
	public void Update () {
		/*
		if (!target) {

			foreach (GameObject obj in myManager.neutrals) {
				OreDispenser dispense = obj.GetComponent<OreDispenser> ();
				if (dispense && !dispense.currentMinor) {
					currentlyMining = dispense;
					target = obj;
					myManager.cMover.resetMoveLocation (obj.transform.position);
					break;
				}
			}

		}
*/


		switch (state) {
		case miningState.traveling:
			if (myManager.cMover.move ()) {



				state = miningState.mining;
				timer = miningTime;


			}
			break;


		case miningState.mining:
			timer -= Time.deltaTime;
			if (timer < 0) {
				if (myManager.GetComponent<ResourceDropOff> ()) {
					float haul = currentlyMining.getOre (resourceOneAmount);
					dropoff.GetComponent<ResourceDropOff> ().dropOff (haul, resourceTwoAmount);
					state = miningState.traveling;

				} else {

					dropoff = GameManager.main.activePlayer.getNearestDropOff (target);
				
				if (dropoff == null) {
					myManager.changeState (new DefaultState ());
				
				} else {
						resourceOneAmount = currentlyMining.getOre (resourceOneAmount);
					myManager.cMover.resetMoveLocation (dropoff.transform.position);
					state = miningState.returning;

				}
			}
			} else if (timer / miningTime > .75) {
			
				Vector3 pos = hook.transform.position ;
				pos.y -= 25f * Time.deltaTime;
				hook.transform.position = pos;
			} else if (timer / miningTime < .25) {
				Vector3 pos = hook.transform.position;
				pos.y += 25f * Time.deltaTime;
				hook.transform.position = pos;
			
			}

			break;

		case miningState.returning:
			if (myManager.cMover.move ()) {
				if (dropoff == null) {
					dropoff = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.getNearestDropOff (target);

					if (dropoff == null) {
						myManager.changeState (new DefaultState ());
					} else {
						myManager.cMover.resetMoveLocation (dropoff.transform.position);
						state = miningState.returning;
					}


				} else {
					dropoff.GetComponent<ResourceDropOff> ().dropOff (resourceOneAmount, resourceTwoAmount);
					myManager.cMover.resetMoveLocation (target.transform.position);
					state = miningState.traveling;
				}
			}
			break;

		}


		if (!target) 
			{myManager.changeState(new DefaultState());	}

	}


	override
	public void attackResponse(GameObject src)
	{
	}

}
