using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MiningSaw : MonoBehaviour {

	public Animator myAnimator;
	UnitManager myManager;
	public float startTime;


	public List<GameObject> objects;


	public List<float> timers;



	private Vector3 targetLocation;
	private int state = -1;
	private float nextActionTime;
	private bool Turnedon;

	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		//objects = myManager.enemies;
	}
	
	// Update is called once per frame
	void Update () {
		if (Turnedon) {
			if (Time.time > nextActionTime) {
				if (state >= 0 && objects [state] != null) {
					objects [state].SetActive (false);
				}
				state++;
				if (state == objects.Count) {
					state = 0;
				}
				if (objects [state]) {
					objects [state].SetActive (true);
				}
				nextActionTime += timers [state];


				//Debug.Log ("Setting state " + state);
				//myAnimator.SetInteger ("State", state);
			}

		}
	
	}

	public void turnOn()
	{
		Turnedon = true;
		myAnimator.SetInteger ("State", 1);
		nextActionTime = Time.time + startTime;
	}



}
