using UnityEngine;
using System.Collections;

public class airmover : IMover {


	private Vector3 targetPosition;
	private CharacterController controller;
	//The calculated path
	public float turnSpeed;
	//The AI's speed per second

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;

	private bool workingframe = false;

	private Vector3 dir;

	public float flyerHeight;



	public void Start () {

		controller = GetComponent<CharacterController>();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		//seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		initialSpeed = getMaxSpeed();
	}



	public void Update () {

	}


	override
	public void stop()
	{GetComponent<UnitManager> ().animStop();
	}

	override
	public bool move()
	{

		if (!workingframe) {
			workingframe = !workingframe;
			//Debug.Log ("Returnin 1 ");
			return false;
		}
			
		if (Vector3.Distance(transform.position, targetPosition) <= nextWaypointDistance) {
			speed = 0;
			//Debug.Log ("Returnin 2 ");
			return true;
		}
		if (speed < getMaxSpeed()) {
			speed += .1f * acceleration;

			if (speed > getMaxSpeed()) {
				speed = getMaxSpeed();
			}

		}
		dir = (targetPosition -transform.position).normalized;

		//Make sure your the right height above the terrain
		RaycastHit objecthit;
		Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

		if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, 1 << 8)) {
		//if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, (~8))) {

			dir.y -= Time.deltaTime *  (this.gameObject.transform.position.y -(objecthit.point.y + flyerHeight) ) *speed/2;

		}

		dir *= speed * Time.deltaTime;
		controller.Move (dir);
		//Debug.Log ("air movin " + dir);
		if (myFogger) {
			myFogger.move ();
			}
		Vector3 turnAmount = targetPosition - transform.position;
		turnAmount.y = 0;
	
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(turnAmount), Time.deltaTime * turnSpeed *  0.2f);
	//	Debug.Log ("Returnin 3 ");
		return false;
	}


	override
	public void resetMoveLocation(Vector3 location)
	{//	location.y += 2;
		if (speed == 0) {
			speed = .1f;
		}
		targetPosition = location + Vector3.up * flyerHeight;
	//Debug.Log ("Target is " + location);
		GetComponent<UnitManager> ().animMove ();
		//this.gameObject.transform.LookAt(destination);


		workingframe = false;
	
		//queueTargetLocation(location);s

	}

	override
	public void resetMoveLocation(Transform targ)

	{
	}




}
