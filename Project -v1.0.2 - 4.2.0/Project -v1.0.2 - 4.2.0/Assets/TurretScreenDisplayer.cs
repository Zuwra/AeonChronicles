using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretScreenDisplayer : MonoBehaviour {

	private UnitManager manage;
	public GameObject display;
	private List<TurretMount> mounts = new List<TurretMount>();


	public buildTurret A;
	public buildTurret B;
	public buildTurret C;
	public buildTurret D;



	// Use this for initialization
	void Start () {
		manage = GetComponent<UnitManager> ();
	}

	// Update is called once per frame
	void Update () {
		mounts.RemoveAll (item => item == null);
				foreach (TurretMount obj in mounts) {
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

				//Debug.Log ("Updating buttons");
				updateButtons(obj);
			}
				}

	}



	public void updateButtons(TurretMount t)
	{
		t.hasDisplayer.initialize (A.chargeCount > 0, B.chargeCount > 0,C.chargeCount > 0,D.chargeCount > 0);
	}


	public void createDisplayer(TurretMount t)
{
		GameObject obj = (GameObject)Instantiate (display);
		obj.GetComponent<TurretPlacer> ().setUnit (t.gameObject);
		obj.GetComponent<TurretPlacer> ().factory = this;
		obj.GetComponent<TurretPlacer> ().initialize (A.chargeCount > 0, B.chargeCount > 0,C.chargeCount > 0,D.chargeCount > 0);
		t.hasDisplayer = obj.GetComponent<TurretPlacer>();
}




	public void destroyDisplayer(TurretMount t)
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
		
					foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
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

			foreach (TurretMount mount in other.gameObject.GetComponentsInChildren<TurretMount> ()) {
				if (mounts.Contains(mount)) {
					mounts.Remove (mount);
					destroyDisplayer (mount);
				}

				}

			}
		}


	public void buildGatling(TurretPlacer p )
	{
		p.unit.GetComponent<TurretMount>().placeTurret (A.createUnit ());
	}

	public void buildRailGun(TurretPlacer p )
	{
		p.unit.GetComponent<TurretMount>().placeTurret (B.createUnit ());
	}

	public void buildMortar(TurretPlacer p )
	{
		p.unit.GetComponent<TurretMount>().placeTurret (C.createUnit ());
	}

	public void buildRepair(TurretPlacer p )
	{
		p.unit.GetComponent<TurretMount>().placeTurret (D.createUnit ());
	}







}
