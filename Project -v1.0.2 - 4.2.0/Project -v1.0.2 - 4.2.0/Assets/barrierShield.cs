using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class barrierShield : MonoBehaviour {


	public float Health;
	public float duration;
	public float TotalHealth;

	public GameObject Effect;
	public float DecayRate;

	float TotalAbsorbed;

	private float radius;
	public Slider cooldownSlider;
	// Use this for initialization
	void Start () {
		TotalHealth = Health;
		DecayRate = (Health / duration) / 10;
		radius = GetComponent<SphereCollider> ().radius;
		StartCoroutine (RunTime (duration));

	}
		

	IEnumerator RunTime(float dur)
	{
		GetComponent<Collider> ().enabled = false;
		yield return new WaitForSeconds (1);
		GetComponent<Collider> ().enabled = true;

		while (Health > 0) {
			yield return new WaitForSeconds (.1f);
			Health -= DecayRate; 
			cooldownSlider.value = Health / TotalHealth;

		}
		cooldownSlider.gameObject.SetActive (false);
		GetComponent<Animator> ().SetInteger ("State", 1);
		yield return new WaitForSeconds (1.1f);
		GetComponent<Collider> ().enabled = false;
		yield return new WaitForSeconds (1.9f);

		PlayerPrefs.SetInt ("TotalBarrierBlocked", PlayerPrefs.GetInt("TotalBarrierBlocked") +  (int)TotalAbsorbed);
		Destroy (this.gameObject);
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {

		
			Projectile proj = other.GetComponent<Projectile> ();
			if (proj.sourceInt != 1) {

				float dist = Vector3.Distance (this.gameObject.transform.position, other.transform.position);

				if (dist > radius - 5 && dist < radius + 5) {
					
					Health -= proj.damage;
					TotalAbsorbed += proj.damage;
					Instantiate (Effect, other.gameObject.transform.position, other.gameObject.transform.rotation);
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
					TotalAbsorbed += proj.damage;
					Instantiate (Effect, other.gameObject.transform.position,  other.gameObject.transform.rotation);
					proj.selfDestruct ();

					if (Health <= 0) {
						StartCoroutine (RunTime (0));
					}

				}



			}
		}


	}


}
