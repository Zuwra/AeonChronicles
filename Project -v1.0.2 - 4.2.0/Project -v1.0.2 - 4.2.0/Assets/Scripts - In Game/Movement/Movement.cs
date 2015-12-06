using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour, IMoveable {
	
	protected RTSObject m_Parent;
	protected Vector3 m_Position = new Vector3();
	
	public abstract Vector3 TargetLocation { get; }

	public float Speed { get; set; }
	public float CurrentSpeed { get; set; }

	public abstract void MoveTo (Vector3 location);

	public abstract void Follow (Transform target);

	public abstract void Stop ();
	
	public abstract void AssignDetails(Item item);
}
