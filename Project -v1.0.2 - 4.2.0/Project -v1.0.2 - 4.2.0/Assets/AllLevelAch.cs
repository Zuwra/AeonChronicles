using UnityEngine;
using System.Collections;

public class AllLevelAch: Achievement{

	public int LevelNum;

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber >= LevelNum) {
				
					Accomplished ();


			}
		}
	}


}