using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecificUpgrade : Upgrade {

	public List<string> unitsToApply = new List<string>();

	override
	public void applyUpgrade(GameObject obj)
	{	Debug.Log ("Applying upper ");
	}

	override
	public void unApplyUpgrade(GameObject obj)
	{
	}

	protected bool confirmUnit(GameObject obj)
	{
		UnitManager manage = obj.GetComponent<UnitManager> ();

		if (unitsToApply.Contains (manage.UnitName)) {
			return true;
		}
		return false;
	}


}
