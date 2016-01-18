using UnityEngine;
using System.Collections;

public static class Orders {

	public static Order CreateStopOrder()
	{
		return new Order("Stop", 0);
	}
	
	public static Order CreateMoveOrder(Vector3 location)
	{
		return new Order("Move", 1, location);
	}
	
	public static Order CreateAttackOrder(GameObject obj)
	{
		return new Order("Attack", 2, obj);
	}
	
	public static Order CreateDeployOrder()
	{
		return new Order("Deploy", 3);
	}

	public static Order CreateAttackMove(Vector3 location )
	{
		return new Order ("AttackMove", 4, location);
	}

	public static Order CreateFollowCommand(GameObject obj)
	{return new Order ("Follow", 5, obj);
	}

	public static Order CreateInteractCommand(GameObject obj)
	{return new Order ("Interact", 6, obj);}



}
