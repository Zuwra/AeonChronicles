using UnityEngine;
using System.Collections;

public class DefaultState : UnitState{

	// Update is called once per frame

	public DefaultState(UnitManager man, IMover move, IWeapon weapon)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;

	}


	override
	public void Update () {// change this later so t will only check for attackable enemies.
		if (myWeapon != null) {
			if (myManager.enemies.Count > 0) {

				//Debug.Log("best enemey is" + myManager.findBestEnemy());
				GameObject target = myManager.findBestEnemy ();
				if (Vector3.Distance (myManager.gameObject.transform.position, target.transform.position) <= myManager.getChaseRange ()) {
					myManager.changeState (new AttackMoveState (myManager.findBestEnemy (),
						new Vector3 (), AttackMoveState.MoveType.passive, myManager, myMover, myWeapon, myManager.gameObject.transform.position));
				}
			}
		}
	
	
	}


	override
	public void attackResponse(GameObject src)
	{
		UnitManager manage = src.GetComponent<UnitManager> ();
		if (manage.PlayerOwner != myManager.PlayerOwner) {
	


			if (myWeapon.isValidTarget (src)) {
				myManager.GiveOrder (Orders.CreateAttackMove (src.transform.position));
			}

			else {
				Vector3 spot = (myManager.transform.position + (myManager.transform.position - src.transform.position)* .4f);
				spot.y += 100;
				Ray ray = new Ray(spot, Vector3.down);

				RaycastHit hit;
				Vector3 dest = new Vector3();
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~8))
				{
					dest = hit.point;
				}

				myManager.GiveOrder(Orders.CreateAttackMove(dest));
			}



		}

	}

}
