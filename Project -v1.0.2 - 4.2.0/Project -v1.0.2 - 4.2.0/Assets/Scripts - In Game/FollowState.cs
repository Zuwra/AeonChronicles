using UnityEngine;
using System.Collections;

public class FollowState : UnitState {

	private GameObject target;


	private int refreshTime = 5;
	private int currentFrame = 0;


	public FollowState(GameObject unit, UnitManager man)
	{
		myManager = man;


		target = unit;
		//myMover.resetMoveLocation (target.transform.position);

	}

	public override void initialize()
	{myManager.cMover.resetMoveLocation (target.transform.position);

		refreshTime = 30 - (int)myManager.cMover.getMaxSpeed();
		if (refreshTime < 5) {
			refreshTime = 8;
		}
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

		if(Vector3.Distance(myManager.gameObject.transform.position, target.transform.position) >13)
		{myManager.cMover.move ();}
		//attack


	}

	override
	public void attackResponse(GameObject src)
	{
	}


}
