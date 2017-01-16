using UnityEngine;
using System.Collections;

public class ArmyOfOneAch : Achievement{



	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {


			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().activePlayer.getUnitStats()) {
				if (vets.UnitName != "Nimbus" && vets.kills >=20) {
					Accomplished ();
					break;
				}
			}
		
		}
	}


}
