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

	// Use this for initialization
	void Start () {
		if (LevelData.myLevels == null) {
			Debug.Log ("No levels");
			return;
		}

		if (LevelData.myLevels.Count > 0 && LevelData.ComingFromLevel) {

			SetResults (LevelData.myLevels [LevelData.currentLevel - 1]);
			PlayerPrefs.SetInt ("TechAmount", LevelData.myLevels [LevelData.currentLevel - 1].TechCredits);
			if (LevelData.currentLevel > PlayerPrefs.GetInt ("LastLevel")) {
				PlayerPrefs.SetInt ("LastLevel", LevelData.currentLevel);
			}

		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void SetResults(LevelData.levelInfo info)
	{
		MissionManager.main.toggleVictory ();
		//myCanvas.enabled = true;
		timeDisplay.text = ""+info.time;
		enemyDisplay.text =""+ info.EnemiesDest;
		allyDisplay.text = ""+info.unitsLost;
		objDisplay.text = ""+info.bonusObj;
		techDisplay.text = ""+info.TechCredits;

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

		foreach (VeteranStats vet in LevelData.myVets) {
			if (vet.unitType != "MiniGun" && vet.unitType != "Imperio Cannon") {
		
				Uname += vet.UnitName + "\n";
				UType += vet.unitType + "\n";
				killString += vet.kills + "\n";
				damageS += vet.damageDone + "\n";
				energyS += vet.energyGained + "\n";
				ArmorS += vet.mitigatedDamage + "\n";
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
