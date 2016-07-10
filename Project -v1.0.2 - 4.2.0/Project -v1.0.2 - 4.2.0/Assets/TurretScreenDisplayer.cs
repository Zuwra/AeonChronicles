using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretScreenDisplayer : MonoBehaviour {

	private UnitManager manage;

	public List<TurretMount> mounts = new List<TurretMount>();



	public buildTurret A;
	public buildTurret B;
	public buildTurret C;
	public buildTurret D;


	private float nextActionTime;
	// Use this for initialization
	void Start () {
		manage = GetComponent<UnitManager> ();
		nextActionTime = Time.time;
	}



	// Update is called once per frame
	void Update () {

		if (Time.time > nextActionTime) {
			nextActionTime = Time.time + .1f;

			mounts.RemoveAll (item => item == null);
			foreach (TurretMount obj in mounts) {
				
				if (obj.gameObject.GetComponentInParent<TurretPickUp> ()) {

					if (!obj.gameObject.GetComponentInParent<TurretPickUp> ().autocast) {

						continue;
					}
				}
			

					updateButtons (obj.hasDisplayer);

			}

		}

	}

	public void updateButtons(TurretPlacer t)
	{Debug.Log ("Updating buttons");
		
		if (D) {
			t.initialize (A.chargeCount > 0, B.chargeCount > 0, C.chargeCount > 0, D.chargeCount > 0);
		} else if (C) {
			t.initialize (A.chargeCount > 0, B.chargeCount > 0, C.chargeCount > 0, false);
		}
		else {
			t.initialize (A.chargeCount > 0, B.chargeCount > 0, false,false);
		}

		//t.hasDisplayer.initialize (A.chargeCount > 0, B.chargeCount > 0,C.chargeCount > 0,D.chargeCount > 0);
	}








	void OnTriggerEnter(Collider other)
	{
		//need to set up calls to listener components
		//this will need to be refactored for team games

		if (!other.isTrigger) {

			UnitManager manager = other.gameObject.GetComponent<UnitManager> ();
			if (manager) {
				if (manage.PlayerOwner == manager.PlayerOwner) {
		
					foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
						if (mount) {
							
							mounts.Add (mount);
							mount.addShop (this);
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

		if (manage == null || manager == null) {
			return;
		}
			
		if (manage.PlayerOwner == manager.PlayerOwner) {

			foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
				if (mounts.Contains(mount)) {
					mounts.Remove (mount);

					mount.removeShop (this);
				}

				}

			}
		}


	public bool buildGatling(TurretPlacer p )
	{
		if (A.chargeCount > 0) {

			p.myMount.placeTurret (A.createUnit ());
			return true;
		}
		return false;
	}

	public bool buildRailGun(TurretPlacer p )
	{if (B.chargeCount > 0) {
			p.myMount.placeTurret (B.createUnit ());
			return true;
		}
		return false;
	}

	public bool buildMortar(TurretPlacer p )
		{if (C.chargeCount > 0) {
				p.myMount.placeTurret (C.createUnit ());
			return true;
		}
		return false;
	}

	public bool buildRepair(TurretPlacer p )
			{if (D.chargeCount > 0) {
				p.myMount.placeTurret (D.createUnit ());
			return true;
		}
		return false;
	}







}
