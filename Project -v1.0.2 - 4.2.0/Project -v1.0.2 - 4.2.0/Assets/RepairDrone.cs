using UnityEngine;
using System.Collections;

public class RepairDrone : MonoBehaviour {


	GameObject target;
	UnitManager targetMan;

	BuildingInteractor buildInter;
	public float repairRate;
	public MultiShotParticle particleEff;
	bool goingHome;
	private RepairTurret myHome;
	public GameObject hometurret;
	float nextActionTime;

	public bool AidConstruction;
	// Use this for initialization
	void Start () {
		myHome = hometurret.GetComponent<RepairTurret> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			if (Vector3.Distance (myHome.transform.position, this.gameObject.transform.position) > 50) {
				returnHome ();
			}

			else if (Vector3.Distance (target.transform.position, this.gameObject.transform.position) > 5) {
				transform.Translate (((target.transform.position + (Vector3.up*4)) - this.gameObject.transform.position).normalized * Time.deltaTime * 23f);
			} else {
				if (Time.time > nextActionTime) {
					nextActionTime = Time.time + 1;
					int amount = (int)Mathf.Min (repairRate, targetMan.myStats.Maxhealth - targetMan.myStats.health);
					particleEff.playEffect ();
					if (buildInter && !buildInter.ConstructDone ()) {
						if (AidConstruction) {
							buildInter.construct (.012f);
							PopUpMaker.CreateGlobalPopUp ("+" , Color.green, target.transform.position);
						}
					
					} else {
						targetMan.myStats.heal (amount);
						if (amount > 0) {
							PopUpMaker.CreateGlobalPopUp ("+" + amount, Color.green, target.transform.position);
						}

					}


				
					if (targetMan.myStats.atFullHealth ()) {
						returnHome ();

					}
				}
			}
		} else if (goingHome) {
			if (Vector3.Distance (myHome.transform.position, this.gameObject.transform.position) > 3) {
				transform.Translate (((myHome.transform.position + (Vector3.up*2)) - this.gameObject.transform.position).normalized * Time.deltaTime * 23f);
			} else {
				
				transform.position = hometurret.transform.position;
				transform.Translate (Vector3.up * 2);
				transform.rotation = hometurret.transform.rotation;
				transform.SetParent (hometurret.transform);
				goingHome = false;
				myHome.doneRepairing ();
			}
		}

	
	}

	public void setTarget(GameObject t)
	{target = t;
		buildInter = t.GetComponent<BuildingInteractor> ();
		targetMan = target.GetComponent<UnitManager> ();
		transform.rotation = Quaternion.identity;


	}


	public void returnHome()
	{
		goingHome = true;
		target = null;
		targetMan = null;
		buildInter = null;
		myHome.doneRepairing ();
	}

}
