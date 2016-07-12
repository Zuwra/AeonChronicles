using UnityEngine;
using System.Collections;

public class InteractState : UnitState {

	private GameObject target;



	private int refreshTime = 5;
	private int currentFrame = 0;


	public InteractState(GameObject unit, UnitManager man)
	{
		myManager = man;


		target = unit;
	//myMover.resetMoveLocation (target.transform.position);


		refreshTime = 30 - (int)myManager.cMover.getMaxSpeed();
		if (refreshTime < 5) {
			refreshTime = 8;
		}
	}


	public override void initialize()
	{myManager.cMover.resetMoveLocation (target.transform.position);
	}

	override
	public void endState()
	{
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
			myManager.cMover.resetMoveLocation(target.transform.position);
		}

		//attack

		if (!myManager.inRange(target)) {
			myManager.cMover.move ();
			} else {
			myManager.cMover.stop ();
			IWeapon myWeap = myManager.canAttack (target);	
			if (myWeap) {
				
				myWeap.attack (target,myManager);

				}
			}

		
	
	}


	override
	public void attackResponse(GameObject src)
	{
	}

}
