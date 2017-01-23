using UnityEngine;
using System.Collections;

public class HadrianLifeAch : Achievement{


	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}


	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == 0 && LevelData.getDifficulty () > 1) {
				GameObject had = GameObject.Find ("Hadrian");

				if (had && had.GetComponent<UnitStats>().health >= 450) {

					Accomplished ();
				}

			}
		}
	}


}
