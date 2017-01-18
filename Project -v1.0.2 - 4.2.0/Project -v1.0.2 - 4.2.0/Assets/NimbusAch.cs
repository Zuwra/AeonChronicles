using UnityEngine;
using System.Collections;

public class NimbusAch  : Achievement{

	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {

			float counter = 0;
			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().activePlayer.getUnitStats()) {
				if (vets.UnitName == "Nimbus") {
					counter++;
				}
			}
			if (counter >= 5) {
				Accomplished ();
			}
		}
	}


}
