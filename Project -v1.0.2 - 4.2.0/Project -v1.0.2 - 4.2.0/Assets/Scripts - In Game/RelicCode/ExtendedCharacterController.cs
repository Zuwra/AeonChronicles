using UnityEngine;
using System.Collections;

public class ExtendedControllerColliderHit
{
	private ExtendedCharacterController controller;
	private ControllerColliderHit hit;
	private Vector3 movementBeforeCollision;
	private Vector3 movementAfterCollision;

	public ExtendedControllerColliderHit(ExtendedCharacterController controller, ControllerColliderHit hit, Vector3 movementBeforeCollision, Vector3 movementAfterCollision)
	{
		this.controller = controller;
		this.hit = hit;
		this.movementBeforeCollision = movementBeforeCollision;
		this.movementAfterCollision = movementAfterCollision;
	}

	public ExtendedCharacterController Controller
	{
		get
		{
			return controller;
		}
	}

	public Collider collider
	{
		get
		{
			return hit.collider;
		}
	}

	public GameObject gameObject
	{
		get
		{
			return hit.collider.gameObject;
		}
	}

	public Vector3 point
	{
		get
		{
			return hit.point;
		}
	}

	public Vector3 normal
	{
		get
		{
			return hit.normal;
		}
	}

	public Vector3 MovementBeforeCollision
	{
		get
		{
			return movementBeforeCollision;
		}
	}

	public Vector3 MovementAfterCollision
	{
		get
		{
			return movementAfterCollision;
		}

		set
		{
			movementAfterCollision = value;
		}
	}
}

public class ExtendedCharacterController : MonoBehaviour {

	private CapsuleCollider capsuleCollider;
	private CharacterController controller;

	// Moving platform support private 
	private Transform activePlatform; 
	private Vector3 activeLocalPlatformPoint; 
	private Vector3 activeGlobalPlatformPoint; 
    //private Vector3 lastPlatformVelocity;
	
	// If you want to support moving platform rotation as well: 
	private Quaternion activeLocalPlatformRotation; 
	private Quaternion activeGlobalPlatformRotation;


	public LayerMask collideWith = ~0;
	public Vector3 center = Vector3.zero;
	public float height = 2.0f;
	public float radius = 0.5f;
	public float skinWidth = 0.03f;
	public float minMoveDistance = 0.001f;
	public float maxStepHeight = 0.3f;

	private float centerOffset;

	// Use this for initialization
	void Start ()
	{
		Collider colliderTest = GetComponentInChildren<CapsuleCollider>();

		if (colliderTest == null)
		{
			capsuleCollider = gameObject.AddComponent<CapsuleCollider>();

			if (GetComponent<Rigidbody>() == null)
			{
				Rigidbody body = gameObject.AddComponent<Rigidbody>();
				body.isKinematic = true;
			}
		}

		controller = GetComponent<CharacterController>();

		if (controller == null)
		{
			controller = gameObject.AddComponent<CharacterController>();
		}

		controller.detectCollisions = false;
		controller.stepOffset = maxStepHeight;

		ApplyChanges();
	}

	public void ApplyChanges()
	{
		if (capsuleCollider != null)
		{
			capsuleCollider.height = height;
			capsuleCollider.radius = radius;
			capsuleCollider.center = center;
		}

		controller.height = height;
		controller.radius = radius;
		controller.center = center;

		centerOffset = Mathf.Max(height * 0.5f - radius, 0.0f);
	}

	public void Move(Vector3 moveAmount)
	{
		controller.Move(moveAmount);
	}

	public bool IsGrounded
	{
		get
		{
			return controller.isGrounded;
		}
	}

