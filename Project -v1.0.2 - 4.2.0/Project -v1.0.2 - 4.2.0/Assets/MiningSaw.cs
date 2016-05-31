using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MiningSaw : MonoBehaviour {

	public Animator myAnimator;
	UnitManager myManager;
	int inAction; //0 = idle, 1 = rotating, 2 = attacking
	List<GameObject> objects;

	public GameObject chopDecal;
	public GameObject sliceDecal;
	public GameObject arm;
	public float speed;

	private Vector3 targetLocation;
	private int state = 0;
	private float nextActionTime;

	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		objects = myManager.enemies;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextActionTime) {
			nextActionTime += 3f;

			state++;
			if (state > 5) {
				state = 1;}
			Debug.Log ("Setting state " + state);
			myAnimator.SetInteger ("State", state);
		}


		/*
		if (inAction == 0) {
			if (objects.Count > 0) {
				GameObject target = objects [Random.Range (0, objects.Count)];
				targetLocation = target.transform.position;
				chopDecal.transform.position = targetLocation;
				targetLocation.y = arm.transform.position.y;

				inAction = 1;
			}
		} else if (inAction == 1) {
			
			Debug.Log (targetLocation + "   " + arm.transform.position);
			Vector3 angle = targetLocation - arm.transform.position;

			arm.transform.Rotate (Vector3.up, -30 * Time.deltaTime);
			Debug.Log (Vector3.Angle (angle, arm.transform.forward));
			if (Vector3.Angle (angle, arm.transform.forward) - 100 < 5 && Vector3.Angle (angle, arm.transform.forward) - 100 > 0) {
				inAction = 2;
				Debug.Log ("At Target");
			}
		
		
		} else {
		
		}
			*/
	
	}





}
