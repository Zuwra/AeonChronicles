using UnityEngine;
using System.Collections;

public class AttackMoveState : UnitState {

	public enum MoveType{passive, command, patrol};
	private MoveType commandType;

	private Vector3 home;// to be used if its a passive attack move and patrol;
	private Vector3 endLocation; // end location of an attack move
	private GameObject enemy;
	private Vector3 target; // currently moving towards

	private bool enemyDead = false;
	private int refreshTime = 4;
	private int currentFrame = 0;


		public AttackMoveState(GameObject obj, Vector3 location, MoveType type,UnitManager man, Vector3 myhome )
		{
		myManager = man;

		home = myhome;
		enemy = obj;
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
			Debug.Log (myManager.cMover);
			myManager.cMover.resetMoveLocation (target);
		}
	}

	// Update is called once per frame
	override
	public void Update () {

		currentFrame++;


		if (currentFrame > refreshTime) {
			currentFrame = 0;
			GameObject temp =  myManager.findBestEnemy ();

			if (temp && temp != enemy) {
			
				enemy = temp;
			
					//myManager.gameObject.transform.LookAt (enemy.transform.position);
				if (myManager.cMover) {
					myManager.cMover.resetMoveLocation (enemy.transform.position);
				}
					//Debug.Log("Just called th reset2" + enemy.transform.position);
				

			}
		}
		//still need to figure out calcualte ion for if enemy goes out of range or if a better one comes into range

			
	

		if (enemy != null && myManager.myWeapon.Count > 0) {
			enemyDead = false;
			bool attacked = false;
			foreach (IWeapon weap in myManager.myWeapon) {
				if (weap.inRange (enemy)) {
					//Debug.Log ("Stopping me");
					if (myManager.cMover) {
						myManager.cMover.stop ();
					}
					attacked = true;
					if (weap.canAttack (enemy)) {
						
						weap.attack (enemy, myManager);
					} 
				} 
			}

			if (!attacked) {

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
	public void attackResponse(GameObject src)
	{
		if(src){
			UnitManager manage = src.GetComponent<UnitManager> ();
			if (manage) {
				if (manage.PlayerOwner != myManager.PlayerOwner) {

			
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
