using UnityEngine;
using System.Collections;

public class MoveState : UnitState{

	// Use this for initialization

	Vector3 location;
	// Update is called once per frame

	public MoveState(Vector3 loc, UnitManager man)
	{
		myManager = man;

		location = loc;
		//myMover.resetMoveLocation (location);
	}

	public override void initialize()
	{myManager.cMover.resetMoveLocation (location);
	}




	override
	public void Update () {

		if (myManager.cMover.move ()) 
			{myManager.changeState(new DefaultState());	}

	}


	override
	public void attackResponse(GameObject src)
	{
	}

}
