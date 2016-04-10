using UnityEngine;
using System.Collections;

public class SateliteScan : TargetAbility{ 

	private RaceManager racer;
	public GameObject prefab;
	// Use this for initialization
	void Start () {
		racer = GameObject.FindObjectOfType<GameManager> ().activePlayer;

	}

	// Update is called once per frame
	void Update () {

	}
	override
	public continueOrder canActivate(){

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
	public  void setAutoCast(){}

	public override bool isValidTarget (GameObject target, Vector3 location){
		if (target == null) {
			return true;
		}

		return (!target.GetComponent<UnitManager>());


	}



	override
	public  bool Cast(GameObject target, Vector3 location)
	{
		myCost.payCost ();


		//	myCost.payCost ();

		GameObject proj = null;

		Vector3 spawnLoc = location;

		location.y += 5;
		proj = (GameObject)Instantiate (prefab, location, Quaternion.identity);
		proj.GetComponent<UnitManager>().setInteractor();
		proj.GetComponent<UnitManager> ().interactor.initialize ();


		return false;

	}



	override
	public void Cast(){



		//myCost.payCost ();


		Vector3 pos = this.gameObject.transform.position;

		Instantiate (prefab, pos, Quaternion.identity);




	}

}
