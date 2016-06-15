using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FogIndicator : MonoBehaviour {

	public Vector3 location;
	private Camera myCam;
	private RectTransform child;
	private RectTransform myCanvas;
	// Use this for initialization
	void Start () {

		myCam = GameObject.FindObjectOfType<MainCamera> ().GetComponent<Camera> ();
		child = transform.FindChild ("Image").GetComponent<RectTransform> ();
		myCanvas = GetComponent<RectTransform> ();

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;		

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {
			location  = hit.point;
		}
		child.gameObject.GetComponent<Image> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		Vector3 temp = myCam.WorldToScreenPoint(location);

		child.transform.position = temp ;


	
	}


}
