using UnityEngine;
using System.Collections;

public class SateliteScan : TargetAbility{ 

	public GameObject prefab;

	UltimateApplier myApplier;
	// Use this for initialization
	void Start () {
		myApplier = GetComponent<UltimateApplier> ();
		myType = type.target;
	}


	override
	public continueOrder canActivate(bool showError){

		continueOrder order = new continueOrder ();

		if (!myCost.canActivate (this)) {
			order.canCast = false;
		} else {
			order.nextUnitCast = false;
		}
		return order;
	}

	override
	public void Activate()
	{
	}  // returns whether or not the next unit in the same group should also cast it


	override
	public  void setAutoCast(bool offOn){}

	public override bool isValidTarget (GameObject target, Vector3 location){
		return true;

	}



	override
	public  bool Cast(GameObject target, Vector3 location)
	{
		myCost.payCost ();

		GameObject proj = null;


		location.y += 5;
		proj = (GameObject)Instantiate (prefab, location, Quaternion.identity);
		if (proj.GetComponent<UnitManager> ()) {
			proj.GetComponent<UnitManager> ().setInteractor ();
			proj.GetComponent<UnitManager> ().interactor.initialize ();
		}
		if (myApplier) {
			myApplier.applyUlt (proj, this);
		}

		return false;

	}



	override
	public void Cast(){


		Vector3 pos = this.gameObject.transform.position;

		Instantiate (prefab, pos, Quaternion.identity);




	}

}
