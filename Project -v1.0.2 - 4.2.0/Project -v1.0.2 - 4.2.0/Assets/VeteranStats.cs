using UnityEngine;
using System.Collections;
using System;

public class VeteranStats : IComparable<VeteranStats>{

	public string unitType;
	public string UnitName;
	public float damageDone;
	public float mitigatedDamage;
	public int kills;
	public float energyGained;
	public float healingDone;
	public float damageTaken;
	public float misc;
	public bool isWarrior;

	public VeteranStats(bool hasName, string myType, bool isW)
	{unitType = myType;
		isWarrior = isW;

		if (hasName) {
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


	public int CompareTo(VeteranStats obja)
	{


			VeteranStats a = (VeteranStats)obja;
		

			if (a.calculateScore () < calculateScore()) {
				return -1;
			} else {
				return 1;
			}
		
	
	}

}
