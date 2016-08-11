using UnityEngine;
using System.Collections;

public class RocketBooster : Ability{

	private bool researching;
	public float researchTime;
	private float completionTime;
	public GameObject rocket;
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
				if(rocket)
				rocket.GetComponent<MeshRenderer> ().enabled = true;


				GetComponent<UnitManager> ().cMover.changeSpeed (.75f,0,true, this);
				GetComponent<UnitManager> ().cMover.acceleration += 4;
				GetComponent<UnitManager> ().AbilityStartingRow = 2;
				GetComponent<UnitManager> ().AbilityPriority = 11;

				GetComponent<AttachGrinder> ().delete ();
				GetComponent<CarryRacks> ().delete ();
				GetComponent<UnitManager>().UnitName = "Speeder Vessel";
				if (mySelect.IsSelected) {
					RaceManager.removeUnitSelect (GetComponent<UnitManager> ());
					RaceManager.AddUnitSelect(GetComponent<UnitManager> ());
					RaceManager.upDateUI ();
				}

				delete ();
			}
		}
	}


	public void disableButtons()
	{
		
		active = false;
		if (mySelect.IsSelected) {
		
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
		if (!canActivate (true).canCast) {
			return;
		}
	
		researching = true;
		myCost.payCost ();
		completionTime = researchTime;
		GetComponent<AttachGrinder> ().disabled= true;
		GetComponent<CarryRacks> ().disabled= true;

	}

}
