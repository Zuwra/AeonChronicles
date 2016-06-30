﻿using UnityEngine;
using System.Collections;

public class AttckWhileMoveState : UnitState{

	private Vector3 location;

	
	public AttckWhileMoveState(Vector3 loca, UnitManager man)
	{
		myManager = man;
		location = loca;
		//myMover.resetMoveLocation (location);
	}


	public override void initialize()
	{myManager.cMover.resetMoveLocation (location);
	}

	override
	public void Update () {

		if(myManager.enemies.Count > 0){
			GameObject closestEnemy = myManager.findClosestEnemy();
			foreach(IWeapon weap in myManager.myWeapon)
				if (weap.canAttack(closestEnemy))
		    		{
					weap.attack(closestEnemy, myManager);
					}
			}

		if (myManager.cMover.move ()) 
		{myManager.changeState(new DefaultState());}
		
	}


	override
	public void attackResponse(GameObject src)
	{
	}
}
