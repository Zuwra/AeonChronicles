using UnityEngine;
using System.Collections;

public class AllDestroyed : Achievement{

	public int LevelNum;

	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}


	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == LevelNum) {
				if (GameObject.FindObjectOfType<GameManager> ().playerList [1].getUnitList().Count == 0) {
				
					Accomplished ();
				}
			
			}
		}
	}


}
