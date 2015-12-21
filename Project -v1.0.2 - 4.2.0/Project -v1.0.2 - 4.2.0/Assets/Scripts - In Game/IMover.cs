using UnityEngine;
using System.Collections;

public abstract class IMover: MonoBehaviour {



	public float speed = 0;
	public float acceleration;
	public float MaxSpeed = 10;

	public abstract bool move ();

	public 	abstract void resetMoveLocation (Vector3 location);


}
