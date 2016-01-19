using UnityEngine;
using System.Collections;

public class HoldState : UnitState {
	



	public HoldState(UnitManager man, customMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;
	}


	// Update is called once per frame
	override
	public void Update () {
		GameObject enemy = myManager.findBestEnemy();

		if (enemy) {
			if (myWeapon.canAttack(enemy)) {
				if (Vector3.Distance (enemy.transform.position, myManager.gameObject.transform.position) < myWeapon.range) {
					myWeapon.attack(enemy);
				}
			}
		}
	}
	public override void initialize()
	{
	}


	override
	public void attackResponse(GameObject src)
	{}

}
