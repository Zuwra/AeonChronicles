using UnityEngine;

using System.Collections;

public abstract class TargetAbility : Ability {

	public float range;

	public GameObject target;
	public Vector3 location;
	public Texture targetArea;
	public float areaSize;
	public enum targetType{ground, unit}
	public targetType myTargetType;

	public bool inRange(Vector3 location)
	{

		float pyth = Mathf.Pow (this.gameObject.transform.position.x - location.x, 2) + Mathf.Pow (this.gameObject.transform.position.z - location.z, 2);
		if(Mathf.Pow(pyth,.5f) < range)
				{return true;}


		//Debug.Log ("Distance " + Vector3.Distance(this.gameObject.transform.position, location));
		return false;

	}

	public abstract void Cast(); 

	public abstract bool Cast(GameObject target, Vector3 location); 

	public abstract bool isValidTarget (GameObject target, Vector3 location);

	public bool onPathableGround(Vector3 location)
	{//float dist = Vector3.Distance(location, AstarPath.active.graphs [0].GetNearest (location).node.Walkable);
		//Debug.Log ("distance is " + dist);
		return AstarPath.active.graphs [0].GetNearest (location).node.Walkable;// (dist < 5);
	}


}
