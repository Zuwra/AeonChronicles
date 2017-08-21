using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BridgeActivator : VisionTrigger {

	public GameObject Bridge;

	void Start()
	{
		Bridge.SetActive (false);
	}

	public override void UnitExitTrigger(UnitManager manager)
	{
		if (InVision.Count == 0) {
			Bridge.SetActive (false);
			StartCoroutine (DeathRescan ());
		}
	}

	public override void UnitEnterTrigger(UnitManager manager)
	{
		Bridge.SetActive (true);
		StartCoroutine (DeathRescan ());
	}



	IEnumerator DeathRescan()
	{	
		GraphUpdateObject b =new GraphUpdateObject( new Bounds(transform.position,Vector3.one * 75)); 
		yield return new WaitForSeconds (.2f);

		AstarPath.active.UpdateGraphs (b);

	}
}
