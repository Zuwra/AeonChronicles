using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BridgeActivator : VisionTrigger {

	public GameObject Bridge;

	public GameObject DeathZone;
	void Start()
	{
		Bridge.SetActive (false);
		DeathZone.SetActive (true);
		StartCoroutine (DeathRescan ());
	}

	public override void UnitExitTrigger(UnitManager manager)
	{
		if (InVision.Count == 0) {
			Bridge.SetActive (false);
			DeathZone.SetActive (true);
			StartCoroutine (DeathRescan ());
		}
	}

	public override void UnitEnterTrigger(UnitManager manager)
	{
		Bridge.SetActive (true);
		DeathZone.SetActive (false);
		StartCoroutine (DeathRescan ());
	}



	IEnumerator DeathRescan()
	{	
		GraphUpdateObject b =new GraphUpdateObject( new Bounds(transform.position,Vector3.one * 75)); 
		yield return new WaitForSeconds (.05f);

		AstarPath.active.UpdateGraphs (b);

	}
}
