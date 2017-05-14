using UnityEngine;
using System.Collections;

public class ShieldGlobe : MonoBehaviour {


	public GameObject target;
	public float speed;
	public bool isOverCharge;

	float targetRadius;
	// Use this for initialization
	void Start () {
	
	}


	public void setInfo (GameObject Obj, bool b)
	{target = Obj;
		isOverCharge = b;

		targetRadius = Obj.GetComponent<CharacterController>().radius;
		
	}

	// Update is called once per frame
	void Update () {

		if (target) {
		
			//this.gameObject.transform.Translate ((target.gameObject.transform.position - this.gameObject.transform.position).normalized);




			Vector3 dir = (target.gameObject.transform.position -transform.position);

			//Make sure your the right height above the terrain
			RaycastHit objecthit;
			Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

			if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, (~8))) {
				if (Vector3.Distance (this.gameObject.transform.position, objecthit.point) < 2.5f) {

					dir.y -=   (this.gameObject.transform.position.y -(objecthit.point.y + 2.5f) ) *speed *3f;
				}


			}
			dir.Normalize ();
			dir *= speed * Time.deltaTime;
			this.gameObject.transform.Translate (dir);

			if (Vector3.Distance (this.gameObject.transform.position, target.transform.position) < 3 + targetRadius) {
				if (!isOverCharge) {
					target.GetComponent<UnitManager> ().myStats.changeEnergy (5);

					PopUpMaker.CreateGlobalPopUp ("+5", Color.blue, target.transform.position);
					Destroy (this.gameObject);
				} else {
					StimPack sp = target.GetComponent<StimPack> ();
					int charges = sp.chargeCount;
					if (charges < 3) {
						target.GetComponent<StimPack> ().chargeCount++;
						if (target.GetComponent<Selected> ().IsSelected) {
							RaceManager.upDateUI ();
						}
					}


					PopUpMaker.CreateGlobalPopUp ("+1", Color.yellow, target.transform.position);

				}
				Destroy (this.gameObject);
			}
		}
		else
		{Destroy(this.gameObject);}
	
	}


}
