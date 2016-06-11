using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {
	[TextArea(2,10)]
	public string description;
	public int reward;
	public bool ActiveOnStart;
	public bool bonus;
	public bool completed;

	// Use this for initialization
	void Start () {
		if (ActiveOnStart) {
			VictoryTrigger.instance.addObjective (this);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void complete()
	{
		completed = true;
		VictoryTrigger.instance.CompleteObject (this);
	}




}
