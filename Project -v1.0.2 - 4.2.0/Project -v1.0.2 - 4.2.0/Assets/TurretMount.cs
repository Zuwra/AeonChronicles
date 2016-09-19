using UnityEngine;
using System.Collections;

public class TurretMount : MonoBehaviour {

	public GameObject turret;

	public TurretPlacer hasDisplayer;

	public bool rapidArms;
	// Use this for initialization
	void Start () {
	
		FButtonManager.main.updateTankNumber ();
		if (rapidArms) {
		
			foreach (buildTurret bt in GameObject.FindObjectsOfType<buildTurret>()) {
				
				bt.addMount (this);
			}
		

			foreach (TurretScreenDisplayer tm in GameObject.FindObjectsOfType<TurretScreenDisplayer>()) {
				if (!tm.mounts.Contains (this)) {
					tm.mounts.Add (this);
					addShop (tm);
				
				}

			}
		}


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
			
			if (turret.GetComponent<Selected> ().turretDisplay) {
				turret.GetComponent<Selected> ().turretDisplay.hover (true);
			}
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
		{
		
		turret = obj;
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
		FButtonManager.main.updateTankNumber ();

	}


	public GameObject unPlaceTurret()
	{hasDisplayer.gameObject.SetActive (true);
		GameObject toReturn = turret;
		turret = null;
		UnitManager manager = this.gameObject.GetComponentInParent<UnitManager> ();

		manager.removeWeapon(GetComponent<IWeapon>());

		if (GetComponentInParent<repairReturn> ()) {
			GetComponentInParent<repairReturn> ().active = false;
			GetComponentInParent<repairReturn> ().removeTurret();
		}
	



			foreach (TurretMount turr in transform.parent.GetComponentsInChildren<TurretMount> ()) {
			if (turr.turret != null && turr.turret.GetComponent<RepairTurret> () == null) {
				manager.setWeapon (turr.turret.GetComponent<IWeapon> ());
				turret = turr.gameObject;
				//GetComponentInParent<repairReturn> ().removeTurret();

				return turr.turret;
			} else if (turr.turret != null && turr.turret.GetComponent<RepairTurret> () != null) {
				GetComponentInParent<repairReturn> ().active = true;
				GetComponentInParent<repairReturn> ().placeTurret ();
				turret = turr.gameObject;
			}
			}

		FButtonManager.main.updateTankNumber ();
		Debug.Log ("Deatched");
		return toReturn;
	}
}
