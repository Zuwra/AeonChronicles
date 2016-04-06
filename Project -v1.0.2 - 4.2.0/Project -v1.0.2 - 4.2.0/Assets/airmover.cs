using UnityEngine;
using System.Collections;

public class airmover : IMover {









	private Vector3 targetPosition;
	private CharacterController controller;
	//The calculated path

	//The AI's speed per second

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;

	private bool workingframe = false;

	private Vector3 dir;

	public float flyerHeight;


	private float nextActionTime;

	public void Start () {
		nextActionTime = Time.time;
		controller = GetComponent<CharacterController>();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		//seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		initialSpeed = getMaxSpeed();
	}



	public void Update () {

	}


	override
	public void stop()
	{
	}

	override
	public bool move()
	{if (!workingframe) {
			workingframe = !workingframe;
			return false;
		}
			
		if (Vector3.Distance(transform.position, targetPosition) <= nextWaypointDistance) {
			speed = 0;

			return true;
		}
		if (speed < getMaxSpeed()) {
			speed += .1f * acceleration;

			if (speed > getMaxSpeed()) {
				speed = getMaxSpeed();
			}
		}

		//Direction to the next waypoint
		dir = (targetPosition -transform.position).normalized;
		dir *= speed * Time.deltaTime;
		controller.Move (dir);



		if (Time.time > nextActionTime) {
			nextActionTime += .6f;

			RaycastHit objecthit;
			Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

			if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, (8))) {
			

				while (this.gameObject.transform.position.y < objecthit.point.y + 24) {
					controller.Move (Vector3.up * .2f);

				}

				while (this.gameObject.transform.position.y > objecthit.point.y + 26) {
					controller.Move (Vector3.down * .2f);
			
				}
			}
		}


		return false;
	}


	override
	public void resetMoveLocation(Vector3 location)
	{//	location.y += 2;

		Vector3 destination = new Vector3(location.x, this.gameObject.transform.position.y, location.z);

		this.gameObject.transform.LookAt(destination);

		destination.y = location.y + flyerHeight;
		workingframe = false;
		targetPosition = destination;

		//queueTargetLocation(location);

	}

	override
	public void resetMoveLocation(Transform targ)

	{
	}


}
