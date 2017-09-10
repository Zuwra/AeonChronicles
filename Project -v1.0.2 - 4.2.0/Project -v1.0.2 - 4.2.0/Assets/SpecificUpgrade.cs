using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SpecificUpgrade : Upgrade {

	public List<string> unitsToApply = new List<string>();

	override
	public void applyUpgrade(GameObject obj)
	{	
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

	public abstract float ChangeString (string name, float number);

	public virtual string AddString (string name, string ToAddOn)
	{
		return ToAddOn;
	}


}
