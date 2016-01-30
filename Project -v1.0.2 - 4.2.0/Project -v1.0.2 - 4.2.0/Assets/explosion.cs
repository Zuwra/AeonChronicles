using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class explosion : MonoBehaviour {


	public GameObject source;

	public bool friendlyFire;
	public float damageAmount;
	public DamageTypes.DamageType type;
	public float maxSize= 5.0f;
	public float growthRate = 1.0f;
	private float scale = 1.0f;

	public List<Notify> triggers = new List<Notify> ();


	// Use this for initialization
	void Start () {
	
	}




	// Update is called once per frame
	void Update () {



		transform.localScale = Vector3.one * scale;
		scale += growthRate * Time.deltaTime;
		if (scale > maxSize) Destroy (gameObject);

	
	}



	void OnTriggerEnter(Collider other)
		{if (!other.isTrigger) {
			UnitManager manager = other.gameObject.GetComponent<UnitManager> ();

			if (manager) {

				if (friendlyFire || source.GetComponent<UnitManager> ().PlayerOwner != manager.PlayerOwner) {
					other.gameObject.GetComponent<UnitStats> ().TakeDamage (damageAmount, source, type);

					foreach (Notify not in triggers) {
					
						not.trigger(source,  null, other.gameObject);
					}
				}

			}
		}
	}
}
