using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class barrierShield : MonoBehaviour {


	public float Health;
	public float duration;

	public GameObject Effect;

	private float radius;
	// Use this for initialization
	void Start () {
		radius = GetComponent<SphereCollider> ().radius;
		StartCoroutine (RunTime (duration));

	}
		

	IEnumerator RunTime(float dur)
	{
		yield return new WaitForSeconds (dur -2);
		GetComponent<Animator> ().SetInteger ("State", 1);
		yield return new WaitForSeconds (3);
		Destroy (this.gameObject);
	
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {

			Debug.Log ("Entering " + other.gameObject);
			Projectile proj = other.GetComponent<Projectile> ();
			if (proj.sourceInt != 1) {
				Debug.Log ("other player " + other.gameObject);
				float dist = Vector3.Distance (this.gameObject.transform.position, other.transform.position);

				if (dist > radius - 5 && dist < radius + 5) {
					Debug.Log ("In range");
					Health -= proj.damage;
					Instantiate (Effect, other.gameObject.transform.position, Quaternion.identity);
					proj.selfDestruct ();



					if (Health <= 0) {
						StartCoroutine (RunTime (0));
					}
				
				}



			}
		}


	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {


			Projectile proj = other.GetComponent<Projectile> ();

			if (proj.sourceInt != 1) {
				
				float dist = Vector3.Distance (this.gameObject.transform.position, other.transform.position);

				if (dist > radius - 5 && dist < radius + 5) {
					Health -= proj.damage;
					Instantiate (Effect, other.gameObject.transform.position, Quaternion.identity);
					proj.selfDestruct ();

					if (Health <= 0) {
						StartCoroutine (RunTime (0));
					}

				}



			}
		}


	}


}
