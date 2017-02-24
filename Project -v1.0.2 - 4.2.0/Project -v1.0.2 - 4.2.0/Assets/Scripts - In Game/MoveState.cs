using UnityEngine;
using System.Collections;

public class MoveState : UnitState{

	// Use this for initialization

	Vector3 location;
	public bool assumedMove = false;
	// Update is called once per frame

	public MoveState(Vector3 loc, UnitManager man)
	{
		myManager = man;

		location = loc;
		assumedMove = false;
		//myMover.resetMoveLocation (location);
	}

	public MoveState(Vector3 loc, UnitManager man, bool assumed)
	{assumedMove = assumed;
		myManager = man;

		location = loc;
		//myMover.resetMoveLocation (location);
	}

	public override void initialize()
	{myManager.cMover.resetMoveLocation (location);
	}


	override
	public void endState()
	{
	}

	override
	public void Update () {

	
		if (myManager.cMover && myManager.cMover.move ()) 
		{
				myManager.changeState(new DefaultState());	}
	

	}


	override
	public void attackResponse(UnitManager src, float amount)
	{
	}

}
