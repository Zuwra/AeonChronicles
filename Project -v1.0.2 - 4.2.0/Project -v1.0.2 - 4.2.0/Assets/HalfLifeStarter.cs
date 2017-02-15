using UnityEngine;
using System.Collections;

public class HalfLifeStarter : MonoBehaviour, Notify{

	public GameObject poisonEffect;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Projectile> ().triggers.Add (this);


	}

	// Update is called once per frame
	void Update () {

	}


	public float trigger(GameObject source,GameObject proj, UnitManager target, float damage)
	{
		Poison enemyPois = target.GetComponent<Poison> ();
		if (enemyPois == null) {

			enemyPois = target.gameObject.AddComponent<Poison> ();
		
			enemyPois.startPoison (poisonEffect);
		}
			UnitStats theirStats = target.gameObject.GetComponent<UnitStats> ();
		float damageAmount =  Mathf.Max(50,theirStats.Maxhealth /2);

			enemyPois.remainingPoison = damageAmount;
			enemyPois.damageRate = damageAmount / 6;
			enemyPois.setEnergyDrain (theirStats.currentEnergy / 12);
		return damage;
	}


}
