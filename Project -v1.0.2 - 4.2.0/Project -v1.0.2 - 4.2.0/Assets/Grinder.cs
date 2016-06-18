using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grinder : MonoBehaviour {


	private List<UnitStats> enemies = new List<UnitStats> ();
	private List<UnitStats> terrain = new List<UnitStats> ();
	public int Owner;

	private float nextAction;
	public float damage = 5;
	public float massReduction = 3;

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
				
				
					s.TakeDamage (damage * (massReduction), this.gameObject.gameObject.gameObject, DamageTypes.DamageType.True);
			
				}
			}
			if (terrain.Count > 0) {
				terrain.RemoveAll (item => item == null);
				foreach (UnitStats t in terrain) {

					t.TakeDamage (8, this.gameObject.gameObject.gameObject, DamageTypes.DamageType.True);

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

	void OnTriggerEnter(Collider other)
	{
		UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
		if (manage == null) {
			return;
		}

		if (manage.PlayerOwner == Owner) {
			return;
		}

		if (manage.myStats.isUnitType (UnitTypes.UnitTypeTag.Destructable_Terrain)) {
			terrain.Add (manage.myStats);
		
		} 
			enemies.Add (manage.myStats);

			


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
