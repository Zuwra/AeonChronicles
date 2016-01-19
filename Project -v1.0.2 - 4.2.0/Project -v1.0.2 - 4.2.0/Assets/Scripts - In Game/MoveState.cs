﻿using UnityEngine;
using System.Collections;

public class MoveState : UnitState{

	// Use this for initialization

	Vector3 location;
	// Update is called once per frame

	public MoveState(Vector3 loc, UnitManager man, IMover move, IWeapon weapon)
	{
		myManager = man;

		myMover = move;
		myWeapon = weapon;
		location = loc;
		//myMover.resetMoveLocation (location);
	}

	public override void initialize()
	{myMover.resetMoveLocation (location);
	}




	override
	public void Update () {

		if (myMover.move ()) 
			{myManager.changeState(new DefaultState(myManager,myMover,myWeapon));	}

	}


	override
	public void attackResponse(GameObject src)
	{
	}

}
