using UnityEngine;
using System.Collections;

public class ArmyOfOneAch : Achievement{

	public override string GetDecription()
	{return Description;
	}

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
