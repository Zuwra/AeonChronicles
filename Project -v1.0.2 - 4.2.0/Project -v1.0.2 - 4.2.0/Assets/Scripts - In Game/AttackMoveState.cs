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


		public AttackMoveState(GameObject obj, Vector3 location, MoveType type,UnitManager man, IMover move, IWeapon weapon, Vector3 myhome )
		{
		myManager = man;
		myMover = move;
		myWeapon = weapon;

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
			target = home;
		} else {
			enemyDead = true;
		}

		}

	public override void initialize()
	{myMover.resetMoveLocation (target);
	}

	// Update is called once per frame
	override
	public void Update () {

		currentFrame++;


		if (currentFrame > refreshTime) {
			currentFrame = 0;
			enemy = myManager.findBestEnemy ();

			if(enemy){
				myMover.resetMoveLocation(enemy.transform.position);
				//Debug.Log("Just called th reset2" + enemy.transform.position);
				
			}
		}
		//still need to figure out calcualte ion for if enemy goes out of range or if a better one comes into range

			
	

		if (enemy != null && myWeapon != null) {
			enemyDead = false;
			if(myWeapon.inRange(enemy)){
				if (myWeapon.canAttack (enemy)) {
					
					myWeapon.attack (enemy);
				} else {
					
				}
			}
			else{
				
				myMover.move();
			}
		}

		else {
			if(!enemyDead){
			if(commandType == MoveType.command){
					//Debug.Log("continuing on");
					myMover.resetMoveLocation(endLocation);}
				else
				{//Debug.Log("going home");
					
					myMover.resetMoveLocation(home);}
				enemyDead = true;
			}

			bool there = myMover.move();
			if(there && commandType == MoveType.patrol)
				{target = home;
				myMover.resetMoveLocation(home);}
			else if(there)
			{
				myManager.changeState(new DefaultState());
			}

		}


	}



	public void setHome(Vector3 input)
		{home = input;}



	override
	public void attackResponse(GameObject src)
	{}


}
