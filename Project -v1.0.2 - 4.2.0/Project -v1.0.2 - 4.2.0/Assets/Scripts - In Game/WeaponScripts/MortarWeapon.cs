using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MortarWeapon : IWeapon {




	public float minimumRange;
	public float attackArc;


	public override bool checkMinimumRange(UnitManager target)
	{
		float distance = Mathf.Sqrt((Mathf.Pow (transform.position.x - target.transform.position.x, 2) + Mathf.Pow (transform.position.z - target.transform.position.z, 2))) - target.CharController.radius;
		if ( distance < minimumRange) {

			return false;
		}
		return true;

	}


	public override bool inRange(UnitManager target)
	{
		if (this && target) {

			foreach (UnitTypes.UnitTypeTag tag in cantAttackTypes) {
				if (target.myStats.isUnitType (tag))
				{	
					return false;	}
			}


			float distance = Mathf.Sqrt((Mathf.Pow (transform.position.x - target.transform.position.x, 2) + Mathf.Pow (transform.position.z - target.transform.position.z, 2))) - target.CharController.radius;

			float verticalDistance = this.gameObject.transform.position.y - target.transform.position.y;

			if (distance > (range + (verticalDistance/3 )) ||  (minimumRange >0 && distance < minimumRange)) {


				return false;
			}
		} else {

			return false;}
		return true;

	}

	protected override GameObject createBullet()
	{GameObject proj;
		if (turret) {
			proj = myBulletPool.FastSpawn(turret.transform.rotation * firePoints [originIndex].position + this.gameObject.transform.position, Quaternion.identity);
		} else {
			proj= myBulletPool.FastSpawn(transform.rotation * firePoints[originIndex].position + this.gameObject.transform.position, Quaternion.identity);
		}
		MortarProjectile projScript = proj.GetComponent<MortarProjectile> ();
		projScript.arcAngle = attackArc;
		return proj;

	}

}
