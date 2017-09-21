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
	


		if (myManager.enemies.Count > 0) {
			if (myManager.myWeapon.Count >0) {

			
				UnitManager target = myManager.findBestEnemy ();


				if (target == null) {
					return;}
				
				if (Vector3.Distance (myManager.gameObject.transform.position, target.transform.position) <= myManager.getChaseRange ()) {
					//Debug.Log ("Chasing attacker " + target);
					myManager.changeState (new AttackMoveState (target.gameObject,
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
	public void endState()
	{
	}

	override
	public void attackResponse(UnitManager src, float amount)
	{	

		if(src){
		//UnitManager manage = src.GetComponent<UnitManager> ();

			if (src.PlayerOwner != myManager.PlayerOwner) {


					if (myManager.myWeapon.Count > 0) {
						if (myManager.isValidTarget(src)) {
						//Debug.Log ("Creating attack move  " + amount);
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
					if(amount > 0){
						foreach (UnitManager ally in myManager.allies) {
							if (ally) {
								if (myManager.gameObject != ally) {
									UnitState hisState = ally.getState ();
									if (hisState is DefaultState) {
									//Debug.Log ("Callign to" + ally.gameObject);
										hisState.attackResponse (src,0);
									}
								}
							}
						}
					}


			}
		}

	}

}
