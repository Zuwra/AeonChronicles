using UnityEngine;
using System.Collections;

public class TurretMountTwo : MonoBehaviour {
	public GameObject turret;

	public TurretplacerTwo hasDisplayer;


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
		obj.transform.rotation = this.gameObject.transform.rotation;

		UnitManager manager = this.gameObject.GetComponentInParent<UnitManager> ();



		if (obj.GetComponent<LaserNodeTurret> () && GetComponentInParent<LaserNode> ()) {
			GetComponentInParent<LaserNode> ().active = true;
		} else {
			GetComponentInParent<LaserNode> ().active = false;
		}



		if (obj.GetComponent<Ballistic> () && GetComponentInParent<BallisticMissile> ()) {
			GetComponentInParent<BallisticMissile> ().active = true;
		} else {
			GetComponentInParent<BallisticMissile> ().active = false;
		}

		manager.PlayerOwner = GetComponentInParent<UnitManager> ().PlayerOwner;


	}


	public void unPlaceTurret()
	{
		turret = null;
		UnitManager manager = this.gameObject.GetComponentInParent<UnitManager> ();
		manager.setWeapon(null);

		if (GetComponentInParent<LaserNode> ()) {
			GetComponentInParent<LaserNode> ().active = false;
		}

		if (GetComponentInParent<BallisticMissile> ()) {
			GetComponentInParent<BallisticMissile> ().active = false;
		}


	}
}