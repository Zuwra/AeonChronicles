using UnityEngine;
using System.Collections;

public class ShieldGlobe : MonoBehaviour {


	public GameObject target;
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (target) {
		
			this.gameObject.transform.Translate ((target.gameObject.transform.position - this.gameObject.transform.position).normalized * Time.deltaTime * speed);
			if(Vector3.Distance(this.gameObject.transform.position, target.transform.position) < 3)
			{target.GetComponent<UnitManager> ().myStats.changeEnergy (5);

				PopUpMaker.CreateGlobalPopUp ("+5", Color.blue, target.transform.position);
				Destroy (this.gameObject);}
		}
		else
		{Destroy(this.gameObject);}
	
	}
}
