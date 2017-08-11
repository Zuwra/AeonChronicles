using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	

	Vector3 movement = new Vector3(300,300,0);

	// Update is called once per frame
	void Update () {

		transform.position += movement * Time.deltaTime;
		if (transform.position.x < 0) {
			movement.x *= (-1);
		}

		if (transform.position.x > Screen.width) {
			movement.x *= (-1);
		}

		if (transform.position.y > Screen.height) {
			movement.y *= (-1);
		}

		if (transform.position.y < 0) {
			movement.y *= (-1);
		}
	}
}
