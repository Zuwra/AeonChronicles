﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TimeDelayTrigger : MonoBehaviour {


	public float timeDelay;
	public int player = 1;

	public  int index;
	public  float input;
	public  Vector3 location;
	public  GameObject target; 
	public  bool doIt;
	public float delay;


	public List<SceneEventTrigger> myTriggers;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Clock.main.getTotalSecond () > timeDelay) {
			StartCoroutine (Fire ());
		}
	}



	IEnumerator Fire ()
	{
		yield return new WaitForSeconds (delay + .0001f);

		foreach (SceneEventTrigger trig in myTriggers) {
			//Debug.Log ("Triggering " + trig);
			trig.trigger (index, input, location, target, doIt);
		}
		Destroy (this);

	}


}
