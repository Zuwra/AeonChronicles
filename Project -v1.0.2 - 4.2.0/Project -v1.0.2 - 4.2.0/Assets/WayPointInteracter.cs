using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointInteracter : StandardInteract {

	public WayPoint previous;
	private WayPoint next;

	void Start() {

		next = previous.myFriends[Random.Range(0, previous.myFriends.Count)];
		myManager.GiveOrder (Orders.CreateMoveOrder(next.transform.position));
	}

	public override UnitState computeState(UnitState s)
	{


		if (s is DefaultState && next) {

			Invoke ("giveOrder", .01f);
		}
		return s;
	}


	void giveOrder()
	{
		WayPoint temp = next;
		next = next.nextPoint (previous);
		previous = temp;
		myManager.GiveOrder (Orders.CreateMoveOrder (next.transform.position));
	}

}
