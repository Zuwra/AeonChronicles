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

	public UnityEngine.Events.UnityEvent OnTrigger;

	public List<SceneEventTrigger> myTriggers;
	// Use this for initialization
	void Start () {
		
		Invoke ("DelayedUpdate", timeDelay);
	}

	// Update is called once per frame
	void DelayedUpdate () {
		Debug.Log ("Firing");
			StartCoroutine (Fire ());
	}



	IEnumerator Fire ()
	{
		yield return new WaitForSeconds (delay + .0001f);

		foreach (SceneEventTrigger trig in myTriggers) {
			//Debug.Log ("Triggering " + trig);
			OnTrigger.Invoke();
			trig.trigger (index, input, location, target, doIt);
			yield return null;
		}
		Destroy (this);

	}


}
