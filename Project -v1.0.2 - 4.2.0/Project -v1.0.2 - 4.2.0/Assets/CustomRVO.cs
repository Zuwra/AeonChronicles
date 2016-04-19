using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*#if RVOImp
 * using RVO;
 #endif*/
using Pathfinding;
using Pathfinding.RVO;

[HelpURL("http://arongranberg.com/astar/docs/class_r_v_o_example_agent.php")]
public class CustomRVO : IMover {
	public float repathRate = 1;

	private float nextRepath = 0;

	#if RVOImp
	private int agentID;
	#endif

	private bool pathSet;
	private Vector3 target;
	private bool canSearchAgain = true;

	private RVOController controller;
	private int currentWaypoint = 0;

	Path path = null;

	List<Vector3> vectorPath;
	int wp;

	#if RVOImp
	public bool astarRVO = true;
	#endif

	public float moveNextDist = 1;
	Seeker seeker;

	MeshRenderer[] rends;

	//IAgent rvoAgent;
	#if RVOImp
	public Vector3 position {
	get {
	if (astarRVO) return rvoAgent.InterpolatedPosition;
	else return RVO.Simulator.Instance.getAgentPosition3(agentID);
	//#else
	return rvoAgent.InterpolatedPosition;
	}
	}
	#endif

	public void Awake () {
		myFogger = GetComponent<FogOfWarUnit> ();
		initialSpeed = getMaxSpeed();
		seeker = GetComponent<Seeker>();
	}

	// Use this for initialization
	public void Start () {
		#if RVOImp
	
		#endif
		//resetMoveLocation(-transform.position); // + transform.forward * 400);
		controller = GetComponent<RVOController>();
	}


	override
	public void resetMoveLocation (Vector3 target) {
		this.target = target;
		currentWaypoint = 1;
		RecalculatePath();
	}



	public void RecalculatePath () {
		pathSet = true;
		canSearchAgain = false;
		nextRepath = Time.time+repathRate*(Random.value+0.5f);
		seeker.StartPath(transform.position, target, OnPathComplete);
	}

	public void OnPathComplete (Path _p) {
		ABPath p = _p as ABPath;
		//currentWaypoint = 0;
		canSearchAgain = true;

		if (path != null) path.Release(this);
		path = p;
		p.Claim(this);

		if (p.error) {
			wp = 0;
			vectorPath = null;
			return;
		}


		Vector3 p1 = p.originalStartPoint;
		Vector3 p2 = transform.position;
		p1.y = p2.y;
		float d = (p2-p1).magnitude;
		wp = 0;

		vectorPath = p.vectorPath;
		Vector3 waypoint;

		for (float t = 0; t <= d; t += moveNextDist*0.6f) {
			wp--;
			Vector3 pos = p1 + (p2-p1)*t;

			do {
				wp++;
				waypoint = vectorPath[wp];
				waypoint.y = pos.y;
			} while ((pos - waypoint).sqrMagnitude < moveNextDist*moveNextDist && wp != vectorPath.Count-1);
		}
	}

	public void Update () {
		
	}
	override
	public void stop ()
	{
		controller.Move (Vector3.zero);
	}
	override 
	public bool move()
	{
		if (Time.time >= nextRepath && canSearchAgain) {
			RecalculatePath();
		}



		if (path == null && !pathSet) {
			if (controller) {
				controller.Move (Vector3.zero);
			}
		
			return true;
		} else if (pathSet && path == null) {
			return false;}



		if (currentWaypoint >= path.vectorPath.Count) {
			speed = 0;

			path = null;
			pathSet = false;
			if (controller) {
				controller.Move (Vector3.zero);
			}
		
			return true;
		}

		if (speed < getMaxSpeed()) {
			speed += acceleration * Time.deltaTime;

			if (speed > getMaxSpeed()) {
				speed =  getMaxSpeed();
			}
		}

		//Direction to the next waypoint
	
		Vector3 dir = (path.vectorPath[currentWaypoint]-this.transform.position).normalized * speed;
		if (myFogger) {
			myFogger.move ();
		}
	
		controller.Move (dir);

	
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < 3) {

			currentWaypoint++;

		}

		return false;






		/*





		 dir = Vector3.zero;

		Vector3 pos = transform.position;

		if (vectorPath != null && vectorPath.Count != 0) {
			Vector3 waypoint = vectorPath[wp];
			waypoint.y = pos.y;

			while ((pos - waypoint).sqrMagnitude < moveNextDist*moveNextDist && wp != vectorPath.Count-1) {
				wp++;
				waypoint = vectorPath[wp];
				waypoint.y = pos.y;
			}

			dir = waypoint - pos;
			float magn = dir.magnitude;
			if (magn > 0) {
				float newmagn = Mathf.Min(magn, controller.maxSpeed);
				dir *= newmagn / magn;
			}
			//dir = Vector3.ClampMagnitude (waypoint - pos, 1.0f) * maxSpeed;
		}

		controller.Move(dir  * MaxSpeed);
		//AM I there yet?
		if (Vector3.Distance (this.gameObject.transform.position, target) < 3) {
			controller.Move (Vector3.zero);
			return true;
		} else {

			return false;
		}
		*/
	}



	override
	public void resetMoveLocation(Transform targ)

	{
	}

	void OnControllerColliderHit(ControllerColliderHit other)
	{
		if (other.gameObject.transform.position == target) {

			path = null;
		}
	}
}
