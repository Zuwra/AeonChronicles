using UnityEngine;
using System.Collections;

public class NoDreadAch: Achievement{

	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == 2) {
				foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().activePlayer.getUnitStats()) {
					if (vets.unitType == "DreadNaught") {
						return;
					}


				}
				Accomplished ();
			}
		}
	}


}