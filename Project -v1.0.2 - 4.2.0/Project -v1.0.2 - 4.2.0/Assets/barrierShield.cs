using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class barrierShield : MonoBehaviour {


	public float Health;
	public float duration;

	public GameObject Effect;

	private Selected select;

	private float radius;
	// Use this for initialization
	void Start () {
		radius = GetComponent<SphereCollider> ().radius;

	}

	// Update is called once per frame
	void Update () {

		duration -= Time.deltaTime;
		if (duration <= 0) {
			Destroy (this.gameObject);
		}
	
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {

		
			Projectile proj = other.GetComponent<Projectile> ();
			if (proj.Source.GetComponent<UnitManager> ().PlayerOwner != 1) {
				
				float dist = Vector3.Distance (this.gameObject.transform.position, other.transform.position);

				if (dist > radius - 5 && dist < radius + 5) {
					Health -= other.gameObject.GetComponent<Projectile> ().damage;
					Instantiate (Effect, other.gameObject.transform.position, Quaternion.identity);
					Destroy (other.gameObject);


					if (Health <= 0) {
						Destroy (this.gameObject);
					}
				
				}



			}
		}


	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {


			Projectile proj = other.GetComponent<Projectile> ();

			if (proj.Source.GetComponent<UnitManager> ().PlayerOwner != 1) {
				
				float dist = Vector3.Distance (this.gameObject.transform.position, other.transform.position);

				if (dist > radius - 5 && dist < radius + 5) {
					Health -= other.gameObject.GetComponent<Projectile> ().damage;
					Instantiate (Effect, other.gameObject.transform.position, Quaternion.identity);
					Destroy (other.gameObject);

					if (Health <= 0) {
						Destroy (this.gameObject);
					}

				}



			}
		}


	}


}
