using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

	[Tooltip("Percentage of health enemies will have on easy mode")]
	public float EasyHealth;
	[Tooltip("Percentage of Damage enemies will have on easy mode")]
	public float EasyDamage;
	public float HardWaveReduct = .7f;

	[Tooltip("time is in minutes")]
	public float LevelOneUpgradeTime;
	public float LevelTwoUpgradeTime;
	public float LevelThreeUpgradeTime;

	[Tooltip("1 is normal time, 0-1 makes waves come faster, above 1 makes them come slower.")]
	public float EasyWaveTimeMod = 1.2f;
	[Tooltip("1 is normal time, 0-1 makes waves come faster, above 1 makes them come slower.")]
	public float HardWaveTimeMod = .8f;

	int upgradeCount;

	[Tooltip("This will also delete everything in medium list")]
	public List<GameObject> deleteOnEasy;

	public List<GameObject> deleteOnMedium;
	int difficulty;
	// Use this for initialization
	void Start () {
		difficulty = LevelData.getDifficulty ();
		Debug.Log (" Difficulty " + difficulty);

		if (difficulty == 1) {
			LevelOneUpgradeTime *= 1.5f; 
			LevelTwoUpgradeTime *= 1.5f;
			LevelThreeUpgradeTime *= 1.5f;
		} else if (difficulty == 2) {
			
		} else if (difficulty == 3) {
			LevelOneUpgradeTime *= .7f;
			LevelTwoUpgradeTime *= .7f;
			LevelThreeUpgradeTime *= .7f;
		}


		StartCoroutine (UpgradeTimer (LevelOneUpgradeTime));
		StartCoroutine (UpgradeTimer (LevelTwoUpgradeTime));
		StartCoroutine (UpgradeTimer (LevelThreeUpgradeTime));

		if (difficulty == 1) {
			foreach (UnitManager man in GameObject.FindObjectsOfType<UnitManager>()) {
				if (man.PlayerOwner == 2) {
					if (man.myStats) {
						man.myStats.Maxhealth *= EasyHealth;
						man.myStats.health *= EasyHealth;
					}
				
				}
		
			}

			foreach (WaveManager ws in  GameObject.FindObjectsOfType<WaveManager>()) {
				for (int i = 0; i < ws.myWaves.Count; i++) {
					ws.myWaves [i].waveSpawnTime *= EasyWaveTimeMod;

				}

			}

			foreach (MiningSawDamager saw in GameObject.FindObjectsOfType<MiningSawDamager>()) {
				saw.damage *= (EasyDamage);
			}

		

			foreach (GameObject obj in deleteOnEasy) {
				Destroy (obj);
			}
			foreach (GameObject obj in deleteOnMedium) {
				Destroy (obj);
			}

		} else if (difficulty == 2) {
			
			foreach (GameObject obj in deleteOnMedium) {
				Destroy (obj);
			}
		
		} else if (difficulty == 3) {
		
			foreach (WaveManager ws in  GameObject.FindObjectsOfType<WaveManager>()) {
				for (int i = 0; i < ws.myWaves.Count; i++) {
					ws.myWaves [i].waveSpawnTime *= HardWaveTimeMod;
				
				}

			}


		
		}
	}

	IEnumerator UpgradeTimer(float time)
	{
		yield return new WaitForSeconds (time * 60);
		upgradeCount++;
		foreach (UnitManager man in GameObject.FindObjectsOfType<UnitManager>()) {
			if (man.PlayerOwner == 2) {
				if (man.myStats) {
					man.myStats.armor += 1;
					foreach (IWeapon weap in man.myWeapon) {
						if (weap) {
							weap.changeAttack (0, 1 * (int)(weap.baseDamage / 10), true, null);
						}
					}
				}

			}

		}

	}



	public void SetUnitStats(GameObject obj)
	{

		UnitManager man = obj.GetComponent<UnitManager> ();

		if (difficulty == 1) {


					if (man && man.myStats) {
						man.myStats.Maxhealth *= EasyHealth;
						man.myStats.health *= EasyHealth;
					}

		} else if (difficulty == 2) {
			
		} else if (difficulty == 3) {


			if (man.myStats) {
				man.myStats.Maxhealth += EasyHealth;
				man.myStats.health += EasyHealth;
			}

		}


		if (man.myStats) {

				man.myStats.armor += upgradeCount;
				foreach (IWeapon weap in man.myWeapon) {
					if (weap) {
					weap.changeAttack (0, upgradeCount * Mathf.Max(1,(int)(weap.baseDamage / 10)), true, null);
					}
				}
		}

	}


}
