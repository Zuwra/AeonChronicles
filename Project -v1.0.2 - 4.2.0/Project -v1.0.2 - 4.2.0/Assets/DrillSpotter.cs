using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSpotter : VisionTrigger {



	public LineRenderer line;

	Coroutine currentTargetLine;

	public override void  UnitEnterTrigger(UnitManager manager)
	{
		LaserDrill.instance.addtarget (manager);
		if (currentTargetLine == null) {
			currentTargetLine = StartCoroutine (targetUnit ());
		}

	}

	public override void  UnitExitTrigger(UnitManager manager)
	{
		LaserDrill.instance.removeTarget (manager);
	}

	public void Dying()
	{
		LaserDrill.instance.removeTargets (InVision);
	}

	IEnumerator targetUnit()
	{

		while (LaserDrill.instance.currentTarget) {
			if (InVision.Contains (LaserDrill.instance.currentTarget)) {
				line.SetPositions (new Vector3[]{ transform.position, LaserDrill.instance.currentTarget.transform.position }); 
			} else {
				line.SetPositions (new Vector3[]{ transform.position, transform.position }); 
			}
			yield return null;
		}
		line.SetPositions (new Vector3[]{ transform.position, transform.position }); 
		currentTargetLine = null;
	}


}
