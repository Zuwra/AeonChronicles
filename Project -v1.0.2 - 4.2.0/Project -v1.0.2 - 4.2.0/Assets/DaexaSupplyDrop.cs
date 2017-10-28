using UnityEngine;
using System.Collections;

public class DaexaSupplyDrop: TargetAbility{

	private RaceManager racer;
	public GameObject prefab;
	UltimateApplier myApplier;
	// Use this for initialization
	void Start () {
		racer = GameObject.FindObjectOfType<GameManager> ().activePlayer;
		myApplier = GetComponent<UltimateApplier> ();
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


		//	myCost.payCost ();

		GameObject proj = null;

		Vector3 spawnLoc = location;
		spawnLoc.y += 150;
		location.y += 5;
		proj = (GameObject)Instantiate (prefab, spawnLoc, Quaternion.identity);

		UnitManager tempMan = proj.GetComponent<UnitManager> ();
		tempMan.setInteractor ();
		tempMan.interactor.initialize ();
		racer.applyUpgrade (tempMan);

		if (proj.GetComponent<FogOfWarUnit> ()) {
			proj.GetComponent<FogOfWarUnit> ().AutoUpdate ();
		}

		proj.GetComponent<SpaceDrop> ().setLocation (location);
		if (myApplier) {
			myApplier.applyUlt (proj, this);
		}
		//GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitCreated (-15);

		return false;

	}



	override
	public void Cast(){



		//myCost.payCost ();


		Vector3 pos = this.gameObject.transform.position;

		Instantiate (prefab, pos, Quaternion.identity);




	}


}