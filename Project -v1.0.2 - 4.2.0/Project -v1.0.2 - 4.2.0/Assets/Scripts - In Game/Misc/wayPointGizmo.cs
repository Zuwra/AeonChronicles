// © 2013 Brett Hewitt All Rights Reserved

using UnityEngine;
using System.Collections;

public class wayPointGizmo : MonoBehaviour {

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, 2.0f);
	}
}
