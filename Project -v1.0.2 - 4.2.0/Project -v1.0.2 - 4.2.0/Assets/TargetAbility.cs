using UnityEngine;
using System.Collections;

public abstract class TargetAbility : Ability {

	public float range;

	public GameObject target;
	public Vector3 location;


	public bool inRange(Vector3 location)
	{

	
			if(Vector3.Distance(this.gameObject.transform.position, location) < range)
				{return true;}

		return false;

	}

	public abstract void Cast(); 





}
