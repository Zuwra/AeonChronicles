using UnityEngine;
using System.Collections;

public class PoisonStarter : MonoBehaviour, Notify{

	public float damageAmount;
	public GameObject poisonEffect;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Projectile> ().triggers.Add (this);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void trigger(GameObject source,GameObject proj, GameObject target)
	{
		Poison enemyPois = target.GetComponent<Poison> ();
	if (enemyPois == null) {

			target.AddComponent<Poison> ();
			enemyPois = target.GetComponent<Poison> ();
			enemyPois.startPoison(poisonEffect);

			enemyPois.remainingPoison = damageAmount;
			Debug.Log("HERE" + enemyPois.remainingPoison);
		} else {
		

			enemyPois.AddPoisonStack();
			}
		}


}
