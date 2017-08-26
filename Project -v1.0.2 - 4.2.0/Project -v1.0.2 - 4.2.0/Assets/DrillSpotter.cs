using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSpotter : VisionTrigger {




	public override void  UnitEnterTrigger(UnitManager manager)
	{
		LaserDrill.instance.addtarget (manager);
	}

	public override void  UnitExitTrigger(UnitManager manager)
	{
		LaserDrill.instance.removeTarget (manager);
	}

	public void Dying()
	{
		LaserDrill.instance.removeTargets (InVision);
	}


}
