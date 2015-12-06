using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipFlight : MonoBehaviour {
	/*
	public bool stealCamera = false;

	public GameObject[] waypointList;

	private GameObject target;
	private int currentIndex =0;
	Quaternion targetRotation;
	private Vector3 destination;
	private bool descending = false;


	private CameraManagerScript camera;

	private float MaxDistance;
	private GameObject player;
	private EffectBase tempEffect;
	private MovementComponent mover;
	public bool IShouldMove;

	public float speed =6;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		mover = gameObject.GetComponent<MovementComponent> ();
		if (waypointList.Length > 0) {
						target = waypointList [0];
				}
		if (target) {
						this.gameObject.transform.LookAt (target.transform.position);
				}

		camera = GameObject.Find ("CameraManager").GetComponent<CameraManagerScript> ();
		if (waypointList.Length > 0) {
						MaxDistance = Vector3.Distance (this.gameObject.transform.position, waypointList [waypointList.Length - 1].transform.position);
				}
		if (stealCamera) {
						
						
						camera.VerticalMoveSpeed += 3;
						
						camera.HorizontalMoveSpeed += 3;
						camera.setCameraDistance (new Vector3 (4, 2, -2));
						
						camera.setObjectToFollow (this.gameObject);
				} else {

			if(this.gameObject.name != "Ghost")
			{camera.setObjectToFollow(player);}
		
		}


	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (target != null) {
						if (IShouldMove) {
								if (stealCamera) {
										player.GetComponent<MovementComponent> ().stun (0.3f);
								}

								float curve = 1 - Mathf.Pow (((MaxDistance - Vector3.Distance (this.gameObject.transform.position, waypointList [waypointList.Length - 1].transform.position)) 
										/ MaxDistance), 5);
					

								destination = (target.transform.position - transform.position).normalized;
								targetRotation = Quaternion.LookRotation (destination); 
			
								if (descending) {
										targetRotation.z = 0; 
										targetRotation.x = 0;
								}
			
								transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime);
			
								if (descending) {
										mover.Move (0, (Vector3.down * Time.deltaTime) * 2 / 3);
										mover.Move (0, (destination) * Time.deltaTime * curve * 6);
				
								} else {
										mover.Move (0, (this.gameObject.transform.forward) * Time.deltaTime * curve * speed);
								}
		


						
						}		
				}
	
	}

	public void OnTriggerEnter(Collider other) {

		if (other.tag.Equals ("ShipWayPoint")) {
						if (other.gameObject == waypointList [currentIndex]) {




								if (waypointList.Length > currentIndex) {
										Destroy (target);
										currentIndex ++;
										target = waypointList [currentIndex];


										if (target.GetComponent<ShipPath> ().state == ShipPath.Movestate.descending) {




												//foreach (ParticleSystem obj in floatingEffect.GetComponentsInChildren<ParticleSystem> ()) {
												//	obj.renderer.enabled = true;
												//}

												descending = true;
			
										} else if (target.GetComponent<ShipPath> ().state == ShipPath.Movestate.stop) {
												target = null;


												//floatingEffect.GetComponent<ParticleSystem> ().renderer.enabled = false;
												if (stealCamera) {

														camera.VerticalMoveSpeed -= 3;

														camera.HorizontalMoveSpeed -= 3;
														GameObject player = GameObject.FindGameObjectWithTag ("Player");
														camera.setObjectToFollow (player);
														Vector3 hi = player.transform.position;
														hi.y += 4;
														player.transform.position = hi;
														camera.setCameraDistance (new Vector3 (-4, -2, 2));
												}
										}
					else{
						if(this.gameObject.GetComponent<AudioSource>() != null)
						{this.gameObject.GetComponent<AudioSource>().Play();}

					}
								}
				
						}
				}

		}

*/

}
