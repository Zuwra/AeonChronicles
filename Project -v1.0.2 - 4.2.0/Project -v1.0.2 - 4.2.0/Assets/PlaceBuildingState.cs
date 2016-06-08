using UnityEngine;
using System.Collections;

public class PlaceBuildingState :UnitState {
	
	private Vector3 location;

	public Ability myAbility;


	private bool Follow;


	public PlaceBuildingState(Vector3 loc, Ability abil)
	{

		location = loc;
		myAbility = abil;

	}

	public override void initialize()
	{
		myManager.cMover.resetMoveLocation (location);
	}

	// Update is called once per frame
	override
	public void Update () {


	


		if (Vector3.Distance(myManager.gameObject.transform.position , location) > 4) {


			myManager.cMover.move ();
		} else {

			myAbility.Activate();

			return;

		}
		//attack


	}

	override
	public void attackResponse(GameObject src)
	{
	}


}
