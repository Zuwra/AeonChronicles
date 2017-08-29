using UnityEngine;
using System.Collections;

public class rotater : animate {


	public float speed;
	public bool Yaxis;
	public bool ZAxis;
	Vector3 axis;


	// Use this for initialization
	void Start () {
		if (Yaxis) {
			axis = Vector3.up;
		} else if (ZAxis) {
			axis = Vector3.forward;
		} else {
			axis = Vector3.right;
		}
	
	}
	
	// Update is called once per frame
	void Update () {  
		if(active)
			transform.Rotate (axis, speed * Time.deltaTime);

	
	}

	public void setSpeed(float f)
	{
		speed = f;
	}







}
