using UnityEngine;
using System.Collections;

public class TurretMount : MonoBehaviour {

	public GameObject turret;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void placeTurret(GameObject obj)
	{turret = obj;
		obj.transform.position = this.transform.position;
		obj.transform.parent = this.gameObject.transform;

		UnitManager manager = this.gameObject.GetComponentInParent<UnitManager> ();
		manager.setWeapon(obj.GetComponent<IWeapon> ());



	}
}
