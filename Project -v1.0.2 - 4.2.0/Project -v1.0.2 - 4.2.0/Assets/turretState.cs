using UnityEngine;
using System.Collections;

public class turretState : UnitState {


	private GameObject enemy;

	public turretState(UnitManager man, IMover move, IWeapon weapon )
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;

	

	

	}

	public override void initialize()
	{
	}

	// Update is called once per frame
	override
	public void Update () {
		if (!myWeapon) {
			return;}
		if (!myWeapon.isOffCooldown()) {
			return;
		}

		if (myManager.enemies.Count > 0) {
			Debug.Log ("Finding and enemty");

			enemy = findBestEnemy ();

		}

		if (!enemy) {

			return;
		}
	
		if (myWeapon.canAttack (enemy)) {
				
					//myManager.gameObject.transform.LookAt(enemy.transform.position);
					myWeapon.attack (enemy);

				}
			


	}


	override
	public void attackResponse(GameObject src)
	{}



	public GameObject findBestEnemy()
	{myManager.enemies.RemoveAll(item => item == null);
		GameObject best = null;


		float bestPriority = -1;

		for (int i = 0; i < myManager.enemies.Count; i ++) {
			if (myManager.enemies[i] != null) {

				float currDistance = Vector3.Distance(myManager.enemies[i].transform.position, this.myManager.gameObject.transform.position);
				if (currDistance > myWeapon.range) {
					
					continue;}

				if (!myWeapon.isValidTarget (myManager.enemies [i])) {

					continue;
				}
				if (myManager.enemies[i].GetComponent<UnitStats> ().attackPriority < bestPriority) {
					
					continue;
				}
				else 
				{best = myManager.enemies[i];
					
					bestPriority = myManager.enemies[i].GetComponent<UnitStats> ().attackPriority;
				}





			}
		}

		return best;
	}




}
