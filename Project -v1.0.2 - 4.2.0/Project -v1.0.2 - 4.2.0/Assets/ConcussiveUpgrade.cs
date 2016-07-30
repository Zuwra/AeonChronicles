using UnityEngine;
using System.Collections;

public class ConcussiveUpgrade :Upgrade{


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



	//[ToolTip("Only fill these in if this upgrade replaces another one")]

	//public GameObject UIButton;

	public override void applyUpgrade (GameObject obj){

		obj.GetComponent<SlowDebuff> ().enabled = true;


	}


	public override void unApplyUpgrade (GameObject obj){

		obj.GetComponent<SlowDebuff> ().enabled = false;

	}
}
