using UnityEngine;
using System.Collections;

public class HadrianLifeAch : Achievement{

	public string UnitName;
	public float maxDamage;
	public int levelNum;
	public int lowestDifficulty;
	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}


	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			//Debug.Log ("level " + GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == 0 +"     difficulty " + LevelData.getDifficulty ());
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber != levelNum || LevelData.getDifficulty() < lowestDifficulty) {
				return;

			}
			float totalDamage = 0;
			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().playerList[0].getUnitStats()) {
				if (vets.UnitName == UnitName) {
					totalDamage += vets.damageTaken;

					if (vets.damageTaken <= maxDamage) {
						Accomplished ();
					}

				}
			}
			if (totalDamage <= maxDamage) {
				Accomplished ();
			}
		}
	}


}
