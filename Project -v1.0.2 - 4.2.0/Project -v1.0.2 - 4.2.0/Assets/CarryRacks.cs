using UnityEngine;
using System.Collections;

public class CarryRacks : Ability{

	private bool researching;
	public float researchTime;
	private float completionTime;
	public GameObject Racks;
	private Selected mySelect;
	public bool disabled;
	// Use this for initialization
	void Start () {
		mySelect = GetComponent<Selected> ();
	}

	// Update is called once per frame
	void Update () {

		if (researching) {
			completionTime -= Time.deltaTime;
			mySelect.updateCoolDown (1 - completionTime/researchTime);
			if ( completionTime < 0) {

				if (Racks) {
					Racks.GetComponent<MeshRenderer> ().enabled = true;
					foreach (MeshRenderer r in Racks.GetComponentsInChildren<MeshRenderer> ()) {
						r.enabled = true;
					}
					foreach (TurretMount m in Racks.GetComponentsInChildren<TurretMount>()) {
						m.enabled = true;
					}

					foreach (CapsuleCollider c in Racks.GetComponentsInChildren<CapsuleCollider>()) {
						c.enabled = true;
					}


				}
				mySelect.updateCoolDown (0);
				GetComponent<UnitManager> ().AbilityStartingRow = 0;
				GetComponent<UnitManager> ().AbilityPriority = 9;
				GetComponent<UnitManager>().UnitName = "Storage Vessel";

				if (mySelect.IsSelected) {
					RaceManager.removeUnitSelect (GetComponent<UnitManager> ());
					RaceManager.AddUnitSelect(GetComponent<UnitManager> ());
					RaceManager.upDateUI ();
				}
				GetComponent<RocketBooster> ().delete ();
				GetComponent<AttachGrinder> ().delete ();
				delete ();
			}
		}
	}


	public void delete()
	{
		GetComponent<UnitManager> ().abilityList.Remove (this);
		Destroy (myCost);
		Destroy (this);
	}



	public override void setAutoCast(bool offOn){
	}





	override
	public continueOrder canActivate(bool showError)
	{continueOrder ord = new continueOrder ();
		ord.nextUnitCast = false;
		if (researching  || disabled) {

			ord.canCast = false;
			ord.nextUnitCast = true;
		}

		if (!myCost.canActivate (this)) {
			
			ord.canCast = false;
			ord.nextUnitCast = true;
		}

		return ord;


	}

	override
	public void Activate()
	{
		if (!canActivate (false).canCast) {
			return;
		}
		researching = true;
		myCost.payCost ();
		completionTime = researchTime;
		GetComponent<AttachGrinder> ().disabled= true;
		GetComponent<RocketBooster> ().disabled= true;

	}


}
