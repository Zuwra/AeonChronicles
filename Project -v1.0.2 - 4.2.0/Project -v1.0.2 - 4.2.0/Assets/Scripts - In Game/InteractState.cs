using UnityEngine;
using System.Collections;

public class InteractState : UnitState {

	private UnitManager target;



	private int refreshTime = 15;
	//private int currentFrame = 0;
	private float nextActionTime;
	private IWeapon bestWeap;

	public InteractState(GameObject unit, UnitManager man)
	{
		myManager = man;


		target = unit.GetComponent<UnitManager>();
	//myMover.resetMoveLocation (target.transform.position);
		nextActionTime = Time.time + .7f;
		bestWeap = myManager.canAttack (target);	

	
		refreshTime = 30 - (int)myManager.cMover.getMaxSpeed();
		if (refreshTime < 5) {
			refreshTime = 8;
		}
	}


	public override void initialize()
	{myManager.cMover.resetMoveLocation (target.transform.position);
	}

	override
	public void endState()
	{
	}

	// Update is called once per frame
	override
	public void Update () {
		if (!target) {
			myManager.changeState(new DefaultState());
			return;
		}


		if (!myManager.inRange(target)  ) {
			//Debug.Log ("I am not in range");

			if (!bestWeap) {
				bestWeap = myManager.canAttack (target);
			}
			if ((bestWeap && bestWeap.isOffCooldown ()) || !bestWeap) {
				myManager.cMover.move ();
			} else {
			
				return;}

		} else {
			//Debug.Log ("Totally in range");
			myManager.cMover.stop ();
			IWeapon myWeap = myManager.canAttack (target);	

			if (myWeap) {
				//Debug.Log ("Attacking");
				myWeap.attack (target,myManager);
			
			}
			return;
		}

	
		if ( Time.time > nextActionTime) {
			nextActionTime = Time.time + .7f;

			myManager.cMover.resetMoveLocation(target.transform.position);
		}

		//attack



		
	
	}


	override
	public void attackResponse(UnitManager src, float amount)
	{
	}

}
