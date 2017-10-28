using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleWeapon : IWeapon{


	public float angleOfAttack;

	public override bool canAttack(UnitManager target)
	{

		if (!offCooldown) {
			return false;}
		if (!target) {
			return false;}

		foreach (Validator val in validators) {
			if(val.validate(this.gameObject,target.gameObject) == false)
			{
				return false;}
		}

		if (myManager.getState () is AttckWhileMoveState) {
			if (angleOfAttack == 0 || Vector3.Angle (transform.forward, (target.transform.position - transform.position)) > angleOfAttack) {
			
				return false;
			}
		}
		// Account for height advantage
		float distance = Mathf.Sqrt((Mathf.Pow (transform.position.x - target.transform.position.x, 2) + Mathf.Pow (transform.position.z - target.transform.position.z, 2))) - target.CharController.radius ;

		float verticalDistance = this.gameObject.transform.position.y - target.transform.position.y;
		if (distance > (range + (verticalDistance/3))) {

			return false;}

		foreach (UnitTypes.UnitTypeTag tag in cantAttackTypes) {
			if (target.myStats.isUnitType (tag))
			{return false;	}
		}

		return true;

	}


}
