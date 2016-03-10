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


	
	public Order(string name, int orderType)
	{
		Name = name;
		OrderType = orderType;
	}
	
	public Order(string name, int orderType, Vector3 orderLocation)
	{
		Name = name;
		OrderType = orderType;
		OrderLocation = orderLocation;
	}
	
	public Order(string name, int orderType, GameObject target)
	{
		Name = name;
		OrderType = orderType;
		OrderLocation = target.transform.position;
		Target = target;
	}
}
