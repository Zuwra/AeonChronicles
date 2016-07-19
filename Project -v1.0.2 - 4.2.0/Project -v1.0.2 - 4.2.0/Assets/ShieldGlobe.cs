using UnityEngine;
using System.Collections;

public class ShieldGlobe : MonoBehaviour {


	public GameObject target;
	public float speed;
	public bool isOverCharge;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (target) {
		
			this.gameObject.transform.Translate ((target.gameObject.transform.position - this.gameObject.transform.position).normalized * Time.deltaTime * speed);
			if (Vector3.Distance (this.gameObject.transform.position, target.transform.position) < 3) {
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
