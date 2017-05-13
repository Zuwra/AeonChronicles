using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelStartPlacer : MonoBehaviour {

	[Tooltip("All the possible places this unit can be put at the beginning")]
	public List<Vector3> startingLocations;
	// Use this for initialization
	void Start () {
	
		if (startingLocations.Count > 0) {
			transform.position = startingLocations [Random.Range (0, startingLocations.Count - 1)];
		}

	}


	void OnDrawGizmos()
	{
		foreach (Vector3 vec in startingLocations) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere (vec, 2);
		}
	}

}
