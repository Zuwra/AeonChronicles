using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class  UnitState  {


	public UnitManager myManager;

	// Update is called once per frame
	public abstract void Update ();

//	public abstract void attackResponse(UnitManager src, float amount);


	public virtual void attackResponse(UnitManager src, float amount)
	{
		if(src){// && !enemy){

			if (src.PlayerOwner != myManager.PlayerOwner) {

				if(amount > 0){
					foreach (UnitManager ally in myManager.allies) {
						if (ally) {
							UnitState hisState = ally.getState ();
							if (hisState is DefaultState) {
								hisState.attackResponse (src, 0);
							}

						}
					}
				}

			}
		}

	}

	public abstract void initialize();

	public abstract void endState();

}
