using UnityEngine;
using System.Collections;

public class InteractState : UnitState {

	private GameObject target;



	private int refreshTime = 5;
	private int currentFrame = 0;


	public InteractState(GameObject unit, UnitManager man, IMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;

		target = unit;
	//myMover.resetMoveLocation (target.transform.position);


		refreshTime = 30 - (int)myMover.MaxSpeed;
		if (refreshTime < 5) {
			refreshTime = 8;
		}
	}


	public override void initialize()
	{myMover.resetMoveLocation (target.transform.position);
	}



	// Update is called once per frame
	override
	public void Update () {
		if (!target) {
			myManager.changeState(new DefaultState());
			return;
		}

		currentFrame ++;
		if (currentFrame > refreshTime) {
			currentFrame = 0;
			myMover.resetMoveLocation(target.transform.position);
		}

		//attack

			if (!myWeapon.inRange (target)) {
				myMover.move ();
			} else {

				if (myWeapon.canAttack (target)) {
					myWeapon.attack (target);

	
				}
			}

		
	
	}


	override
	public void attackResponse(GameObject src)
	{
	}

}
