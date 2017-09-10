using UnityEngine;
using System.Collections;

public class buildResumeState : UnitState{


	private Vector3 location;


	public BuildStructure myAbility;
	GameObject destination;

	private bool Follow;


	public buildResumeState(GameObject target)
	{
		destination = target;
		location = target.gameObject.transform.position;

	}

	public void cancel()
	{
		myAbility.myCost.refundCost ();
	}

	public override void initialize()
	{
		myManager.cMover.resetMoveLocation (destination.transform.position);

		foreach (Ability ab in myManager.abilityList) {
			if (ab is BuildStructure) {
				if (
					((BuildStructure)ab).unitToBuild.GetComponent<UnitManager> ().UnitName == destination.GetComponent<UnitManager> ().UnitName) {
					myAbility = (BuildStructure)ab;
					break;
				}
			}
		}
	}

	// Update is called once per frame
	override
	public void Update () {

		if (Vector3.Distance (myManager.transform.position, location) > 23) {
			if (myManager.cMover.move ()) {
				Vector3 endSpot = location;
				location.y += 5;
				if (myManager.cMover is airmover) {
					endSpot.y += ((airmover)myManager.cMover).flyerHeight;
				}

				myManager.gameObject.transform.position = endSpot;
				myAbility.resumeBuilding (destination);



			}
		} else {
			Vector3 endSpot = location;
			location.y += 5;
			if (myManager.cMover is airmover) {
				endSpot.y += ((airmover)myManager.cMover).flyerHeight;
			}

			myManager.gameObject.transform.position = endSpot;
			myAbility.resumeBuilding (destination);

		}



	}
	/*
	override
	public void attackResponse(UnitManager src, float amount)
	{
	}
*/
	override
	public void endState()
	{
	}


}
