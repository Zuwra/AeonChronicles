using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

	public Vector3 ToMoveTo;


	// Use this for initialization
	void Start () {

		RaycastHit hit;

		if (Physics.Raycast (ToMoveTo + transform.position, Vector3.down, out hit, 1000, 1 << 8)) {
			{
				Vector3 attackMovePoint = hit.point;

				GetComponent<UnitManager> ().GiveOrder (Orders.CreatePatrol (attackMovePoint, false));
			}
		}
			Destroy (this);

	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere (ToMoveTo +transform.position, 1);
	}

}
