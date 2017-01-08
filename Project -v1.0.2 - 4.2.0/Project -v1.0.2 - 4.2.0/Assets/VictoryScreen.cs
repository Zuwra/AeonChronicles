using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour {

	public Canvas myCanvas;
	public Text timeDisplay;
	public Text enemyDisplay;
	public Text allyDisplay;
	public Text objDisplay;
	public Text techDisplay;
	public Text ResourceTitle;
	public Text ResourceDisplay;


	public Text UnitName;
	public Text UnitType;
	public Text kills;
	public Text damageDealt;
	public Text energyRegen;
	public Text ArmorDamage;
	public bool victoryScreen;

	// Use this for initialization
	void Start () {
		if (LevelData.getsaveInfo().myLevels == null) {
		//	Debug.Log ("No levels");
			return;
		}

	


	}



	public void SetResults(LevelData.levelInfo info, bool win)
	{
		if (win == victoryScreen) {
			if (MissionManager.main) {
				MissionManager.main.toggleVictory ();
			}
			myCanvas.enabled = true;
			timeDisplay.text = "" + info.time;
			enemyDisplay.text = "" + (int)info.EnemiesDest;
			allyDisplay.text = "" + (int)info.unitsLost;
			objDisplay.text = "" + info.bonusObj;
			techDisplay.text = "" + (int)info.TechCredits;

			if (info.Resources > 0) {
				ResourceTitle.text = "Resources Collected: ";
				ResourceDisplay.text = "" + info.Resources;
			} else {
				ResourceTitle.text = "";
				ResourceDisplay.text = "";
			}

			string Uname = "Name\n\n";
			string UType = "Unit Type\n\n";
			string killString = "Kills\n\n";
			string damageS = "Damage Dealt\n";
			string energyS = "Energy Regenerated\n";
			string ArmorS = "Damage on Armor\n";

			int index = 1;
			foreach (VeteranStats vet in GameObject.FindObjectOfType<GameManager>().activePlayer.getUnitStats()) {
				if (vet.unitType != "MiniGun" && vet.unitType != "Imperio Cannon" && vet.unitType != "Aether Core" && vet.unitType != "Armory" && vet.unitType != "Construction Yard") {
		
					Uname += index + ". " + vet.UnitName + "\n";
					UType += vet.unitType + "\n";
					killString += (int)vet.kills + "\n";
					damageS += (int)vet.damageDone + "\n";
					energyS += (int)vet.energyGained + "\n";
					ArmorS += (int)vet.mitigatedDamage + "\n";
					index++;
				}
			
		
			}




			UnitName.text = Uname;
			UnitType.text = UType;
			kills.text = killString;
			damageDealt.text = damageS;
			energyRegen.text = energyS;
			ArmorDamage.text = ArmorS;

		}


	}
		





}
