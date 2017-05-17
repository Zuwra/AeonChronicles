using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchAI : MonoBehaviour {

	UnitManager myManager;
	RaceManager raceMan;


	// Use this for initialization
	void Start () {

		myManager = GetComponent<UnitManager> ();
		raceMan = GameManager.main.playerList [0];
		InvokeRepeating ("FindTarget", 40, 5);
	}
	
	// Update is called once per frame
	void FindTarget () {

		if (!(myManager.getState () is DefaultState)) {
			return;
		}


		foreach (KeyValuePair<string, List<UnitManager>> pair in raceMan.getUnitList()) {
			foreach (UnitManager man in pair.Value) {
				if (!man) {
					continue;}

				if (man.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {

					myManager.GiveOrder (Orders.CreateAttackMove (man.transform.position));
					return;
				} else {
					break;
				}
			}
		}


		
	}
}
