using UnityEngine;
using System.Collections;

public class VeteranStats {

	public string UnitName;
	public float damageDone;
	public float mitigatedDamage;
	public int kills;
	public float energyGained;
	public float healingDone;
	public float damageTaken;
	public float misc;

	public VeteranStats(bool hasName)
	{if (hasName) {
			UnitName = RaceNames.getInstance ().getName ();
		} else {
			UnitName = "";}
	}



	public float calculateScore()
	{
		float score = 0;
		score += damageDone;
		score += mitigatedDamage * 2;
		score += kills * 100;
		score += energyGained;
		score += healingDone;
		score += damageTaken / 2;
		score += misc;


		return score;
	}


}
