using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class deathTrigger : MonoBehaviour {

	public int player = 1;

	public  int index;
	public  float input;
	public  Vector3 location;
	public  GameObject target; 
	public  bool doIt;
	public float delay;


	public List<SceneEventTrigger> myTriggers;



	public void Dying()
	{
		foreach (SceneEventTrigger trig in myTriggers) {
			Debug.Log ("Triggering " + trig);
			trig.trigger (index, input, location, target, doIt);
		}


	}






}
