using UnityEngine;
using System.Collections;

public abstract class UnitProduction: Ability {

	public abstract float getProgress ();


	public abstract void startBuilding();

	public abstract void cancelBuilding();


}
