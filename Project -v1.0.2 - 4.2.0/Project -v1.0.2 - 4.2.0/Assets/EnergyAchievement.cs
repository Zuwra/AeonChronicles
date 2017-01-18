using UnityEngine;
using System.Collections;

public class EnergyAchievement : Achievement{

	public float minEnergy;

	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {

			float counter = 0;
			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().activePlayer.getUnitStats()) {
				counter += vets.energyGained;

			}
			if (counter >= minEnergy) {
				Accomplished ();
			}
		}
	}


}