	public void FixedUpdate()
	{
	//	Debug.Log("A");


		if (activePlatform != null) {
		//	Debug.Log("B");
		//	Vector3 activeGlobalPlatformPoint = transform.position;
		//	activeLocalPlatformPoint = activePlatform.InverseTransformPoint (transform.position);
		//	Debug.Log("transform.position" + transform.position);
		//	Debug.Log("activeLocalPlatformPoint" + activeLocalPlatformPoint);
			Vector3 newGlobalPlatformPoint = activePlatform.TransformPoint(activeLocalPlatformPoint);
		//	Debug.Log("newGlobalPlatformPoint" + newGlobalPlatformPoint);
			Vector3 moveDistance = (newGlobalPlatformPoint - activeGlobalPlatformPoint);
		//	Debug.Log("moveDistance" + moveDistance);
			if (moveDistance != Vector3.zero){
				//if(moveDistance.magnitude<10)
					controller.Move(moveDistance);
		//		Debug.Log(moveDistance);
			//	Debug.Log(activePlatform.transform);
			//	Debug.Log(activeLocalPlatformPoint);
			//	Debug.Log(newGlobalPlatformPoint);
			}

            //lastPlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
			
			// If you want to support moving platform rotation as well:
			Quaternion newGlobalPlatformRotation = activePlatform.rotation * activeLocalPlatformRotation;
			Quaternion rotationDiff = newGlobalPlatformRotation * Quaternion.Inverse(activeGlobalPlatformRotation);
			
			// Prevent rotation of the local up vector
			rotationDiff = Quaternion.FromToRotation(rotationDiff * transform.up, transform.up) * rotationDiff;
			
			transform.rotation = rotationDiff * transform.rotation;
		}
		else {
            //lastPlatformVelocity = Vector3.zero;
			//activeGlobalPlatformPoint = Vector3.zero;
		}

		//
		
		// Actual movement logic here
		//...
		//	collisionFlags = myCharacterController.Move (calculatedMovement);
		//...
			
			// Moving platforms support
		if (activePlatform != null) {
			//Debug.Log("D");
			
			activeGlobalPlatformPoint = transform.position;
			activeLocalPlatformPoint = activePlatform.InverseTransformPoint (transform.position);
			
			// If you want to support moving platform rotation as well:
			activeGlobalPlatformRotation = transform.rotation;
			activeLocalPlatformRotation = Quaternion.Inverse(activePlatform.rotation) * transform.rotation; 
		}
		activePlatform = null;

		}
	void OnCollisionExit(Collision collisionInfo) {
        //print("No longer in contact with " + collisionInfo.transform.name);
	}

	public void OnControllerColliderHit(ControllerColliderHit hit)
	{

		// Make sure we are really standing on a straight platform 
		// Not on the underside of one and not falling down from it either!
		if (hit.moveDirection.y < -0.9 && hit.normal.y > 0.5 && hit.collider.CompareTag("Platform")) { 
						//Debug.Log("AAAA");
						activePlatform = hit.collider.transform;

						//	Debug.Log(hit);
						//	Debug.Log(hit.collider);
						//	Debug.Log(hit.collider.transform.position);
						//	Debug.Log(hit.collider.transform.parent.position);

						if (activePlatform != null) {
								//Debug.Log("D");
				
								activeGlobalPlatformPoint = transform.position;
								activeLocalPlatformPoint = activePlatform.InverseTransformPoint (transform.position);
				
								// If you want to support moving platform rotation as well:
								activeGlobalPlatformRotation = transform.rotation;
								activeLocalPlatformRotation = Quaternion.Inverse (activePlatform.rotation) * transform.rotation; 
						}
			

				} else {
			activePlatform = null;
				}
						

		if (hit.collider.transform.IsChildOf(transform) || hit.collider.transform == transform)
		{
			Physics.IgnoreCollision(hit.collider, controller);
		}

		ExtendedControllerColliderHit controllerHit = new ExtendedControllerColliderHit(this, hit, Vector3.zero, Vector3.zero);
		gameObject.SendMessage("OnExtendedControllerColliderHit", controllerHit, SendMessageOptions.DontRequireReceiver);
		hit.gameObject.SendMessage("OnExtendedControllerHitStay", controllerHit, SendMessageOptions.DontRequireReceiver);
	}

	public void OnDrawGizmosSelected()
	{
		centerOffset = Mathf.Max(height * 0.5f - radius, 0.0f);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.TransformPoint(center + Vector3.up * centerOffset), radius);
		Gizmos.DrawWireSphere(transform.TransformPoint(center + Vector3.down * centerOffset), radius);
	}
}
