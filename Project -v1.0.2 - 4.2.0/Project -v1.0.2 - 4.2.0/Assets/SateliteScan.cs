﻿using UnityEngine;
using System.Collections;

public class SateliteScan : TargetAbility{ 

	public GameObject prefab;
	// Use this for initialization
	void Start () {

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

		return false;

	}



	override
	public void Cast(){


		Vector3 pos = this.gameObject.transform.position;

		Instantiate (prefab, pos, Quaternion.identity);




	}

}
