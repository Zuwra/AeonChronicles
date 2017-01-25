using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TuretBuildHelper :buildTurret{


	public SceneEventTrigger helper;

	private bool HasBuilt ;


	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.activated;
	}


	// Update is called once per frame
	void Update ()
	{
		if (buildingUnit) {

			timer -= Time.deltaTime;
			mySelect.updateCoolDown (1 - timer / buildTime);
			if (timer <= 0) {
				HD.stopBuilding ();
				mySelect.updateCoolDown (0);
				buildingUnit = false;
				buildMan.unitFinished (this);
				racer.stopBuildingUnit (this);

				if (!helper.hasTriggered && !HasBuilt) {
					HasBuilt = true;
					helper.trigger (0, 0, Vector3.zero, null, false);
				}
				foreach (Transform obj in this.transform) {

					obj.SendMessage ("DeactivateAnimation", SendMessageOptions.DontRequireReceiver);
				}

				chargeCount++;
				RaceManager.upDateUI ();

			}
		}
		if (autocast) {
	
				if (turretMounts.Count > 0) {
					foreach (TurretMount obj in turretMounts) {
						if (chargeCount == 0) {
							return;
						}


						if (obj.enabled == false) {
							return;
						}

						if (obj.gameObject.GetComponentInParent<TurretPickUp> ()) {

							if (!obj.gameObject.GetComponentInParent<TurretPickUp> ().autocast) {

								return;
							}
						}


						if (obj.turret == null) {
							if (soundEffect) {
								audioSrc.PlayOneShot (soundEffect);
							}
							obj.placeTurret (createUnit ());
						if (PlaceEffect) {
							Instantiate (PlaceEffect, obj.transform.position, Quaternion.identity, obj.transform);
						}

						}


				}
			}


		}
	}
}
