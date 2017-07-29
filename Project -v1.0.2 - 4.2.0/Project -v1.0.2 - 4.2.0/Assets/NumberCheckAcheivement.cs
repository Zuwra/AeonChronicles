using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCheckAcheivement: Achievement{

	public float number;
	public string PlayerPrefTag;
	public bool lessThan;

	public override string GetDecription()
	{return Description ;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			if (GameObject.FindObjectOfType<VictoryTrigger> ().levelNumber == getLevelNumber ()) {
				if (!lessThan) {
					if (PlayerPrefs.GetInt (PlayerPrefTag, 0) > number) {
						Accomplished ();
					}
				} else {
					if (PlayerPrefs.GetInt (PlayerPrefTag, 0) < number) {
						Accomplished ();
					}
				}
			}
		}
		PlayerPrefs.SetInt (PlayerPrefTag,0);

	}

	public override void Reset()
	{
		PlayerPrefs.SetInt (Title, 0);
		PlayerPrefs.SetInt (PlayerPrefTag, 0);

	}

}
