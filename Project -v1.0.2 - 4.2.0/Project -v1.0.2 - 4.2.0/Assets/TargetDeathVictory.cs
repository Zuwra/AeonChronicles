using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetDeathVictory : MonoBehaviour {

	public List<GameObject> targets = new List<GameObject> ();

	VictoryTrigger myVictory;


	// Use this for initialization
	void Start () {
		myVictory = GameObject.FindObjectOfType<VictoryTrigger> ();

		foreach (GameObject obj in targets) {
			obj.AddComponent<DeathWinTrigger> ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void IDied(GameObject obj)
	{
		if (targets.Contains (obj)) {
			targets.Remove (obj);
			if (targets.Count == 0) {
				myVictory.Win ();
			}
		}

	}



}
