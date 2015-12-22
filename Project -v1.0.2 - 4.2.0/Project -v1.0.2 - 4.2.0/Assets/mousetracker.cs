using UnityEngine;
using System.Collections;

public class mousetracker : MonoBehaviour {



	private float z;


	// Use this for initialization
	void Start () {
		z = this.gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
	

		Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, z);
		this.gameObject.transform.position = pos;
	}
}
