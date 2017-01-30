using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UltimateApplier : MonoBehaviour {


	public RaceManager myRace;
	// Use this for initialization
	void Start () {

		myRace = GetComponent<GameManager> ().activePlayer;

		Bombardment bm = (Bombardment)myRace.UltFour;
		bm.shotCount += LevelData.getUltLevel(4) * 15;
		bm.myDamage += LevelData.getUltLevel(4) * 10;


	
	}
	


	public void applyUlt(GameObject thingy, Object ab)
	{
		if (myRace.UltOne == ab) {

			thingy.GetComponent<AetherOvercharge> ().rechargeAmount = LevelData.getUltLevel(1) * .33f;}

		else if (myRace.UltTwo == ab) {
			if (LevelData.getUltLevel (2) > 3) {
			
			}
			thingy.GetComponent<UnitStats> ().health *= 1 + LevelData.getUltLevel(2) * .1f;
			thingy.GetComponent<UnitStats> ().Maxhealth *= 1 + LevelData.getUltLevel(2) * .1f;
			thingy.GetComponent<selfDestructTimer> ().timer *= 1 + LevelData.getUltLevel(2) * .1f;

			if (LevelData.getUltLevel(2) >= 1) {
				thingy.GetComponent<UnitManager> ().abilityList [1].active = true;
			}
			if (LevelData.getUltLevel(2) >= 2) {
				thingy.GetComponent<UnitManager> ().abilityList [2].active = true;
			}

		}
		else if (myRace.UltThree == ab) {
			
			barrierShield bs= thingy.GetComponent<barrierShield> ();
			bs.duration *= 1 + LevelData.getUltLevel(3) * .25f;
			bs.Health *= 1 + LevelData.getUltLevel(3) * .50f;

		}
		else if (myRace.UltFour == ab) {

		}

	}
}
