using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitLostDefeat : MonoBehaviour, LethalDamageinterface {

	public bool noUnits;
	public bool noBuildings;
	public List<GameObject> heros = new List<GameObject> ();
	VictoryTrigger victory;
	RaceManager myRace;



	// Use this for initialization
	void Start () {
		victory = GameObject.FindObjectOfType<VictoryTrigger> ();
		myRace = GameObject.FindObjectOfType<GameManager> ().activePlayer;
		myRace.addActualDeathWatcher (this);
	}
	

	public bool lethalDamageTrigger(UnitManager obj, GameObject source)
	{
		if (heros.Contains (obj.gameObject)) {
			victory.Lose ();

			return true;
		}

		if (noUnits && noBuildings) {
			if (myRace.getUnitList ().Count == 0) {
				victory.Lose ();

				return true;
			}
			return false;
		} else if (noUnits) {

			foreach (KeyValuePair<string, List<UnitManager>> pair in  myRace.getUnitList()) {
				foreach (UnitManager unit in pair.Value) {
					Debug.Log ("checking " + unit);
					if (!unit.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
						return true;
					}
				}
			}
		} else if (noBuildings) {
			foreach (KeyValuePair<string, List<UnitManager>> pair in  myRace.getUnitList()) {
				foreach (UnitManager unit in pair.Value) {
				// We have a structure, so we don't lose
				if (unit.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {

					return true;
				}
			}
			}
		}
		victory.Lose ();
		return false;
	}

}