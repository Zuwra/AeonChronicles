﻿using UnityEngine;
using System.Collections;

public class TurretMount : MonoBehaviour {

	public GameObject turret;

	public TurretPlacer hasDisplayer;

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
		if (obj.GetComponent<IWeapon> ()) {
			manager.setWeapon (obj.GetComponent<IWeapon> ());
		} 

		if (obj.GetComponent<RepairTurret> () && GetComponentInParent<repairReturn> ()) {
			GetComponentInParent<repairReturn> ().active = true;
		} else if (GetComponentInParent<repairReturn> ()) {
			GetComponentInParent<repairReturn> ().active = false;
		}
		manager.PlayerOwner = GetComponentInParent<UnitManager> ().PlayerOwner;


	}


	public void unPlaceTurret()
	{
		turret = null;
		UnitManager manager = this.gameObject.GetComponentInParent<UnitManager> ();
		manager.setWeapon(null);

		if (GetComponentInParent<repairReturn> ()) {
			GetComponentInParent<repairReturn> ().active = false;
		}
	



			foreach (TurretMount turr in transform.parent.GetComponentsInChildren<TurretMount> ()) {
			if (turr.turret != null && turr.turret.GetComponent<RepairTurret> () == null) {
				manager.setWeapon (turr.turret.GetComponent<IWeapon> ());
				return;
			} else if (turr.turret != null && turr.turret.GetComponent<RepairTurret> () != null) {
				GetComponentInParent<repairReturn> ().active = true;
			}
			}


	}
}
