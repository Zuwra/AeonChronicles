using UnityEngine;
using System.Collections;

public class AttachGrinder : Ability{

	private bool researching;
	public float researchTime;
	private float completionTime;
	public GameObject grinder;
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

				mySelect.updateCoolDown (0);

				grinder.GetComponent<MeshRenderer> ().enabled = true;

				GetComponent<UnitManager>().UnitName = "Grinder Vessel";
				GetComponent<UnitManager> ().AbilityStartingRow = 1;
				GetComponent<UnitManager> ().AbilityPriority = 10;



			
				grinder.GetComponent<Grinder> ().setOwner (GetComponent<UnitManager>().PlayerOwner);
				grinder.GetComponent<Grinder> ().turnOn ();

				GetComponent<CarryRacks> ().delete ();
				GetComponent<RocketBooster> ().delete ();
				if (mySelect.IsSelected) {
					RaceManager.upDateUI ();
				}
				if (mySelect.IsSelected) {
					RaceManager.removeUnitSelect (GetComponent<UnitManager> ());
					RaceManager.AddUnitSelect(GetComponent<UnitManager> ());
					RaceManager.upDateUI ();
				}

				delete ();
			}
		}
	}



	public override void setAutoCast(bool offOn){
	}


	public void delete()
	{
		GetComponent<UnitManager> ().abilityList.Remove (this);
		Destroy (myCost);
		Destroy (this);
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
		if (!canActivate (true).canCast) {
			return;
		}
		researching = true;
		myCost.payCost ();
		completionTime = researchTime;
		GetComponent<RocketBooster> ().disabled= true;
		GetComponent<CarryRacks> ().disabled= true;

	}

}
