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
	private int refreshTime = 10;
	private int currentFrame = 0;


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


		//Debug.Log("Just called th reset1" + target + "   "+ enemy);
		if (type == MoveType.passive) {
			// This is breaking stuff so I commented it out
			//target = home;
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

	// Update is called once per frame
	override
	public void Update () {
	//	Debug.Log ("I am " + myManager.gameObject + "   in attackMove");

		currentFrame++;


		if (currentFrame > refreshTime) {
			currentFrame = 0;
			UnitManager temp =  myManager.findBestEnemy ();

			if (temp) {
			
				enemy = temp;
				if (myManager.cMover) {
		
					myManager.cMover.resetMoveLocation (enemy.transform.position);
				}

			} else if(enemy && Vector3.Distance(myManager.gameObject.transform.position,enemy.transform.position) > 100){
				myManager.cMover.resetMoveLocation (home);
			}
		}
		//still need to figure out calcualte ion for if enemy goes out of range or if a better one comes into range
		//Debug.Log("Checking");
			
	

		if (enemy != null && myManager.myWeapon.Count > 0) {
			enemyDead = false;
			bool attacked = false;
			foreach (IWeapon weap in myManager.myWeapon) {
				if (weap.inRange (enemy)) {

					if (myManager.cMover) {
						
						myManager.cMover.stop ();

					}
					attacked = true;
					if (weap.canAttack (enemy)) {
						//Debug.Log ("Attacking " + enemy.gameObject + "   " + Time.time);
						weap.attack (enemy, myManager);
					} 
				} 
			}
			//Debug.Log ("After attack "+ "   " + Time.time);
			if (!attacked) {
				if (myManager.cMover.speed == 0) {
					myManager.cMover.resetMoveLocation (enemy.transform.position);
				}

				myManager.cMover.move ();
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

	override
	public void attackResponse(UnitManager src, float amount)
	{
		if(src){

			if (src.PlayerOwner != myManager.PlayerOwner) {

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
