using UnityEngine;
using System.Collections;

public class bobble : animate {

	public float amplitude;          //Set in Inspector 
	public float speed;                  //Set in Inspector 
	private float tempVal;
	private Vector3 tempPos;
	
	void Start () 
	{tempPos = this.gameObject.transform.position;
		tempVal = transform.position.y;
	}
	
	void Update () 
	{        if (active) {
			tempPos.y = tempVal + amplitude * Mathf.Sin (speed * Time.time);
			transform.position = tempPos;
		}
	}



		


}
