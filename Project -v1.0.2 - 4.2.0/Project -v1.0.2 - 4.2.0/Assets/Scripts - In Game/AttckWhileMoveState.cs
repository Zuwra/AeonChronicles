using UnityEngine;
using System.Collections;

public class AttckWhileMoveState : UnitState{

	private Vector3 location;

	
	public AttckWhileMoveState(Vector3 loca, UnitManager man, IMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;
		location = loca;
		//myMover.resetMoveLocation (location);
	}


	public override void initialize()
	{myMover.resetMoveLocation (location);
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
		{myManager.changeState(new DefaultState());}
		
	}


	override
	public void attackResponse(GameObject src)
	{
	}
}
