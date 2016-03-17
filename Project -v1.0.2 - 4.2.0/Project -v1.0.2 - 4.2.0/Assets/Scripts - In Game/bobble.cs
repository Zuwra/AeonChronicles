using UnityEngine;
using System.Collections;

public class bobble : animate {

	public float amplitude;          //Set in Inspector 
	public float speed;                  //Set in Inspector 
	private float tempVal;
	private Vector3 tempPos;

	public bool x;
	public bool z;







	// Use this for initialization
	void Start () {
		tempPos = this.gameObject.transform.position;
		if (z) {
			tempVal = transform.position.z;
		} else if (x) {
			tempVal = transform.position.x;
		} else {
			tempVal = transform.position.y;
		}

	}

	// Update is called once per frame
	void Update () {
		if (active) {
			if (z) {
				tempPos.z = tempVal + amplitude * Mathf.Sin (speed * Time.time);
			
			} else if (x) {
				tempPos.x = tempVal + amplitude * Mathf.Sin (speed * Time.time);


			} else {
				tempPos.y = tempVal + amplitude * Mathf.Sin (speed * Time.time);
			
			}
			transform.position = tempPos;
		}
	}






		


}
