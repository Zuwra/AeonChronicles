using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigTurretScreenDisplayer : MonoBehaviour {


	private UnitManager manage;
	public GameObject display;
	private List<TurretMountTwo> mounts = new List<TurretMountTwo>();


	public buildTurret A;
	public buildTurret B;




	// Use this for initialization
	void Start () {
		manage = GetComponent<UnitManager> ();
	}

	// Update is called once per frame
	void Update () {

		foreach (TurretMountTwo obj in mounts) {
			if (obj.turret != null) {

				if (obj.hasDisplayer != null) {
					destroyDisplayer (obj);
				}
				continue;}


			if (obj.gameObject.GetComponentInParent<TurretPickUp> ()) {

				if (!obj.gameObject.GetComponentInParent<TurretPickUp> ().autocast) {

					continue;
				}
			}
			if (obj.hasDisplayer == null) {

				createDisplayer (obj);
			} else {
				updateButtons(obj);
			}
		}

	}



	public void updateButtons(TurretMountTwo t)
	{
		t.hasDisplayer.initialize (A.chargeCount > 0, B.chargeCount > 0);
	}


	public void createDisplayer(TurretMountTwo t)
	{
		GameObject obj = (GameObject)Instantiate (display);
		obj.GetComponent<TurretplacerTwo> ().setUnit (t.gameObject);
		obj.GetComponent<TurretplacerTwo> ().factory = this;
		obj.GetComponent<TurretplacerTwo> ().initialize (A.chargeCount > 0, B.chargeCount > 0);
		t.hasDisplayer = obj.GetComponent<TurretplacerTwo>();
	}




	public void destroyDisplayer(TurretMountTwo t)
	{
		if (t.hasDisplayer != null) {
			Destroy (t.hasDisplayer.gameObject);
		}
	}



	void OnTriggerEnter(Collider other)
	{
		//need to set up calls to listener components
		//this will need to be refactored for team games

		if (!other.isTrigger) {

			UnitManager manager = other.gameObject.GetComponent<UnitManager> ();
			if (manage) {
				if (manage.PlayerOwner == manager.PlayerOwner) {

					foreach (TurretMountTwo mount in other.gameObject.GetComponentsInChildren<TurretMountTwo> ()) {
						if (mount) {

							mounts.Add (mount);
						}

					}
				}
			}
		}
	}





	void OnTriggerExit(Collider other)
	{

		//need to set up calls to listener components
		//this will need to be refactored for team games
		if (other.isTrigger) {
			return;}

		UnitManager manager = other.gameObject.GetComponent<UnitManager>();

		if (manage == null) {
			return;
		}

		if (manage.PlayerOwner == manager.PlayerOwner) {

			foreach (TurretMountTwo mount in other.gameObject.GetComponentsInChildren<TurretMountTwo> ()) {
				if (mounts.Contains(mount)) {
					mounts.Remove (mount);
					destroyDisplayer (mount);
				}

			}

		}
	}


	public void buildLaser(TurretplacerTwo p )
	{
		p.unit.GetComponent<TurretMountTwo>().placeTurret (A.createUnit ());
	}

	public void buildMissile(TurretplacerTwo p )
	{
		p.unit.GetComponent<TurretMountTwo>().placeTurret (B.createUnit ());
	}



}
