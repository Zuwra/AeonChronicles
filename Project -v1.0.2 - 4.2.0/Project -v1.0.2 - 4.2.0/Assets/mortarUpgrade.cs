using UnityEngine;
using System.Collections;

public class mortarUpgrade : Upgrade {

	public int podIncrease = 2;
	public float reloadDecrease = .5f;

	override
	public void applyUpgrade(GameObject obj)
	{

		UnitManager manager = obj.GetComponent<UnitManager>();
		mortarPod pod = obj.GetComponent<mortarPod> ();
		if (pod)
		{
			
			pod.totalShots += podIncrease;
			pod.reloadRate -= reloadDecrease;

		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}
}
