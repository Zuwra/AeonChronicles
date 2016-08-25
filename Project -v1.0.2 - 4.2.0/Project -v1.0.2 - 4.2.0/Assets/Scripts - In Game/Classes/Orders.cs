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


	public static Order CreateHoldGroundOrder()
	{
		return new Order("Hold", 7);
	}


	public static Order CreatePatrol(Vector3 location )
	{
		return new Order ("Patrol", 8, location);
	}







	public static Order CreateStopOrder(bool queue)
	{
		return new Order("Stop", 0,queue);
	}

	public static Order CreateMoveOrder(Vector3 location,bool queue)
	{
		return new Order("Move", 1, location,queue);
	}

	public static Order CreateAttackOrder(GameObject obj,bool queue)
	{
		return new Order("Attack", 2, obj,queue);
	}

	public static Order CreateDeployOrder(bool queue)
	{
		return new Order("Deploy", 3,queue);
	}

	public static Order CreateAttackMove(Vector3 location ,bool queue)
	{
		return new Order ("AttackMove", 4, location,queue);
	}


	public static Order CreateFollowCommand(GameObject obj,bool queue)
	{return new Order ("Follow", 5, obj,queue);
	}

	public static Order CreateInteractCommand(GameObject obj,bool queue)
	{return new Order ("Interact", 6, obj,queue);}


	public static Order CreateHoldGroundOrder(bool queue)
	{
		return new Order("Hold", 7,queue);
	}


	public static Order CreatePatrol(Vector3 location ,bool queue)
	{
		return new Order ("Patrol", 8, location,queue);
	}


}
