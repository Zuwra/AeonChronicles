using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BonusObjectiverTimer : Objective {


	public float timeBeforeFail;
	private float startTime;
	private bool MyActive;
	private float nextActionTime;

	public bool destroyOnEnd;
	public List<GameObject> targets = new List<GameObject> ();
	string basicDescript;

	// Use this for initialization
	new void Start () {
		base.Start ();
		basicDescript = description;
		base.Start ();
		foreach (GameObject obj in targets) {
			obj.AddComponent<DeathWinTrigger> ();
		}

	}

	// Update is called once per frame
	void Update () {
		if (!MyActive) {
			return;
		}

		if (timeBeforeFail + startTime < Time.time) {
			fail ();
			if (destroyOnEnd) {
				foreach (GameObject go in targets) {
					Destroy (go);
				}
			}
			Destroy (this);
		
		} else if(Time.time > nextActionTime)  {
			nextActionTime += 1;
			description = basicDescript + " (" + Clock.convertToString ((timeBeforeFail + startTime) - Time.time) + ")";
			VictoryTrigger.instance.UpdateObjective (this);

		}

	}

	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		VictoryTrigger.instance.addObjective (this);
		startTime = Time.time;
		nextActionTime = Time.time;
		MyActive = true;
		foreach (GameObject go in targets) {
			foreach (FogOfWarUnit fog in go.GetComponents<FogOfWarUnit>()) {
				fog.enabled = true;
			}

		}
	}


	public void IDied(GameObject obj)
	{Debug.Log ("I died " + obj.name);

		targets.RemoveAll(item => item == null);
		if (targets.Contains (obj)) {
			targets.Remove (obj);

		}
		if (targets.Count == 0) {
			Debug.Log ("Completing");
			complete ();
			Destroy (this);
		}

	}



}
