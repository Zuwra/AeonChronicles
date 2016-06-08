using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitUtility {



	public static void applyWeaponTrigger(UnitManager manager, Notify trig)
	{
		foreach (IWeapon i in manager.myWeapon) {
			i.triggers.Add (trig);
		
		}
	}

	public static void removeWeaponTrigger(UnitManager manager, Notify trig)
	{
		foreach (IWeapon i in manager.myWeapon) {
			if(i.triggers.Contains(trig))
				i.triggers.Remove(trig);

		}
	}


}
