using UnityEngine;
using System.Collections;

public class turretState : UnitState {


	private UnitManager enemy;

	public turretState(UnitManager man)
	{
		myManager = man;

	

	}

	public override void initialize()
	{
	}

	override
	public void endState()
	{
	}

	// Update is called once per frame
	override
	public void Update () {
		
		if (myManager.myWeapon.Count ==0) {
			return;}
		//if (!myWeapon.isOffCooldown()) {
		//	return;
		//}

		//Debug.Log ("Turret Update " + myManager.gameObject + "  " + myManager.myWeapon.Count);
		if (myManager.enemies.Count > 0) {
			enemy = findBestEnemy ();

		}
	
		if (!enemy) {

			return;
		}
	
		IWeapon myWeap = myManager.canAttack (enemy);
		if (myWeap) {
				
					//myManager.gameObject.transform.LookAt(enemy.transform.position);
			myWeap.attack (enemy,myManager);

				}
			


	}


	override
	public void attackResponse(UnitManager src, float amount)
	{}



	public UnitManager findBestEnemy()
	{myManager.enemies.RemoveAll(item => item == null);
		UnitManager best = null;


		float bestPriority = -1;

		for (int i = 0; i < myManager.enemies.Count; i ++) {
			if (myManager.enemies[i] != null) {

				//float currDistance = Vector3.Distance(myManager.enemies[i].transform.position, this.myManager.gameObject.transform.position);

				IWeapon myWeap = myManager.isValidTarget (myManager.enemies [i]);
				if (!myWeap) {

					continue;
				}
				if (!myWeap.inRange(myManager.enemies[i])) {

					continue;}

				if (myManager.enemies[i].myStats.attackPriority < bestPriority) {

					continue;
				}
				else 
				{best = myManager.enemies[i];

					bestPriority = myManager.enemies[i].myStats.attackPriority;
				}

			}
		}

		return best;
	}




}
