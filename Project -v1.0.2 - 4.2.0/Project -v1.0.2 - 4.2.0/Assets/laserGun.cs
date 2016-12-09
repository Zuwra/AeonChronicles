using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class laserGun : MonoBehaviour {



	Camera myCam;
	// Use this for initialization
	void Start () {
		myCam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetMouseButtonDown (0)) {

			if (!EventSystem.current.IsPointerOverGameObject ()) 
			{

			//	Debug.Log ("Not over UI ");
				//lineRender.enabled = true;

				Ray rayb = myCam.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitb;

				if (Physics.Raycast (rayb, out hitb, Mathf.Infinity, ~(1 << 16))) {
					//Debug.Log ("hit a  " + hitb.collider.gameObject);
					if (hitb.collider.gameObject.GetComponent<blowUp> ()) {
						hitb.collider.gameObject.GetComponent<blowUp> ().blowUpTrigger ();
					}

					if (hitb.collider.gameObject.GetComponent<MenuAnimationPlayer>()) {
		
						hitb.collider.gameObject.GetComponent<MenuAnimationPlayer> ().ClickOn ();
					}

				}


			
			}
		} 
	
	}
}
