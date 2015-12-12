using UnityEngine;
using System.Collections;

public class DefaultState : UnitState{

	// Update is called once per frame

	public DefaultState(UnitManager man, customMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;

	}


	override
	public void Update () {// change this later so t will only check for attackable enemies.
		if (myManager.enemies.Count > 0) {

			//Debug.Log("best enemey is" + myManager.findBestEnemy());
			myManager.changeState(new AttackMoveState(myManager.findBestEnemy(),
			     new Vector3(), AttackMoveState.MoveType.passive, myManager, myMover,myWeapon, myManager.gameObject.transform.position));
		}
	
	
	}
}
