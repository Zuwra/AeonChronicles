using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeath : MonoBehaviour, Modifier {
	private UnitStats unitStats;
	public GameObject explosion;
	[Tooltip("If this unit has less than this amount of energy it wont explode")]
	public float mininumEnergy = 7;
	// Use this for initialization
	void Start () {
		unitStats = GetComponent<UnitStats> ();
		unitStats.addDeathTrigger (this);
	}
	
	public float modify(float damage, GameObject source, DamageTypes.DamageType theType) {
		DayexaShield dayexaShield = GetComponent<DayexaShield> ();
		if (dayexaShield) {
			float energy = unitStats.currentEnergy;
			if (energy < mininumEnergy) {
				return damage;
			}
			//explosion.explode (energy * 45);
			if (explosion) {
				GameObject explode = (GameObject)Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
				//Debug.Log ("INstantiating explosion");

				explosion Escript = explode.GetComponent<explosion> ();
				if (Escript) {
					Escript.setSource (this.gameObject);
					Escript.damageAmount = energy;
				}
			}
		}
		return damage;
	}
}
