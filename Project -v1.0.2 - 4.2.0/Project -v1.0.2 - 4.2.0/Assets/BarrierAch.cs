using UnityEngine;
using System.Collections;

public class BarrierAch : Achievement{

	public float minBlocked;
	public string PlayerPrefTag;

	public override string GetDecription()
	{return Description + "       " + PlayerPrefs.GetInt(PlayerPrefTag,0) +"/"+minBlocked;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {

			if (PlayerPrefs.GetInt (PlayerPrefTag, 0) > minBlocked) {
				Accomplished ();
			}
			}
			
		}

	public override void Reset()
	{
		PlayerPrefs.SetInt (Title, 0);
		PlayerPrefs.SetInt (PlayerPrefTag, 0);

	}

}
