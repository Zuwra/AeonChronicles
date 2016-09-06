﻿using UnityEngine;
using System.Collections;

public class DeathWinTrigger : MonoBehaviour,Modifier {

	// Use this for initialization

	// This script gets added to units that need to die. Added through the TargetDeathTrigger Script
	void Start () {
		GetComponent<UnitStats> ().addDeathTrigger (this);
		//Debug.Log ("Adding to " + this.gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public float modify(float num, GameObject obj)
	{
		foreach (TargetDeathVictory vict in GameObject.FindObjectsOfType<TargetDeathVictory> ()) {
			vict.IDied (this.gameObject);
		}

		foreach (BonusObjectiverTimer vict in GameObject.FindObjectsOfType<BonusObjectiverTimer> ()) {
			vict.IDied (this.gameObject);
		}

		return num;
	}
}
