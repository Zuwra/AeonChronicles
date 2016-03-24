using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {
	private GameObject cam;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Translate (Vector3.up * Time.deltaTime * 5);
		Vector3 tempscale = this.transform.lossyScale;
		tempscale +=( Vector3.one * .3f * Time.deltaTime);
		this.transform.localScale = tempscale;

		Vector3 location = cam.transform.position;
		location.x = this.gameObject.transform.position.x;
		gameObject.transform.LookAt (location);
	
	}
}
