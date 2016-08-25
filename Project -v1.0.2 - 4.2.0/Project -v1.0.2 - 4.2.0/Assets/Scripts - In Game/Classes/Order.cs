using UnityEngine;
using System.Collections;

public class Order {

	public string Name
	{
		get;
		private set;
	}
	
	public int OrderType
	{
		get;
		private set;
	}
	
	public Vector3 OrderLocation
	{
		get;
		private set;
	}
	
	public GameObject Target
	{
		get;
	set;
	}

	public bool queued
	{
		get;
		set;
	}


	
	public Order(string name, int orderType)
	{
		Name = name;
		OrderType = orderType;
		queued = false;
	}
	
	public Order(string name, int orderType, Vector3 orderLocation)
	{
		Name = name;
		OrderType = orderType;
		OrderLocation = orderLocation;
		queued = false;
	}
	
	public Order(string name, int orderType, GameObject target)
	{
		Name = name;
		OrderType = orderType;
		OrderLocation = target.transform.position;
		Target = target;
		queued = false;
	}






	public Order(string name, int orderType, bool queue)
	{
		Name = name;
		OrderType = orderType;
		queued = queue;
	}

	public Order(string name, int orderType, Vector3 orderLocation, bool queue)
	{
		Name = name;
		OrderType = orderType;
		OrderLocation = orderLocation;
		queued = queue;
	}

	public Order(string name, int orderType, GameObject target, bool queue)
	{
		Name = name;
		OrderType = orderType;
		OrderLocation = target.transform.position;
		Target = target;
		queued = queue;
	}


}
