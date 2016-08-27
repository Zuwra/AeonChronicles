using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetDeathVictory : Objective {

	public List<GameObject> targets = new List<GameObject> ();


	// Use this for initialization
	void Start () {
		base.Start ();
		foreach (GameObject obj in targets) {
			obj.AddComponent<DeathWinTrigger> ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void IDied(GameObject obj)
	{Debug.Log ("I died " + obj.name);
		
		targets.RemoveAll(item => item == null);
		if (targets.Contains (obj)) {
			targets.Remove (obj);

		}
		if (targets.Count == 0) {
			complete ();
			Destroy (this);
		}

	}



}
