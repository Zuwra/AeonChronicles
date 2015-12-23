using UnityEngine;
using System.Collections;

public class AttckWhileMoveState : UnitState{

	

	
	public AttckWhileMoveState(Vector3 location, UnitManager man, IMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;
		
		myMover.resetMoveLocation (location);
	}
	
	override
	public void Update () {

		if(myManager.enemies.Count > 0){
			GameObject closestEnemy = myManager.findClosestEnemy();
		if (myWeapon.canAttack(closestEnemy))
		    {
				myWeapon.attack(closestEnemy);
			}
			}

		if (myMover.move ()) 
		{myManager.changeState(new DefaultState(myManager,myMover,myWeapon));}
		
	}


	override
	public void attackResponse(GameObject src)
	{
	}
}
