using UnityEngine;
using System.Collections;

public class rotater : animate {


	public float speed;
	public bool Yaxis;

	Vector3 axis;


	// Use this for initialization
	void Start () {
		if (Yaxis) {
			axis = Vector3.up;
		} else {
			axis = Vector3.right;
		}
	
	}
	
	// Update is called once per frame
	void Update () {  
		if(active)
			transform.Rotate (axis, speed * Time.deltaTime);

	
	}






}
