using UnityEngine;
using System.Collections;
using Pathfinding;
public class customMover : IMover {
	//The point to move to
	private Vector3 targetPosition;
	private Seeker seeker;
	private CharacterController controller;
	//The calculated path
	public Path path;
	//The AI's speed per second

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	private bool workingframe = false;

	private Vector3 dir;

	public void Awake () {
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		//seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	}


	public void OnPathComplete (Path p) {


	//	Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		} else {
			path = p;
			Debug.Log("errer:" +p.error);
		}
		if (currentWaypoint < p.vectorPath.Count) {

			Vector3 target = path.vectorPath[currentWaypoint];
			//target.y+=2;

			target.y = this.transform.position.y;


			this.gameObject.transform.LookAt (target);
		}
	}


	public void Update () {

	}
	override
	public bool move()
	{// for some reason the updates are being called out of order so this is here,
		if (!workingframe) {
			workingframe = !workingframe;
			return false;
		}

		if (path == null) {
			//We have no path to move after yet
		
			return true;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			speed = 0;
			
			path = null;

			return true;
		}
		if (speed < MaxSpeed) {
			speed += .1f * acceleration;
			
			if (speed > MaxSpeed) {
				speed = MaxSpeed;
			}
		}
		//Direction to the next waypoint
		dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.deltaTime;
		controller.Move (dir);
		//controller.SimpleMove (dir);
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		
		if (path == null) {
			return true;
		}
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			
			currentWaypoint++;
			if(currentWaypoint == path.vectorPath.Count)
			{path = null;

				return true;
			}
			Vector3 target = path.vectorPath[currentWaypoint];
			//target.y+=2;

			target.y = this.transform.position.y;


			this.gameObject.transform.LookAt (target);

			
		}
		return false;
	}


	override
	public void resetMoveLocation(Vector3 location)
	{//	location.y += 2;

		workingframe = false;
		targetPosition = location;
		currentWaypoint = 0;
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		//queueTargetLocation(location);

	}



	void OnControllerColliderHit(ControllerColliderHit other)
	{
		if (other.gameObject.transform.position == targetPosition) {
			Debug.Log ("Colliding with " + other.gameObject);
			path = null;
		}
	}

} 