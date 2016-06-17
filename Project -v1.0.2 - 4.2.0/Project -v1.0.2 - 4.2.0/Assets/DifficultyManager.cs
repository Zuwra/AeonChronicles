using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

	[Tooltip("Percentage of health enemies will haev on easy mode")]
	public float EasyHealth;
	[Tooltip("Percentage of Damage enemies will haev on easy mode")]
	public float EasyDamage;

	// Use this for initialization
	void Start () {

		if(LevelData.easyMode){
		foreach (UnitManager man in GameObject.FindObjectsOfType<UnitManager>()) {
				if (man.PlayerOwner ==2) {
					if (man.myStats) {
						man.myStats.Maxhealth *= EasyHealth;
						man.myStats.health *= EasyHealth;
					}
					foreach (IWeapon weap in man.myWeapon) {
						weap.baseDamage *= EasyDamage;
					}
				}
		
				}
		}
	}

	//TO DO : APPLY Debuffs to units created throughout the level.
	
	// Update is called once per frame
	void Update () {
	
	}



}
