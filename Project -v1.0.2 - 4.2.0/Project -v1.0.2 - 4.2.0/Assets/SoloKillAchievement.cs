using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloKillAchievement : Achievement{

	public string UnitName;
	public int levelNum;
	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}


	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			//Debug.Log ("level " + GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == 0 +"     difficulty " + LevelData.getDifficulty ());
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber != levelNum) {
				return;
			}


			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().playerList[1].getUnitStats()) {
				if (vets.Died && vets.UnitName != UnitName) {
					return;
				}
			}
			Accomplished ();
				
		}
	}


}
