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

	public UnityEngine.Events.UnityEvent OnStart;
	public UnityEngine.Events.UnityEvent OnComplete;

	public List<SceneEventTrigger> myEvents = new List<SceneEventTrigger>();
	// Use this for initialization
	public void Start () {
		if (ActiveOnStart) {
			VictoryTrigger.instance.addObjective (this);
		}
	
	}

	public virtual void BeginObjective()
	{
		VictoryTrigger.instance.addObjective (this);
		OnStart.Invoke ();
	}
	

	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		BeginObjective ();
	}

	public void complete()
	{
		completed = true;

		foreach (SceneEventTrigger trig in myEvents) {
			if (trig) {
				trig.trigger (0, 0, Vector3.zero, null, false);
			}
		}

		if(nextObjective){
			nextObjective.trigger (0, 0, Vector3.zero, null, false);}
		VictoryTrigger.instance.CompleteObject (this);
	
		OnComplete.Invoke ();


	}

	public void unComplete()
	{completed = false;
		Debug.Log ("uncompleted");
		VictoryTrigger.instance.unComplete (this);
	}

	public void fail()
	{
		VictoryTrigger.instance.FailObjective (this);

	}



}
