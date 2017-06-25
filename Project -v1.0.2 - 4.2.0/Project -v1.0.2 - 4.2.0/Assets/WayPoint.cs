using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

	public List<WayPoint> myFriends;

	// Use this for initialization
	void Awake () {
		foreach(WayPoint w in myFriends)
		{
			if (!w.myFriends.Contains (this)) {
				w.myFriends.Add (this);
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		foreach(WayPoint w in myFriends) {
			Gizmos.DrawLine (this.transform.position, w.transform.position);
		}
	}

	public WayPoint nextPoint(WayPoint w) {
		if (myFriends.Count == 1)
			return w;
		int rand = Random.Range (0, myFriends.Count);
		while (myFriends.IndexOf(w) == rand) {
			rand = Random.Range (0, myFriends.Count);
		}
		return myFriends [rand];
	}
}
