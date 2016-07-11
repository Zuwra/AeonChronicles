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


	public void trigger(GameObject source,GameObject proj, GameObject target, float damage)
	{
		HalfLife enemyPois = target.GetComponent<HalfLife> ();
		if (enemyPois == null) {

			target.AddComponent<HalfLife> ();
			enemyPois = target.GetComponent<HalfLife> ();
			enemyPois.startPoison(poisonEffect);


		} else {


			enemyPois.AddPoisonStack();
		}
	}


}
