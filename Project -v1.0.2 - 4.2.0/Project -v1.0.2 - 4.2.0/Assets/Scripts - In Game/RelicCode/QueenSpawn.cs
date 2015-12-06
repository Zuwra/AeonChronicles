using UnityEngine;
using System.Collections;

public class QueenSpawn : MonoBehaviour {
	/*

	public bool leaveWater = false;
	public bool attackAll = false;

	public GameObject brood;
	private VisionArc outerVision;
	private VisionArc attackVision;
	public MovementComponent mover;
	public float retreatSpeed = 5;
	public float attackSpeed = 5;
	private bool enraged = false;
	private float maxAttackDelay = 1;  // change this to change how fast they spawn
	private float attackDelayTimer = 0;
	private GameObject attackTarget;

	public bool stay;

	// Use this for initialization
	void Start () {

		outerVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		
		attackVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant3, gameObject);
		attackDelayTimer = maxAttackDelay;
		maxAttackDelay = attackSpeed; 

		mover = gameObject.GetComponent<MovementComponent>();
	}
	
	// Update is called once per frame
	void Update () {
				if (enraged && attackDelayTimer >= maxAttackDelay && !stay) {
						KillTarget ();
						return;
				} else {
						attackDelayTimer += Time.deltaTime;
						enraged = false;
						
				}
				foreach (GameObject obj in attackVision.PlayersInVision()) {
						if (obj.GetComponent<HealthComponent> () != null) {
								attackTarget = obj;
								enraged = true;
								return;
						}
				}
				ArrayList evasiveManeuvers = new ArrayList (); // a list of vectors that point in the opposite direction of other characters
		foreach(GameObject obj in outerVision.ObjectsInVision()) {
			if(obj.GetComponent<PlayerBehavior>() != null || obj.GetComponent<HealthComponent>() != null) {
				evasiveManeuvers.Add(InvertDistanceIntensity(gameObject.transform.position - obj.transform.position));
			}
		}

				foreach (GameObject obj in outerVision.PlayersInVision()) {
						if (obj.GetComponent<PlayerBehavior> () != null || obj.GetComponent<HealthComponent> () != null) {
								if (attackDelayTimer >= maxAttackDelay) {
						
										Vector3 spawnLoc = new Vector3 (this.transform.position.x, this.transform.position.y +1, this.transform.position.z);
										GameObject look = (GameObject)Instantiate (brood, spawnLoc, Quaternion.identity);
										look.transform.LookAt(obj.transform.position);
										
										
										attackDelayTimer = 0;

								}
						}
				}

		Vector3 finalVec = new Vector3();
		foreach(Vector3 vec in evasiveManeuvers) {
			finalVec += vec;
		}
		if (evasiveManeuvers.Count > 0 && !stay) {
				mover.Move (0, (finalVec).normalized * Time.deltaTime * retreatSpeed);

				} 

	}

	
	// Returns a higher vector for closer characters within the outerVision 
	// Returns a lower vector for further characters within the outerVision
	Vector3 InvertDistanceIntensity(Vector3 inputVec) {
		Vector3 outVec = new Vector3();
		outVec.x = (outerVision.Distance - Mathf.Abs(inputVec.x)) / outerVision.Distance;
		outVec.y = 0f; // not dealing with up/down right now.
		outVec.z = (outerVision.Distance - Mathf.Abs(inputVec.z)) / outerVision.Distance;
		outVec.x *= (inputVec.x < 0) ? - 1.0f : 1.0f;
		outVec.z *= (inputVec.z < 0) ? - 1.0f : 1.0f;
		return outVec;
	}
	
	void KillTarget() {
		if(!attackTarget) return; // if the attackTarget object is removed, don't attempt to kill it anymore
		mover.Move(0, (attackTarget.transform.position - gameObject.transform.position).normalized * Time.deltaTime * attackSpeed);
		GameObject[] targets = attackVision.ObjectsInVision();
		foreach(GameObject targ in targets) {
			if(targ == attackTarget) {
				AttackBase.GetAttackByVariant(AttackEnum.Default, gameObject).Attack(attackTarget.transform);
				enraged = false;
				attackDelayTimer = 0;
			}
		}
	}
	*/
}
