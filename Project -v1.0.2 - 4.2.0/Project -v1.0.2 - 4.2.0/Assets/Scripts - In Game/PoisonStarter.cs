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
	
	// Update is called once per frame
	void Update () {
	
	}


	public void trigger(GameObject source,GameObject proj, GameObject target, float damage)
	{
		Poison enemyPois = target.GetComponent<Poison> ();
	if (enemyPois == null) {

			enemyPois = target.AddComponent<Poison> ();

			enemyPois.startPoison(poisonEffect);

			enemyPois.remainingPoison = damageAmount;
			enemyPois.damageRate = damageRate;

		} else {
		

			enemyPois.AddPoisonStack();
			}
		}


}
