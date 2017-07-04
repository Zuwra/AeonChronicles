using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggAchievement: Achievement{

	public int eggCount;
	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}


	public override void CheckEnd (){
		if (!IsAccomplished () && LevelData.getDifficulty() > 1) {

			int count = 0;
			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().playerList[0].getVeteranStats()) {
				if (vets.Died && vets.UnitName == "Bunny Egg" && vets.damageTaken >= 119) {
					count++;
				}
			
			}
			if (eggCount <= count) {
				Accomplished ();
			}


			}
		}

}