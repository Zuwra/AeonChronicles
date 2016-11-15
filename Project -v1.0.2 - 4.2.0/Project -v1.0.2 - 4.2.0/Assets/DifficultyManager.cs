using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

	[Tooltip("Percentage of health enemies will have on easy mode")]
	public float EasyHealth;
	[Tooltip("Percentage of Damage enemies will have on easy mode")]
	public float EasyDamage;
	public float HardWaveReduct = .7f;

	// Use this for initialization
	void Start () {

		if (LevelData.getDifficulty() == 1) {
			foreach (UnitManager man in GameObject.FindObjectsOfType<UnitManager>()) {
				if (man.PlayerOwner == 2) {
					if (man.myStats) {
						man.myStats.Maxhealth *= EasyHealth;
						man.myStats.health *= EasyHealth;
					}
				
				}
		
			}

			foreach (MiningSawDamager saw in GameObject.FindObjectsOfType<MiningSawDamager>()) {
				saw.damage *= (EasyDamage * 1.5f);
			}
		} else if (LevelData.getDifficulty() == 2) {
			foreach (WaveSpawner ws in  GameObject.FindObjectsOfType<WaveSpawner>()) {
				for (int i = 0; i < ws.myWaves.Count; i++) {
					float releaseT = ws.myWaves [i].releaseTime;
					ws.myWaves [i].setRelease (releaseT * HardWaveReduct);//.releaseTime =releaseT * HardWaveReduct;

				}
			
			}
		
		} else if (LevelData.getDifficulty() == 3) {
		
			foreach (WaveSpawner ws in  GameObject.FindObjectsOfType<WaveSpawner>()) {
				for (int i = 0; i < ws.myWaves.Count; i++) {
					float releaseT = ws.myWaves [i].releaseTime;
					ws.myWaves [i].setRelease (releaseT * HardWaveReduct * HardWaveReduct);//.releaseTime =releaseT * HardWaveReduct;

				}

			}
		
		}
	}



	public void SetUnitStats(GameObject obj)
	{

		UnitManager man = obj.GetComponent<UnitManager> ();

		if (LevelData.getDifficulty() == 1) {


					if (man.myStats) {
						man.myStats.Maxhealth *= EasyHealth;
						man.myStats.health *= EasyHealth;
					}

				

		} else if (LevelData.getDifficulty() == 2) {
			



		} else if (LevelData.getDifficulty() == 3) {


			if (man.myStats) {
				man.myStats.Maxhealth += EasyHealth;
				man.myStats.health += EasyHealth;
			}



		}
	}

	//TO DO : APPLY Debuffs to units created throughout the level.
	
	// Update is called once per frame
	void Update () {
	
	}



}
