using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

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
			if (win) {
				techDisplay.text = "" + (int)info.TechCredits;
			} else {
				techDisplay.text = "0";
			}

			int bonusTech = LevelData.getDifficulty ();
			if (win) {

				if (bonusTech == 1) {
					bonusTech = 0;
				} else if (bonusTech == 3) {
					bonusTech = 5;
				}
			} else {
				bonusTech = 0;
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

	

			List<VeteranStats> vetStats = GameManager.main.activePlayer.getVeteranStats ();

			for (int i = vetStats.Count - 1; i > -1; i--) {
				if (vetStats[i].UnitName == "") {

					vetStats.RemoveAt (i);

				}
			}
			//vetStats.RemoveAll (isATurret);


			float lowestDamage = 1;
			float lowestKills = 1;
			float lowestScore = 1;
			float lowestHealing = 1;
			float lowestTank = 1;
		//	Debug.Log ("Count is " + vetStats.Count);

			foreach (VeteranStats stat in vetStats) {


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
						if (bestScore.Count > 6) {
							bestScore.RemoveAt (6);

							lowestScore = bestScore [5].calculateScore();
						} else {
							lowestScore = bestScore [bestScore.Count - 1].calculateScore ();
						}


					}
				}



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
						if (bestDamage.Count > 6) {
							bestDamage.RemoveAt (6);

							lowestDamage = bestDamage [5].damageDone;
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
						if (bestKills.Count > 6) {
							bestKills.RemoveAt (6);

							lowestKills = bestKills [5].kills;
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
						if (bestHealing.Count > 6) {
							bestHealing.RemoveAt (6);

							lowestHealing = bestHealing [5].healingDone;
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
						if (bestTank.Count > 6) {
							bestTank.RemoveAt (6);

							lowestTank = bestTank [5].damageTaken;
						} else {
							lowestTank = bestTank [bestTank.Count - 1].damageTaken;
						}


					}
				}



			}

			List<VeteranStats> usedGuys = new List<VeteranStats> ();
			Dictionary<string, int> usedUnits = new Dictionary<string,int> ();

			if (bestScore.Count > 0) {
				GameObject newTemplate = (GameObject)Instantiate (VeteranStatTemplate, secondGrid);
				newTemplate.GetComponent<VeteranVicDisplayer> ().SetStats (bestScore [0], "MVP");
				usedGuys.Add (bestScore [0]);
				usedUnits.Add (bestScore [0].unitType, 1);
			}


			SortList (usedUnits, usedGuys, "Destroyer", bestKills);

			SortList (usedUnits, usedGuys, "Tank of Tanks", bestTank);

			SortList (usedUnits, usedGuys, "The Healer", bestHealing);

			SortList (usedUnits, usedGuys, "The Damager", bestDamage);

		

		}




	}

	public bool isATurret(VeteranStats stat)
	{
		if (stat.UnitName == "MiniGun" || stat.UnitName == "Repair Bay" || stat.UnitName == "Imperio Cannon" || stat.UnitName == "Fire Storm Pod") {
			return true;
		}

		return false;
	}


	void SortList(Dictionary<string,int> usedUnits, List<VeteranStats> usedGuys, string award, List<VeteranStats> thingToCompare)
	{
		if (thingToCompare.Count == 0) {
			return;
		}
		int ind = 0;
		while ( thingToCompare.Count > ind) {


			if ( usedGuys.Contains (thingToCompare [ind]) ||(  usedUnits.ContainsKey (thingToCompare [ind].unitType) && usedUnits [thingToCompare [ind].unitType]  >= 2)) {
				//Debug.Log ("Skipping " + thingToCompare[ind].unitType + "  " + thingToCompare[ind].UnitName);
				ind ++;
				}
			else
				{
					break;
				}
		}

		if (thingToCompare.Count > ind) {
			GameObject newTemplate = (GameObject)Instantiate (VeteranStatTemplate, secondGrid);
			newTemplate.GetComponent<VeteranVicDisplayer> ().SetStats (thingToCompare [ind],award);
			if (usedUnits.ContainsKey (thingToCompare [ind].unitType)) {
				usedUnits [thingToCompare [ind].unitType] = 2;
			} else {
				usedUnits.Add (thingToCompare [ind].unitType, 1);
			}

		}


	}



}
