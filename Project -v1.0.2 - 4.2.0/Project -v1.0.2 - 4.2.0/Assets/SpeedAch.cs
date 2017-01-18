using UnityEngine;
using System.Collections;

public class SpeedAch : Achievement{

	public int LevelNum;
	public float maxTime;
	public override void CheckBeginning (){
	}

	public override string GetDecription()
	{return Description;
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == LevelNum) {
				if(Clock.main.getTotalSecond() <= maxTime )

					Accomplished ();
				

			}
		}
	}


}