using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {
	private GameObject cam;
	public float speed = 5;
	// Use this for initialization
	void Start () {
		cam = MainCamera.main.gameObject;
	
	}
	
	// Update is called once per frame
	Vector3 location;
	void Update () {

		this.transform.Translate (Vector3.up * Time.deltaTime * speed);
	
		location = cam.transform.position;
		location.x = this.gameObject.transform.position.x;
		gameObject.transform.LookAt (location);
	
	}
}
