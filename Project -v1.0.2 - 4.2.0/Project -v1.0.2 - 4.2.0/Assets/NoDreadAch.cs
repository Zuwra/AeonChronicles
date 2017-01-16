using UnityEngine;
using System.Collections;

public class NoDreadAch: Achievement{


	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {

			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().activePlayer.getUnitStats()) {
				if (vets.unitType == "DreadNaught") {
					return;
				}


			}
			Accomplished ();
		}
	}


}