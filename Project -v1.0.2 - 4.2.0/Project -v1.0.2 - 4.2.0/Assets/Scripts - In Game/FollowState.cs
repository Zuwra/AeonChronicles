using UnityEngine;
using System.Collections;

public class FollowState : UnitState {

	private GameObject target;


	private float refreshTime = 5;

	private float followRadius = 0;
	bool isStopped =false;

	public FollowState(GameObject unit, UnitManager man)
	{
		myManager = man;

		refreshTime = Time.time + .5f;
		Debug.Log ("New Follow state" );
		target = unit;
		followRadius = man.GetComponent<CharacterController> ().radius + target.GetComponent<CharacterController>().radius + 4;
		//myMover.resetMoveLocation (target.transform.position);

	}

	public override void initialize()
	{myManager.cMover.resetMoveLocation (target.transform.position);

		refreshTime = 30 - (int)myManager.cMover.getMaxSpeed();
		if (refreshTime < 5) {
			refreshTime = 8;
		}
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

		//Debug.Log ("Time is " + Time.time);
		if (Time.time > refreshTime) {

			//Debug.Log ("Refreshing");
			refreshTime = Time.time + .6f;
			isStopped = false;
			myManager.cMover.resetMoveLocation(target.transform.position);
		}

		if (Vector3.Distance (myManager.gameObject.transform.position, target.transform.position) > followRadius) {
			
			myManager.cMover.move ();
		} else if(!isStopped){
			myManager.cMover.stop ();
		}
		//attack


	}

	override
	public void attackResponse(GameObject src, float amount)
	{
	}


}
