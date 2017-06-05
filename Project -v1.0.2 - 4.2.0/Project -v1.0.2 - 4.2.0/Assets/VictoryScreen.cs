using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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



	public bool victoryScreen;

	public Text DifficultyTitle;
	public Text DifficultyText;

	public GameObject GridParent;
	public Transform secondGrid;
	public GameObject VeteranStatTemplate;

	public List<VeteranStats> bestDamage = new List<VeteranStats> ();
	public List<VeteranStats> bestKills = new List<VeteranStats> ();
	public List<VeteranStats> bestScore = new List<VeteranStats> ();
	public List<VeteranStats> bestHealing = new List<VeteranStats> ();
	public List<VeteranStats> bestTank = new List<VeteranStats> ();

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


			int bonusTech = LevelData.getDifficulty ();
			if (bonusTech == 1) {
				bonusTech = 0;
			} else if (bonusTech == 3) {
				bonusTech = 5;
			}

			if (DifficultyText) {
				if (bonusTech == 0) {
					DifficultyTitle.enabled = false;
					DifficultyText.enabled = false;
				} else {
					DifficultyText.text = "" + bonusTech;
				}
			}

			if (info.Resources > 0) {
				ResourceTitle.text = "Resources Collected ";
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


			List<VeteranStats> vetStats = GameManager.main.activePlayer.getVeteranStats ();
			/*
			List<VeteranStats> bestDamage = new List<VeteranStats> ();
			List<VeteranStats> bestKills = new List<VeteranStats> ();
			List<VeteranStats> bestScore = new List<VeteranStats> ();
			List<VeteranStats> bestHealing = new List<VeteranStats> ();
			List<VeteranStats> bestTank = new List<VeteranStats> ();
*/
			float lowestDamage = -1;
			float lowestKills = -1;
			float lowestScore = -1;
			float lowestHealing = -1;
			float lowestTank = -1;
			Debug.Log ("Count is " + vetStats.Count);

			foreach (VeteranStats stat in vetStats) {
				if (stat.damageDone >= lowestDamage) {
					if (bestDamage.Count == 0) {
						bestDamage.Add (stat);
						lowestDamage = stat.damageDone;
					} else {
						for (int i = 0; i < bestDamage.Count; i++) {
							if (stat.damageDone > bestDamage [i].damageDone) {
								bestDamage.Insert (i,stat);
								break;
							}
						}
						if (bestDamage.Count > 3) {
							bestDamage.RemoveAt (3);

							lowestDamage = bestDamage [2].damageDone;
						} else {
							lowestDamage = bestDamage [bestDamage.Count - 1].damageDone;
						}


					}
				}

				if (stat.kills >= lowestKills) {
					if (bestKills.Count == 0) {
						bestKills.Add (stat);
						lowestKills = stat.kills;
					} else {
						for (int i = 0; i < bestKills.Count; i++) {
							if (stat.kills > bestKills [i].kills) {
								bestKills.Insert (i, stat);
								break;
							}
						}
						if (bestKills.Count > 3) {
							bestKills.RemoveAt (3);

							lowestKills = bestKills [2].kills;
						} else {
							lowestKills = bestKills [bestKills.Count - 1].kills;
						}


					}
				}


				if (stat.healingDone >= lowestHealing) {
					if (bestHealing.Count == 0) {
						bestHealing.Add (stat);
						lowestHealing = stat.healingDone;
					} else {
						for (int i = 0; i < bestHealing.Count; i++) {
							if (stat.healingDone > bestHealing [i].healingDone) {
								bestHealing.Insert (i, stat);
								break;
							}
						}
						if (bestHealing.Count > 3) {
							bestHealing.RemoveAt (3);

							lowestHealing = bestHealing [2].healingDone;
						} else {
							lowestHealing = bestHealing [bestHealing.Count - 1].healingDone;
						}


					}
				}

				if (stat.damageTaken >= lowestTank) {
					if (bestTank.Count == 0) {
						bestTank.Add (stat);
						lowestTank = stat.damageTaken;
					} else {
						for (int i = 0; i < bestTank.Count; i++) {
							if (stat.damageTaken > bestTank [i].damageTaken) {
								bestTank.Insert (i, stat);
								break;
							}
						}
						if (bestTank.Count > 3) {
							bestTank.RemoveAt (3);

							lowestTank = bestTank [2].damageTaken;
						} else {
							lowestTank = bestTank [bestTank.Count - 1].damageTaken;
						}


					}
				}


				float score = stat.calculateScore ();
				if (score >= lowestScore) {
					if (bestScore.Count == 0) {
						bestScore.Add (stat);
						lowestScore = score;
					} else {
						for (int i = 0; i < bestScore.Count; i++) {
							if (score > bestScore [i].calculateScore ()) {
								bestScore.Insert (i, stat);
								break;
							}
						}
						if (bestScore.Count > 3) {
							bestScore.RemoveAt (3);

							lowestScore = bestScore [2].calculateScore();
						} else {
							lowestScore = bestScore [bestScore.Count - 1].calculateScore ();
						}


					}
				}





			}

			Debug.Log ("I am on " + this.gameObject);
			GameObject newTemplate =  (GameObject)Instantiate (VeteranStatTemplate, secondGrid);
			newTemplate.GetComponent<VeteranVicDisplayer> ().SetStats (bestKills [0], "Destroyer");

			newTemplate =  (GameObject)Instantiate (VeteranStatTemplate, secondGrid);
			newTemplate.GetComponent<VeteranVicDisplayer> ().SetStats (bestTank [0], "Tank of Tanks");

			newTemplate =  (GameObject)Instantiate (VeteranStatTemplate, secondGrid);
			newTemplate.GetComponent<VeteranVicDisplayer> ().SetStats (bestHealing [0], "The Healer");

			newTemplate =  (GameObject)Instantiate (VeteranStatTemplate, secondGrid);
			newTemplate.GetComponent<VeteranVicDisplayer> ().SetStats (bestDamage [0], "The Damager");

			newTemplate =  (GameObject)Instantiate (VeteranStatTemplate, secondGrid);
			newTemplate.GetComponent<VeteranVicDisplayer> ().SetStats (bestScore [0], "MVP");

		}




	}



}
