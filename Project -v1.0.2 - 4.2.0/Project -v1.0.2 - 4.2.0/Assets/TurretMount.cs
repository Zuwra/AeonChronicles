using UnityEngine;
using System.Collections;

public class TurretMount : MonoBehaviour {

	public GameObject turret;

	public TurretPlacer hasDisplayer;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (turret && hasDisplayer.gameObject.activeSelf) {
			hasDisplayer.gameObject.SetActive (false);
		} else if (!turret && !hasDisplayer.gameObject.activeSelf) {
			hasDisplayer.gameObject.SetActive (true);
		}

	}

	public void setSelect()
	{
		if (turret) {
			turret.GetComponent<Selected> ().turretDisplay.hover (true);
		}
	}

	public void setDeSelect()
	{
		if (turret) {
			turret.GetComponent<Selected> ().turretDisplay.hover (false);
		}
	}

	public void addShop(TurretScreenDisplayer fact)
	{

		if (hasDisplayer.addFact (fact)) {
		
		}
	

	}


	public void removeShop(TurretScreenDisplayer fact)
	{
		if (hasDisplayer.removeFact (fact)) {
		
		}

		
	}

	public void placeTurret(GameObject obj)
		{turret = obj;
		hasDisplayer.gameObject.SetActive (false);
		Vector3 spot = this.transform.position;
		spot.y += .5f;
		obj.transform.position = spot;
		obj.transform.parent = this.gameObject.transform;
		obj.transform.rotation = this.gameObject.transform.rotation;

		UnitManager manager = this.gameObject.GetComponentInParent<UnitManager> ();
		if (obj.GetComponent<IWeapon> ()) {
			manager.setWeapon (obj.GetComponent<IWeapon> ());
		} 

		if (obj.GetComponent<RepairTurret> () && GetComponentInParent<repairReturn> ()) {
			GetComponentInParent<repairReturn> ().active = true;
			GetComponentInParent<repairReturn> ().placeTurret ();
		} else if (GetComponentInParent<repairReturn> ()) {
			GetComponentInParent<repairReturn> ().active = false;
			GetComponentInParent<repairReturn> ().removeTurret();
		
		}
		manager.PlayerOwner = GetComponentInParent<UnitManager> ().PlayerOwner;


	}


	public void unPlaceTurret()
	{hasDisplayer.gameObject.SetActive (true);
		turret = null;
		UnitManager manager = this.gameObject.GetComponentInParent<UnitManager> ();
		manager.setWeapon(null);

		if (GetComponentInParent<repairReturn> ()) {
			GetComponentInParent<repairReturn> ().active = false;
			GetComponentInParent<repairReturn> ().removeTurret();
		}
	



			foreach (TurretMount turr in transform.parent.GetComponentsInChildren<TurretMount> ()) {
			if (turr.turret != null && turr.turret.GetComponent<RepairTurret> () == null) {
				manager.setWeapon (turr.turret.GetComponent<IWeapon> ());
				//GetComponentInParent<repairReturn> ().removeTurret();

				return;
			} else if (turr.turret != null && turr.turret.GetComponent<RepairTurret> () != null) {
				GetComponentInParent<repairReturn> ().active = true;
				GetComponentInParent<repairReturn> ().placeTurret ();
			}
			}


	}
}
