using UnityEngine;
using System.Collections;

public class ResourceCarry : MonoBehaviour {

	public float ResourceOneAmount;
	public float ResourceTwoAmount;

	public bool carryingOne = false;
	public bool carryingTwo= false;

	public enum workerState{ travel, Carry, Stop, Waiting}
	public workerState myState;

	private GameObject nearestDropoff;
	//private GameObject currentWorkSite;
	private RaceManager manager;


	// Use this for initialization
	void Start () {

		manager = GameObject.Find ("GameRaceManager").GetComponent<RaceManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	
	}



	public void loadResource(bool type)//true = One, false = two
	{if (type) {
			carryingOne = true;
		} else {
			carryingTwo = true;}

		nearestDropoff = manager.getNearestDropOff (this.gameObject);
		myState = workerState.Carry;
	this.gameObject.GetComponent<UnitManager> ().cMover.resetMoveLocation(nearestDropoff.transform.position);
	}



	public void dropOffResource()
	{
		manager.updateResources (ResourceOneAmount, ResourceTwoAmount,true);
		carryingOne = false;
		carryingTwo= false;

		myState = workerState.travel;
		//if (currentWorkSite != null) {
			//this.gameObject.GetComponent<MovementComponent> ().queueTargetLocation (currentWorkSite.transform.position);
	//	}

	}



}
