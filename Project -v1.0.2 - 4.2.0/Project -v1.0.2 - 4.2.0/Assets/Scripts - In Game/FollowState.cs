using UnityEngine;
using System.Collections;

public class FollowState : UnitState {

	private GameObject target;


	private int refreshTime = 5;
	private int currentFrame = 0;


	public FollowState(GameObject unit, UnitManager man, IMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;

		target = unit;
		//myMover.resetMoveLocation (target.transform.position);



	}

	public override void initialize()
		{myMover.resetMoveLocation (target.transform.position);

		refreshTime = 30 - (int)myMover.getMaxSpeed();
		if (refreshTime < 5) {
			refreshTime = 8;
		}
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

		if(Vector3.Distance(myManager.gameObject.transform.position, target.transform.position) >13)
			{myMover.move ();}
		//attack


	}

	override
	public void attackResponse(GameObject src)
	{
	}


}
