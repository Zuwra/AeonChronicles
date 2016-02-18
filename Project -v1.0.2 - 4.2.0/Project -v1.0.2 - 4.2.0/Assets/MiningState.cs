using UnityEngine;
using System.Collections;

public class MiningState : UnitState {

	public float interactionDistance;

	private GameObject target;
	private GameObject dropoff;
	private enum miningState
	{
		traveling, mining, returning
	}

	private float timer;
	private float miningTime;

	private miningState state;

	private float resourceOneAmount;
	private float resourceTwoAmount;


	public MiningState(GameObject unit, UnitManager man, IMover move, IWeapon weapon, float mineTime, float resourceOne, float resourceTwo)
	{
		myManager = man;
		myMover = move;
		myWeapon = weapon;
		miningTime = mineTime;
		resourceOneAmount = resourceOne;
		resourceTwoAmount= resourceTwo;

		target = unit;
		//myMover.resetMoveLocation (target.transform.position);

	

	


	}

	public override void initialize()
	{myMover.resetMoveLocation (target.transform.position);
		dropoff = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.getNearestDropOff (target);
		state = miningState.traveling;
	}

	// Update is called once per frame
	override
	public void Update () {

		if (!target) {

			foreach (GameObject obj in myManager.neutrals) {
				if (obj.GetComponent<OreDispenser> () != null) {
					target = obj;
					myMover.resetMoveLocation (obj.transform.position);
					break;
				}
			}

		}



		switch (state) {
		case miningState.traveling:
			if (myMover.move ()) {
				state = miningState.mining;
				timer = miningTime;
			}
			break;


		case miningState.mining:
			timer -= Time.deltaTime;
			if (timer < 0) {
				dropoff = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.getNearestDropOff (target);
		
				if (dropoff == null) {
					myManager.changeState (new DefaultState ());
				
				} else {
					myMover.resetMoveLocation (dropoff.transform.position);
					state = miningState.returning;
				}
			}
			break;

		case miningState.returning:
			if (myMover.move ()) {
				if (dropoff == null) {
					dropoff = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.getNearestDropOff (target);

					if (dropoff == null) {
						myManager.changeState (new DefaultState ());
					} else {
						myMover.resetMoveLocation (dropoff.transform.position);
						state = miningState.returning;
					}


				} else {
					dropoff.GetComponent<ResourceDropOff> ().dropOff (resourceOneAmount, resourceTwoAmount);
					myMover.resetMoveLocation (target.transform.position);
					state = miningState.traveling;
				}
			}
			break;

		}


		if (!target) 
			{myManager.changeState(new DefaultState());	}

	}


	override
	public void attackResponse(GameObject src)
	{
	}

}
