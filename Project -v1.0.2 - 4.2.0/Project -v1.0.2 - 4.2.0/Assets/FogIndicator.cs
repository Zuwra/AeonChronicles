using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FogIndicator : MonoBehaviour {

	public Vector3 location;
	private Camera myCam;
	private RectTransform child;

	// Use this for initialization
	void Start () {

		myCam = MainCamera.main.GetComponent<Camera> ();
		child = transform.Find ("Image").GetComponent<RectTransform> ();
	

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;		

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {
			location  = hit.point;
		}
		child.gameObject.GetComponent<Image> ().enabled = true;
		child.transform.position = myCam.WorldToScreenPoint(location) ;
	}

	// Update is called once per frame
	void Update () {

		child.transform.position = myCam.WorldToScreenPoint(location) ;

	}


}
