using UnityEngine;
using System.Collections;

public class SpeedUpgrade  : Upgrade {

	public float PercIncrease;
	public float flatIncrease;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override
	public void applyUpgrade (GameObject obj){

		UnitManager manager = obj.GetComponent<UnitManager>();
		//Debug.Log ("Checking " + obj);
		manager.cMover.changeSpeed(PercIncrease,flatIncrease,true,null);

	}

	public override void unApplyUpgrade (GameObject obj){

	}


}
