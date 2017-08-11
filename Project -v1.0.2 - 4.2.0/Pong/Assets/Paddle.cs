using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {



	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.W)) {
			transform.position = transform.position +  new Vector3(0,250,0) * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.position = transform.position -  new Vector3(0,250,0) * Time.deltaTime;
		}
		
	}
}
