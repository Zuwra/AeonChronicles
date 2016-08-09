using UnityEngine;
using System.Collections;

public class CloseScene :SceneEventTrigger {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		this.gameObject.SetActive (false);

	}
}
