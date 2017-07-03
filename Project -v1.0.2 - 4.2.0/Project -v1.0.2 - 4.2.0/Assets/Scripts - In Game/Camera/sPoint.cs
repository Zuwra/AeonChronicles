// © 2013 Brett Hewitt All Rights Reserved

using UnityEngine;
using System.Collections;

public class sPoint : MonoBehaviour {


	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, 1.0f);
	}
}
