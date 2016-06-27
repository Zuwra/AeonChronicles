﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class explosion : MonoBehaviour {


	public GameObject source;
	public GameObject particleEff;
	public bool friendlyFire;
	public float damageAmount;
	public DamageTypes.DamageType type;
	public float maxSize= 5.0f;
	public float growthRate = 1.0f;
	private float scale = 1.0f;

	public List<Notify> triggers = new List<Notify> ();

	private List<GameObject> hitStuff= new List<GameObject>();

	public IWeapon.bonusDamage[] extraDamage;
	// Use this for initialization
	void Start () {
		if (particleEff) {
			Instantiate (particleEff, this.gameObject.transform.position, Quaternion.identity);
		}

	}




	// Update is called once per frame
	void Update () {



		transform.localScale = Vector3.one * scale;
		scale += growthRate * Time.deltaTime;
		if (scale > maxSize) Destroy (gameObject);

	
	}



	void OnTriggerEnter(Collider other)
	{if (!other.isTrigger && !hitStuff.Contains(other.gameObject)) {
			hitStuff.Add (other.gameObject);
			UnitManager manager = other.gameObject.GetComponent<UnitManager> ();

			if (manager) {

				if (friendlyFire || source.GetComponent<UnitManager> ().PlayerOwner != manager.PlayerOwner) {

					float amount = damageAmount	;
					UnitStats stats = other.gameObject.GetComponent<UnitStats> ();
					foreach ( IWeapon.bonusDamage tag in extraDamage) {
						if ( stats.isUnitType (tag.type)) {
							amount += tag.bonus;
						}
					}
					stats.TakeDamage (amount, source, type);
				
					foreach (Notify not in triggers) {
					
						not.trigger(source,  null, other.gameObject, amount);
					}
				}

			}
		}
	}
}
