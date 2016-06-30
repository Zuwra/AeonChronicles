using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MassCapture : SceneEventTrigger {

	public List<CapturableUnit> captures = new List<CapturableUnit>();



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){

				foreach (CapturableUnit u in captures) {
					if (u != null) {
						u.capture ();
					}
				}
			

	}

}
