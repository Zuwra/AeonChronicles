using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UltimateApplier : MonoBehaviour {

	//HyperCharge  Nimbus   BarrierDome   Firestorm
	public RaceManager myRace;
	// Use this for initialization


	int hyperZero;
	int hyperOne;

	int NimbusOne;
	int NimbusTwo;
	int NimbusThree;
	int NimbusFour;

	int DomeOne;
	int DomeTwo;

	int FireOne;
	int FireTwo;

	void Start () {

		myRace = GetComponent<GameManager> ().activePlayer;




		hyperZero  = PlayerPrefs.GetInt ("HyperCharge0",0);
		hyperOne = PlayerPrefs.GetInt ("HyperCharge1",0);

		switch (hyperOne) {
		case 0:

			break;
		case 1:
			RaceUIManager.instance.ultTexts[0].text += "\n Restores 33% of energy on casting.";
			break;
		case 2:
			RaceUIManager.instance.ultTexts[0].text += "\n Restores 67% of energy on casting.";
			break;
		case 3:
			RaceUIManager.instance.ultTexts[0].text += "\n Restores 100% of energy on casting.";
			break;
		}

		NimbusOne = PlayerPrefs.GetInt ("Nimbus0",0);
		NimbusTwo= PlayerPrefs.GetInt ("Nimbus1",0);
		NimbusThree= PlayerPrefs.GetInt ("Nimbus2",0);
		NimbusFour= PlayerPrefs.GetInt ("Nimbus3",0);

		DomeOne = PlayerPrefs.GetInt ("BarrierDome0",0);
		DomeTwo= PlayerPrefs.GetInt ("BarrierDome1",0);

		FireOne = PlayerPrefs.GetInt ("Firestorm0");
		FireTwo = PlayerPrefs.GetInt ("Firestorm1",0);


		Bombardment bm = (Bombardment)myRace.UltFour;
		bm.myDamage += FireOne * 15;
	
		bm.FriendlyFire = 1 - FireTwo * 2;
	
	}
	


	public void applyUlt(GameObject thingy, Object ab)
	{
		if (myRace.UltOne == ab) {
			thingy.GetComponent<AetherOvercharge> ().rechargeAmount = hyperOne * .333f;
			thingy.GetComponent<AetherOvercharge> ().attackDamage += .1f * hyperZero;
			thingy.GetComponent<AetherOvercharge> ().attackSpeed += .1f * hyperZero;
		}

		else if (myRace.UltTwo == ab) {
	
			thingy.GetComponent<IWeapon> ().changeAttackSpeed (-.2f * NimbusOne,0,true,null);


			if (NimbusTwo == 1) {
				thingy.GetComponent<UnitManager> ().abilityList [1].active = true;
			}
			if (NimbusThree == 1) {
				thingy.GetComponent<UnitManager> ().abilityList [2].active = true;
			}
			if (NimbusFour == 1) {
				thingy.GetComponent<UnitManager> ().abilityList [3].active = true;
			}

		}
		else if (myRace.UltThree == ab) {
			
			barrierShield bs= thingy.GetComponent<barrierShield> ();

			bs.duration *= 1 + DomeOne* .25f;
			bs.Health *= 1 + DomeTwo * .25f;

		}
		else if (myRace.UltFour == ab) {

		}

	}
}
