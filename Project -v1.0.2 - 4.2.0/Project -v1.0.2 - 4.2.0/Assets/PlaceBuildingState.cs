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
		myAbility.myCost.payCost ();

	}

	public void cancel()
	{
		myAbility.myCost.refundCost ();
	}

	public override void initialize()
	{
		myManager.cMover.resetMoveLocation (location);
	}

	// Update is called once per frame
	override
	public void Update () {


			if (myManager.cMover.move ()) {
			Vector3 endSpot = location;
			location.y += 5;
			if (myManager.cMover is airmover) {
				endSpot.y += ((airmover)myManager.cMover).flyerHeight;
			}

			myManager.gameObject.transform.position = endSpot;


			//	Debug.Log ("Activating");

			if (myAbility is BuildStructure) {
				((BuildStructure)myAbility).setBuildSpot (location);
			}
				myAbility.Activate ();

	
			}



	}

	override
	public void attackResponse(GameObject src)
	{
	}

	override
	public void endState()
	{
	}


}
