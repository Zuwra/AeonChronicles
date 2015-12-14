using UnityEngine;
using System.Collections;

public class MoveState : UnitState{

	// Use this for initialization

	
	// Update is called once per frame

	public MoveState(Vector3 location, UnitManager man, customMover move, IWeapon weapon)
	{
		myManager = man;

		myMover = move;
		myWeapon = weapon;
		if (myMover == null) {
			Debug.Log(myManager.gameObject.name);
		}
		myMover.resetMoveLocation (location);
	}

	override
	public void Update () {

		if (myMover.move ()) 
			{myManager.changeState(new DefaultState(myManager,myMover,myWeapon));	}

	}
}
