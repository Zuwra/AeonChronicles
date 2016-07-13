
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	public class MiningSawDamager : MonoBehaviour {


		private List<UnitStats> enemies = new List<UnitStats> ();

		public int Owner;
		public GameObject cutEffect;
		public GameObject impactEffect;
		private float nextAction;
		public float damage = 5;
		public float turretRatio = .2f;

		// Use this for initialization
		void Start () {
			nextAction = Time.time;
		}

		// Update is called once per frame
		void Update () {

			if (Time.time > nextAction) {
				nextAction += .2f;

				if (enemies.Count > 0) {
	
					enemies.RemoveAll (item => item == null);
					foreach (UnitStats s in enemies) {

					if (s.isUnitType (UnitTypes.UnitTypeTag.Turret)) {
						s.TakeDamage (damage * (turretRatio), this.gameObject.gameObject.gameObject, DamageTypes.DamageType.Regular);
					} else {

						s.TakeDamage (damage, this.gameObject.gameObject.gameObject, DamageTypes.DamageType.Regular);
					}
					if (cutEffect) {
						Instantiate (cutEffect, getImpactLocation (), Quaternion.identity);
					}
					//obj.transform.SetParent (this.gameObject.transform);
					}
				}

			}

		}


		public void turnOn()
		{
			GetComponent<BoxCollider> ().enabled = true;
			this.enabled = true;
		}


		public void setOwner(int n)
		{Owner = n;
		}

	public Vector3 getImpactLocation()
	{
		Vector3 vec = this.transform.position;
		vec.y = 1;
		return vec;

	}

		void OnTriggerEnter(Collider other)
		{

		if (other.name == "Ground") {
			Instantiate (impactEffect, getImpactLocation(), Quaternion.identity);
		}
			UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
			if (manage == null) {
				return;
			}

			if (manage.PlayerOwner != Owner) {
				enemies.Add (manage.myStats);

				return;
			}


		}


		void OnTriggerExit(Collider other)
		{UnitManager manage = other.gameObject.GetComponent<UnitManager> ();


			if (manage == null) {
				return;
			}

			if (manage.PlayerOwner == Owner) {
				return;
			}

			if (enemies.Contains (manage.myStats)) {
				enemies.Remove (manage.myStats);
			}
		}


	}
