using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

	[Tooltip("Percentage of health enemies will have on easy mode")]
	public float EasyHealth;
	[Tooltip("Percentage of Damage enemies will have on easy mode")]
	public float EasyDamage;
	public float HardWaveReduct = .6f;

	// Use this for initialization
	void Start () {

		if (LevelData.easyMode) {
			foreach (UnitManager man in GameObject.FindObjectsOfType<UnitManager>()) {
				if (man.PlayerOwner == 2) {
					if (man.myStats) {
						man.myStats.Maxhealth *= EasyHealth;
						man.myStats.health *= EasyHealth;
					}
					//	for (int i = 0; i < man.myWeapon.Count; i++) {
					//man.myWeapon[i].changeAttackSpeed(1 +(1 - EasyDamage), 0, true, this);
					//}
					//foreach (IWeapon weap in man.myWeapon) {
					//	weap.changeAttackSpeed (1 +(1 - EasyDamage), 0, true, this);
					//}
				}
		
			}

			foreach (MiningSawDamager saw in GameObject.FindObjectsOfType<MiningSawDamager>()) {
				saw.damage *= (EasyDamage * 1.5f);
			}
		} else {
			foreach (WaveSpawner ws in  GameObject.FindObjectsOfType<WaveSpawner>()) {
				for(int i = 0; i < ws.waveOneTimes.Count; i++){
					ws.waveOneTimes [i] *= HardWaveReduct;

				}
				for(int i = 0; i < ws.waveTwoTimes.Count; i++){
					ws.waveTwoTimes [i] *= HardWaveReduct;

				}
				for(int i = 0; i < ws.waveThreeTimes.Count; i++){
					ws.waveThreeTimes [i] *= HardWaveReduct;

				}
			}
		
		}
	}

	//TO DO : APPLY Debuffs to units created throughout the level.
	
	// Update is called once per frame
	void Update () {
	
	}



}
