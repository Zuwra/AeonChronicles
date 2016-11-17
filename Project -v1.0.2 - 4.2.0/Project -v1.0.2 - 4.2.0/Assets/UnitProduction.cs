using UnityEngine;
using System.Collections;

public abstract class UnitProduction: Ability {

	public GameObject unitToBuild;
	public float buildTime;
	public float buildRate = 1;
	public abstract float getProgress ();


	public abstract void startBuilding();

	public abstract void cancelBuilding();

	public abstract void DeQueueUnit();
	public void setBuildRate(float rate)
	{

		buildRate = rate;
	}

}
