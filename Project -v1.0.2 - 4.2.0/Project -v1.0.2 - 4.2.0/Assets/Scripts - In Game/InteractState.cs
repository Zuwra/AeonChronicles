using UnityEngine;
using System.Collections;

public class InteractState : UnitState {

	private GameObject target;

	private bool attack;

	private int refreshTime = 5;
	private int currentFrame = 0;


	public InteractState(GameObject unit, UnitManager man, customMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;

	target = unit;
	myMover.resetMoveLocation (target.transform.position);

		if (unit.GetComponent<UnitManager> ().PlayerOwner == GameObject.Find ("GameRaceManager").GetComponent<RaceManager> ().playerNumber) {
			attack = false;
		} else {
			attack = true;
		}

		refreshTime = 30 - (int)myMover.MaxSpeed;
		if (refreshTime < 5) {
			refreshTime = 8;
		}
	}

	// Update is called once per frame
	override
	public void Update () {
		if (!target) {
			myManager.changeState(new DefaultState(myManager, myMover,myWeapon));
			return;
		}

		currentFrame ++;
		if (currentFrame > refreshTime) {
			currentFrame = 0;
			myMover.resetMoveLocation(target.transform.position);
		}

		//attack
		if (attack) {
			if (!myWeapon.inRange (target)) {
				myMover.move ();
			} else {

				if (myWeapon.canAttack (target)) {
					myWeapon.attack (target);

	
				}
			}
		}
		//interact
		else {
		//fill in interact stuff

			myManager.changeState(new DefaultState(myManager, myMover,myWeapon));
		}
	
	}


}
