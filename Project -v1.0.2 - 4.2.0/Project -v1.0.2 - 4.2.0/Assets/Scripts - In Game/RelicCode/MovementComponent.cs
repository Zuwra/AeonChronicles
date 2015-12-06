using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;


public class MovementComponent : MonoBehaviour {

	public float turningSpeed;//0 is instantaneous
	public bool moveWhileTurning;
	public float acceleration;
	public float MaxSpeed;
	private float currentSpeed;
	public bool isMoving;

	private float targetAngle = 15;

	public float seperationRadius;
	public Queue<Vector3> targetLocations = new Queue<Vector3> ();



	private float currentYVelocity = 0.0f;
	//private Vector3 currentMoveAmount = Vector3.zero;


    private bool stunned = false;
	private float elapsedStunTime = 0f;
	private float targetStunTime = 0f;

	private Vector3 previousVelocity;
	private CharacterController controller;
	private Quaternion targetRotation;

	private UnitManager manage;

	// Use this for initialization
	void Start () {manage = this.gameObject.GetComponent<UnitManager> ();
	
		controller = this.gameObject.GetComponent<CharacterController> ();
	}

	void Update(){

		/*

		//Need to add functionality for attack Moving
		if (stunned) {
			elapsedStunTime += Time.deltaTime;
			if (elapsedStunTime >= targetStunTime) {
				unstun ();
				elapsedStunTime = 0f;
				targetStunTime = 0f;
			}
		} else if (targetLocations.Count > 0) {

			if(currentSpeed > MaxSpeed)
			{currentSpeed = MaxSpeed;}


			manage.currentState = UnitManager.state.move;
		

			if(turningSpeed == 0)
				{


				this.gameObject.transform.LookAt(targetLocations.Peek());


			}


			else{



				targetRotation  = Quaternion.LookRotation(targetLocations.Peek()); 
				
				targetRotation.z=0; 
				targetRotation.x=0; 
			
				
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);


				while(manage.currentState == UnitManager.state.move && Vector3.Angle(this.gameObject.transform.forward, -this.gameObject.transform.position +targetLocations.Peek()) > targetAngle  )
				{currentSpeed -= .1f*acceleration;
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed );
				}
			}

			if(moveWhileTurning)
				{accelerate();
			
				controller.Move(transform.TransformDirection(Vector3.forward * currentSpeed));
			


			
			}

			else if (Vector3.Angle(this.gameObject.transform.forward, -this.gameObject.transform.position +targetLocations.Peek()) < targetAngle )
			{

				accelerate();
				controller.Move(transform.TransformDirection(Vector3.forward * currentSpeed));

			}


			if (Vector3.Distance (this.gameObject.transform.position, targetLocations.Peek()) < seperationRadius) {
				targetLocations.Dequeue();
				
			}
	
		
		} else {

				
		}


*/


	}

	private void accelerate()
	{
		if(currentSpeed < MaxSpeed)
		{currentSpeed += .1f*acceleration;

		
			if(currentSpeed > MaxSpeed)
			{currentSpeed = MaxSpeed;}
		}
		targetAngle = 25;
	

	}

	public void decelerate ()
		{

		}



	public void clearLocations()
	{targetLocations.Clear ();
	}





	public bool queueTargetLocation(Vector3 location)
	{//figure out pathing
		Seeker seeker = GetComponent<Seeker>();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function

		if (location == null) {
			Debug.Log("Hi");
		}
		seeker.StartPath (this.gameObject.transform.position,location, OnPathComplete );



		location.y += 2;
		targetLocations.Enqueue(location);
		return true;
	
	}

	public void OnPathComplete (Path p) {

		Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
	}

	public void resetMoveLocation(Vector3 location)
	{	location.y += 2;
		targetLocations.Clear ();
		queueTargetLocation(location);
	
	}


	// Update is called once per frame
	/*void FixedUpdate () {

			// Dampen the current momentum and add it to the move vector.
			momentum = Vector3.MoveTowards(momentum, Vector3.zero, momentumDampener);
			currentMoveAmount += momentum;
			bool grounded = false;


			if(affectedByGravity && !grounded && gravityPriority >= currentPriority){
				currentMoveAmount += currentYVelocity * Vector3.up * Time.deltaTime;
				currentYVelocity += Physics.gravity.y * Time.deltaTime;
			}

			Vector3 positionBeforeMove = transform.position;
			if(!currentMoveAmount.Equals(Vector3.zero)){
				if (_extendedController != null)
				{
					_extendedController.Move(currentMoveAmount);
				}
				else
				{
					_controller.Move(currentMoveAmount);
				}
			}
			
			previousVelocity = (transform.position - positionBeforeMove) * (1.0f / Time.deltaTime);
			
			currentMoveAmount = Vector3.zero;
			currentPriority = int.MinValue;
		}

	}

	public void AddMomentum(Vector3 amount){
		this.momentum += amount;
//		Debug.Log ("momentum" + momentum);
	}

	public void Move(int priority, Vector3 amount) {
        if (!stunned)
		{
            if (_entityBase == null || (_entityBase != null && _entityBase.IsActivated))
            {
                if (priority > currentPriority)
                {
                    currentPriority = priority;
                    currentMoveAmount = Vector3.zero;
                }

                if (priority == currentPriority)
                {
                    currentMoveAmount += amount;
                }
            }
        }

	}

	public void ResetVelocity() {
		currentMoveAmount = Vector3.zero;
		currentYVelocity = 0;
	}

	private void HandleHit(Vector3 normal)
	{
		if (affectedByGravity && normal.y > groundAngleTolerance)
		{
			currentYVelocity = 0.0f;
		}
	}

	public void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (_extendedController == null)
		{
			HandleHit(hit.normal);
		}
	}
	
	public void OnExtendedControllerColliderHit(ExtendedControllerColliderHit hit)
	{
		HandleHit(hit.normal);
	}

	public bool AffectedByGravity
	{
		get
		{
			return affectedByGravity;
		}

		set
		{
			affectedByGravity = value;
			currentYVelocity = 0.0f;
		}
	}

	public bool IsGrounded
	{
		get
		{
			if (_extendedController != null)
			{
				return _extendedController.IsGrounded;
			}
			else 
			{
				return _controller.isGrounded;
			}
		}
	}

	public Vector3 Velocity
	{
		get
		{
			return previousVelocity;
		}
	}

*/
    public void stun(float duration)
    {
        //needs to freeze animation and controller input here
        stunned = true;
		elapsedStunTime = 0f;
		targetStunTime = duration;
    }

    public void unstun()
    {
        //needs to unfreeze animation and controller input here
        stunned = false;
		elapsedStunTime = 0f;
		targetStunTime = 0f;
    }

    public bool isStunned()
    {
        return stunned;
    }
}
