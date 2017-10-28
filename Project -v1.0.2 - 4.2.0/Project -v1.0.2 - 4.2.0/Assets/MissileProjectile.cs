using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : Projectile {

	public int numOfCurves;
	public float wildness;


	float nextX;
	float nextY;

	int currentCurve = 0;

	// Use this for initialization
	new void Start () {
		base.Start ();

		nextX = (Random.value -.5f) * wildness;
		nextY = (Random.value -.5f)* wildness;

	}

	public new void OnSpawn()
		{
		base.OnSpawn ();

		currentCurve = 0;
		nextX = (Random.value -.5f) * wildness;
		nextY = (Random.value -.5f)* wildness;


	}

	new void Update()
	{
		if (target != null) {
			lastLocation = target.transform.position + randomOffset;
			//Debug.Log("attacking on " +Vector3.Distance(lastLocation,this.gameObject.transform.position));

		} 

		if (distance - currentDistance < 1.5) {
			Terminate (target);
		} else if (currentDistance / distance > ((float)currentCurve) / ((float)numOfCurves)) {
			if (currentCurve < numOfCurves - 1) {
				currentCurve++;

				nextX = (Random.value - .5f) * wildness;
				nextY = (Random.value - .5f) * wildness;
			} else {
				nextX = 0;
				nextY = 0;
			}
		}
		Vector3 prevPos = transform.position;
		transform.LookAt (lastLocation);
		gameObject.transform.Translate ((Vector3.forward * speed) *40* Time.deltaTime);
		gameObject.transform.Translate (((gameObject.transform.right * nextX) + (gameObject.transform.up * nextY)) * Time.deltaTime);


		// FIX THIS!!!!!!!!!!!!!!!!!!!! My Model is no logner a variable in Projectile
		//myModel.transform.LookAt (prevPos);


		//myModel.transform.LookAt (target.transform.position);
		currentDistance += speed * Time.deltaTime * 40;

	}


}
