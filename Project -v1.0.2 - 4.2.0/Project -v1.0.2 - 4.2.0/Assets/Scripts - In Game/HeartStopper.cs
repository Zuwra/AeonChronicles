﻿using UnityEngine;
using System.Collections;

public class HeartStopper : MonoBehaviour, Notify {




	//IF the target is already poisoned, it channels that poison into percentage of max life damage

	// Use this for initialization
	void Start () {

		this.gameObject.GetComponent<IWeapon> ().triggers.Add (this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}





	public void trigger(GameObject source,GameObject proj, GameObject target, float damage)
		{
		Poison targetPois = target.GetComponent<Poison> ();
		if (targetPois) {
			UnitStats stats = target.GetComponent<UnitStats> ();
			stats.TakeDamage (targetPois.remainingPoison * .01f* stats.Maxhealth, this.gameObject, DamageTypes.DamageType.True);

			Debug.Log ("Did Pulse Damage " + targetPois.remainingPoison * .01f * stats.Maxhealth);

			Destroy (targetPois);
		
		} else {
			target.AddComponent<Poison>();
			target.GetComponent<Poison>().remainingPoison = 12;
		}

	}
}
