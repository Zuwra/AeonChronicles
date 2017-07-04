using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCubeAchievement : Achievement{

	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){

		float lastDeathTime =0;
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == 6) {
				foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().playerList[1].getUnitStats()) {
					
					if (vets.unitType == "Death Cube" && vets.Died) {
						if (lastDeathTime != 0) {
							if (Mathf.Abs (lastDeathTime - vets.DeathTime) < 60) {
								Accomplished ();
							}
						} else {
							lastDeathTime = vets.DeathTime;
						}

					}


				}
			}
		}
	}


}