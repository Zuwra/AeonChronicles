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




	override
	public void stop()
	{GetComponent<UnitManager> ().animStop();
		myspeed = .1f;
	}

	override
	public bool move()
	{

		if (!workingframe) {
			workingframe = !workingframe;
			//Debug.Log ("Returnin 1 ");
			return false;
		}

		float tempDist = Vector3.Distance (transform.position, targetPosition);
		if (tempDist <= nextWaypointDistance) {
			myspeed = 0;
			//Debug.Log ("Returnin 2 ");
			return true;
		}
		if (myspeed< getMaxSpeed()) {
			myspeed += .1f * acceleration;

			if (myspeed > getMaxSpeed()) {
				myspeed = getMaxSpeed();
			}

		}
		dir = (targetPosition -transform.position).normalized;

		//Make sure your the right height above the terrain
		RaycastHit objecthit;
		RaycastHit objecthitB;
		Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);
	
		if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, 1 << 8)) {


			if (Physics.Raycast (transform.position + transform.forward *6 + Vector3.up*30, down, out objecthitB, 1000, 1 << 8)) {
				dir.y -= Time.deltaTime * (transform.position.y - ((objecthit.point.y + objecthitB.point.y)/2 + flyerHeight)) * (myspeed / 8) * Mathf.Min (3, tempDist);
			}
		

		}

		RaycastHit lookAhead = new RaycastHit();
		Vector3 vec = this.gameObject.transform.forward;

		if (Physics.Raycast (this.gameObject.transform.position, vec, out lookAhead, 7, 1 << 9)) {

			Vector3 heading = lookAhead.collider.gameObject.transform.position- transform.position;
			float dirNum = AngleDir (transform.forward, heading, transform.up);
			dir -= this.gameObject.transform.TransformDirection (Vector3.right) * dirNum;
		
		}
		dir *= myspeed * Time.deltaTime;

		controller.Move (dir);
		//Debug.Log ("air movin " + dir);
		if (myFogger) {
			myFogger.move ();
			}
		Vector3 turnAmount = targetPosition - transform.position;
		turnAmount.y = 0;
		if (turnAmount != Vector3.zero) {
			Quaternion toTurn = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (turnAmount), Time.deltaTime * turnSpeed * 0.2f);

			if (!float.IsNaN (toTurn.x) && !float.IsNaN (toTurn.y) && !float.IsNaN (toTurn.z) && !float.IsNaN (toTurn.w)) {
				transform.rotation = toTurn;
			}
		}
	//	Debug.Log ("Returnin 3 ");
		return false;
	}


	float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);

		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
	}



	override
	public void resetMoveLocation(Vector3 location)
	{//	location.y += 2;


		RaycastHit objecthit;
	
		if (Physics.Raycast (location + Vector3.up *30, Vector3.down, out objecthit, 1000, 1 << 8)) {
			//if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, (~8))) {
		
			targetPosition = objecthit.point + Vector3.up * flyerHeight;

		} else {
			targetPosition = location + Vector3.up * flyerHeight;
		}
			

		//Debug.Log ("Moving to " + location + "   but acvtually " + targetPosition);
		if (myspeed == 0) {
			myspeed = .1f;
		}
		//targetPosition = location + Vector3.up * flyerHeight;
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
