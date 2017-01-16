using UnityEngine;
using System.Collections;

public class AllDestroyed : Achievement{

	public int LevelNum;

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == LevelNum) {
				if (GameObject.FindObjectOfType<GameManager> ().playerList [1].unitList.Count == 0) {
				
					Accomplished ();
				}
			
			}
		}
	}


}
