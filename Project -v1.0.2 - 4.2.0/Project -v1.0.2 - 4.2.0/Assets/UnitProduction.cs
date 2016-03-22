using UnityEngine;
using System.Collections;

public abstract class UnitProduction: Ability {

	public GameObject unitToBuild;

	public abstract float getProgress ();


	public abstract void startBuilding();

	public abstract void cancelBuilding();

	public abstract void DeQueueUnit();


}
