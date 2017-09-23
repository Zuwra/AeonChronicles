using UnityEngine;
using System.Collections;

public class HoldState : UnitState {
	



	public HoldState(UnitManager man)
	{
		myManager = man;
	}


	// Update is called once per frame
	override
	public void Update () {

		if (myManager) {
			UnitManager enemy = myManager.findBestEnemy ();

			if (enemy) {
				IWeapon myWeap = myManager.canAttack (enemy);
				if (myWeap) {

					myWeap.attack (enemy, myManager);

				}
			}
		}
	}
	public override void initialize()
	{
	}
	override
	public void endState()
	{
	}

	override
	public void attackResponse(UnitManager src, float amount)
	{}

}
