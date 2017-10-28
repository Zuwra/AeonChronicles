using UnityEngine;
using System.Collections;

public class AttackMoveState : UnitState {

	public enum MoveType{passive, command, patrol};
	private MoveType commandType;

	private Vector3 home;// to be used if its a passive attack move and patrol;
	private Vector3 endLocation; // end location of an attack move
	private UnitManager enemy;
	private Vector3 target; // currently moving towards

	private bool enemyDead = false;

	private float nextActionTime = 0;


		public AttackMoveState(GameObject obj, Vector3 location, MoveType type,UnitManager man, Vector3 myhome )
		{
		myManager = man;

		home = myhome;
		if (obj) {
			enemy = obj.GetComponent<UnitManager> ();
		}
		endLocation = location;
		commandType = type;
		if (enemy != null) {
			target = enemy.transform.position;
		} else {
			target = endLocation;
		}


		nextActionTime = Time.time + .1f;
		//Debug.Log("Just called th reset1" + target + "   "+ enemy);
		if (type == MoveType.passive) {
			// This is breaking stuff so I commented it out
			//target = home;
		} else if (type == MoveType.command) {
			
			home = location;
			enemyDead = true;
		} else {
			enemyDead = true;
		}
		//Debug.Log ("Target is " + obj + " locat " + target);
		}

	public override void initialize()
	{if (myManager) {
		
			//Debug.Log ("has manager");
		}
		if (myManager.cMover) {
			//Debug.Log (myManager.cMover);
			myManager.cMover.resetMoveLocation (target);
		}
	}

	bool EnemyTooClose = false;
	Vector3 lastEnemyLocation;
	// Update is called once per frame
	override
	public void Update () {
		
		if (Time.time > nextActionTime) {
			nextActionTime += .2f;
			UnitManager temp =  myManager.findBestEnemy ();



			if (temp) {
				if (temp == enemy ){
					if(Vector3.Distance (lastEnemyLocation, temp.transform.position) > 3) {
						if (myManager.cMover) {
							lastEnemyLocation = enemy.transform.position;
							if (EnemyTooClose) {
								myManager.cMover.resetMoveLocation (myManager.transform.position - (enemy.transform.position - myManager.transform.position).normalized * 10);
							} else {
								myManager.cMover.resetMoveLocation (enemy.transform.position);
							}
						}
					}	
				} else {

					enemy = temp;


					if (myManager.cMover) {
						lastEnemyLocation = enemy.transform.position;
						if (EnemyTooClose) {
							myManager.cMover.resetMoveLocation (myManager.transform.position - (enemy.transform.position - myManager.transform.position).normalized * 10);
						} else {
							myManager.cMover.resetMoveLocation (enemy.transform.position);
						}
					}
				}

			} else if(enemy && Vector3.Distance(myManager.gameObject.transform.position,enemy.transform.position) > 85 ){
				enemy = null;
				myManager.cMover.resetMoveLocation (home);
			}
		}
		//still need to figure out calculation for if enemy goes out of range or if a better one comes into range


		if (enemy != null && myManager.myWeapon.Count > 0) {
			enemyDead = false;
			bool attacked = false;
			foreach (IWeapon weap in myManager.myWeapon) {
				if (weap.inRange (enemy)) {


					if (myManager.cMover) {
						
						myManager.cMover.stop ();
					}
					attacked = true;
					if (weap.simpleCanAttack (enemy)) {
						weap.attack (enemy, myManager);
					} 
				} else {
					EnemyTooClose =	!weap.checkMinimumRange (enemy);
					
				}

			}

			if (!attacked) {
				
				if (myManager.cMover.myspeed == 0) {
					lastEnemyLocation = enemy.transform.position;
					if (EnemyTooClose) {

						myManager.cMover.resetMoveLocation ( myManager.transform.position - ( enemy.transform.position - myManager.transform.position ).normalized *10);
						//Debug.Log ("Moving Away him " + enemy.transform.position + "  " + ( myManager.transform.position - ( enemy.transform.position - myManager.transform.position ).normalized *10));
					} else {
					//	Debug.Log ("Towards");
						myManager.cMover.resetMoveLocation (enemy.transform.position);
					}
				}

				//bool returned = 
				myManager.cMover.move ();
				//Debug.Log ("A " +myManager + "  " + myManager.cMover + " 0----" + returned);
			}
		} else {
			if (!enemyDead) {
				if (commandType == MoveType.command) {
					//Debug.Log("continuing on");
					myManager.cMover.resetMoveLocation (endLocation);
				} else if (commandType == MoveType.passive) {//Debug.Log("going home");
					if (myManager.cMover) {
	
						myManager.cMover.resetMoveLocation (home);
					}
				} else {
					if (myManager.cMover) {

						myManager.cMover.resetMoveLocation (target);
					}
				}
				enemyDead = true;
			}

			if (myManager.cMover) {
				//Debug.Log ("MovingB");
				bool there = myManager.cMover.move ();
				if (there && commandType == MoveType.patrol) {
					
					if (target == home) {
					
						target = endLocation;
					} else {
				
						target = home;
					}

					myManager.cMover.resetMoveLocation (target);
				} else if (there) {

					myManager.changeState (new DefaultState ());
				}

			}
		}


	}



	public void setHome(Vector3 input)
		{home = input;}

	override
	public void endState()
	{
	}

	bool hasCalledAide = false;

	override
	public void attackResponse(UnitManager src, float amount)
	{
		if(src && !hasCalledAide){

			if (src.PlayerOwner != myManager.PlayerOwner) {
				hasCalledAide = true;
					if(amount > 0){
						foreach (UnitManager ally in myManager.allies) {
							if (ally) {
								UnitState hisState = ally.getState ();
								if (hisState is DefaultState) {
									hisState.attackResponse (src, 0);
								}

							}
						}
					}

			}
		}

	}


}
