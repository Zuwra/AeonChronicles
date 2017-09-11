using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetDeathVictory : Objective {

	public List<GameObject> targets = new List<GameObject> ();
	public List<VoiceTrigger> VoiceTriggers;

	[System.Serializable]
	public struct VoiceTrigger{
		public int numDied;
		public int VoiceLine;
		public UnityEngine.Events.UnityEvent triggerMe;
	}

	string initialDescription;
	int totalTargetCount;
	// Use this for initialization
	new void Start () {
		base.Start ();
		//Debug.Log ("Death" + this.gameObject);
		foreach (GameObject obj in targets) {
			obj.AddComponent<DeathWinTrigger> ();
		}
		totalTargetCount = targets.Count;
		initialDescription = description;
		if (targets.Count > 1) {
			description += "  0/" + targets.Count;
		}
	
	}
	


	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){

		startObjective ();
	}

	public void startObjective()
	{
		BeginObjective ();

	//	VictoryTrigger.instance.addObjective (this);

		foreach (GameObject go in targets) {
			if (go) {
				foreach (FogOfWarUnit fog in go.GetComponents<FogOfWarUnit>()) {
					fog.enabled = true;
				}
			}
		}
	}

	public void IDied(GameObject obj)
	{
		targets.RemoveAll(item => item == null);
		if (targets.Contains (obj)) {
			targets.Remove (obj);

		}
		if (totalTargetCount > 1) {
			int targetsKilled = totalTargetCount - targets.Count;

			foreach (VoiceTrigger trig in VoiceTriggers) {

				if (targetsKilled == trig.numDied) {
					if (trig.VoiceLine != -1) {
						dialogManager.instance.playLine (trig.VoiceLine);
					}
					trig.triggerMe.Invoke ();
					break;
				}
			}

			description = initialDescription + "  " +targetsKilled + "/" + totalTargetCount;
			VictoryTrigger.instance.UpdateObjective (this);
		}

		if (targets.Count == 0) {
			complete ();
			Destroy (this);
		}

	}



}
