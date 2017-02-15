using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Objective : SceneEventTrigger {
	[TextArea(2,10)]
	public string description;
	public int reward;
	public bool ActiveOnStart;
	public bool bonus;
	public bool completed;
	public Objective nextObjective;
	public bool UltimateObjective;

	public List<SceneEventTrigger> myEvents = new List<SceneEventTrigger>();
	// Use this for initialization
	public void Start () {
		if (ActiveOnStart) {
			VictoryTrigger.instance.addObjective (this);
		}
	
	}
	

	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		VictoryTrigger.instance.addObjective (this);
	}

	public void complete()
	{
		completed = true;

		foreach (SceneEventTrigger trig in myEvents) {
			trig.trigger (0, 0, Vector3.zero, null, false);
		}

		if(nextObjective){
			VictoryTrigger.instance.addObjective (nextObjective);}
		VictoryTrigger.instance.CompleteObject (this);
	}

	public void unComplete()
	{completed = false;
		Debug.Log ("uncompelted");
		VictoryTrigger.instance.unComplete (this);
	}

	public void fail()
	{
		VictoryTrigger.instance.FailObjective (this);

	}



}
