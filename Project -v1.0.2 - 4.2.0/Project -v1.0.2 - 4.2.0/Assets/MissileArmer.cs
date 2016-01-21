using UnityEngine;
using System.Collections;

public class MissileArmer : MonoBehaviour {


	public UnitManager manager;

	public bool missiles;
	public bool nitro;
	public bool repairs;
	// Use this for initialization
	void Start () {
		manager = GetComponent<UnitManager> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}




	void OnTriggerEnter(Collider other)
	{
		//need to set up calls to listener components
		//this will need to be refactored for team games
		if (other.isTrigger) {
			return;}


		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

		if (manage == null) {
			return;
		}

		if (manage.PlayerOwner == manager.PlayerOwner) {

			if (missiles) {
				missileSalvo salvo = other.gameObject.GetComponent<missileSalvo> ();
				if (salvo) {
					salvo.chargeCount = salvo.maxRockets;
				}
			}	

			if (repairs) {
				RepairTurret repair = other.gameObject.GetComponent<RepairTurret> ();

				if (repair) {
					repair.chargeCount = repair.maxRepair;
				}
			}

			if (nitro) {
				StimPack stim = other.gameObject.GetComponent<StimPack> ();

				if (stim) {
					stim.chargeCount = 3;
				}

			}
			RaceManager.upDateUI ();
			}



	}



}
