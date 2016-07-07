using UnityEngine;
using System.Collections;

public class DefaultState : UnitState{

	// Update is called once per frame

	public DefaultState()//UnitManager man, IMover move, IWeapon weapon)
	{
		//myManager = man;
		//myMover = move;
		//myWeapon = weapon;

	}


	override
	public void Update () {// change this later so t will only check for attackable enemies.
		if (myManager.myWeapon.Count >0) {
			if (myManager.enemies.Count > 0) {

				GameObject target = myManager.findBestEnemy ();

				if (target == null) {
					return;}
				if (Vector3.Distance (myManager.gameObject.transform.position, target.transform.position) <= myManager.getChaseRange ()) {
					//Debug.Log ("Chasing attacker " + target);
					myManager.changeState (new AttackMoveState (target,
						new Vector3 (), AttackMoveState.MoveType.passive, myManager, myManager.gameObject.transform.position));
				}
			}
		}
	
	
	}

	public override void initialize()
	{if (myManager.cMover) {
			myManager.cMover.stop ();
		}
	}


	override
	public void attackResponse(GameObject src)
	{	
		if(src){
		UnitManager manage = src.GetComponent<UnitManager> ();
			if (manage) {
				if (manage.PlayerOwner != myManager.PlayerOwner) {
	
					Debug.Log ("Default State damage response");



					if (myManager.myWeapon.Count > 0) {
						if (myManager.isValidTarget(src)) {
							Debug.Log ("Attack moving");
							myManager.GiveOrder (Orders.CreateAttackMove (src.transform.position));
						} else {
							Vector3 spot = (myManager.transform.position + (myManager.transform.position - src.transform.position) * .4f);
							spot.y += 100;
							Ray ray = new Ray (spot, Vector3.down);

							RaycastHit hit;
							Vector3 dest = new Vector3 ();
							if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~8)) {
								dest = hit.point;
							}

							myManager.GiveOrder (Orders.CreateAttackMove (dest));
						}

					}
					// Inform other alleis to also attack
					foreach (GameObject ally in myManager.allies) {
						if (ally){
							UnitState hisState= ally.GetComponent<UnitManager> ().getState ();
							if (hisState is DefaultState) {
								hisState.attackResponse (src);
							}

						}
					}

				}
			}
		}

	}

}
