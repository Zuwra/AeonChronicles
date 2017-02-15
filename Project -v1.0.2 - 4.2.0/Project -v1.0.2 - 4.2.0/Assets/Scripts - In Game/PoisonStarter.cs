using UnityEngine;
using System.Collections;

public class PoisonStarter : MonoBehaviour, Notify{

	public float damageAmount;
	public GameObject poisonEffect;
	public float damageRate;


	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Projectile> ().triggers.Add (this);

	
	}
	

	public float trigger(GameObject source,GameObject proj, UnitManager target, float damage)
	{
		Poison enemyPois = target.GetComponent<Poison> ();
	if (enemyPois == null) {

			enemyPois = target.gameObject.AddComponent<Poison> ();

			enemyPois.startPoison(poisonEffect);

			enemyPois.remainingPoison = damageAmount;
			enemyPois.damageRate = damageRate;

		} else {
		

			enemyPois.AddPoisonStack();
			}

		return damage;
		}


}
