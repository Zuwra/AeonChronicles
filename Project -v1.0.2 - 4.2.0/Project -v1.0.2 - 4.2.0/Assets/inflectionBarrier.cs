using UnityEngine;
using System.Collections;

public class inflectionBarrier : MonoBehaviour {


	public float Health;
	public float duration;

	public GameObject Effect;
	GameObject source;


	public void setSource(GameObject o)
	{source = o;}

	//private float radius;
	// Use this for initialization
	void Start () {
		//radius = GetComponent<SphereCollider> ().radius;

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

					Health -= proj.damage;
					Instantiate (Effect, other.gameObject.transform.position, Quaternion.identity);
					Destroy (other.gameObject);
				if (proj.explosionO) {
				

				
					GameObject explode = (GameObject)Instantiate (proj.explosionO, transform.transform.position, Quaternion.identity);
						//Debug.Log ("INstantiating explosion");

						explosion Escript = explode.GetComponent<explosion> ();
						if (Escript) {
							explode.GetComponent<explosion> ().source = source;
						explode.GetComponent<explosion> ().damageAmount = proj.damage;
						}



				}

					if (Health <= 0) {
						Destroy (this.gameObject);
					}

				

			}
		}


	}



}
