using UnityEngine;
using System.Collections;

public class KillUnitAch :Achievement {

	public string UnitName;
	public int MinNumber;

	public override string GetDecription()
	{return Description + "          " + PlayerPrefs.GetInt (UnitName + "Deaths", 0)  + "/" + MinNumber;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd ()
	{
		if (!IsAccomplished ()) {
			int counter = 0;

			foreach (VeteranStats vets in  GameObject.FindObjectOfType<GameManager> ().playerList[1].getVeteranStats()) {
				if (vets.UnitName.Contains (UnitName) && vets.Died) {
					counter++;

				}
			}

			int total = PlayerPrefs.GetInt (UnitName + "Deaths", 0) + counter;
			PlayerPrefs.SetInt (UnitName + "Deaths", total);
			if (total >= MinNumber) {
				Accomplished ();
			}
		}

	}

	public override void Reset()
	{PlayerPrefs.SetInt (UnitName + "Deaths", 0);
		PlayerPrefs.SetInt (Title, 0);
	}
}