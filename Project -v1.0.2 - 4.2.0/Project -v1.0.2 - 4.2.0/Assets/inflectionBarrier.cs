using UnityEngine;
using System.Collections;

public class inflectionBarrier : MonoBehaviour, Notify {


	public float Health;
	public float duration;

	public GameObject Effect;
	public GameObject source;

	UnitManager attachedUnit;

	public void setSource(GameObject o)
	{source = o;}

	//private float radius;
	// Use this for initialization
	void Start () {

		Instantiate (Effect, this.gameObject.transform.position, Quaternion.identity);
		//radius = GetComponent<SphereCollider> ().radius;

	}

	// Update is called once per frame
	void Update () {

		duration -= Time.deltaTime;
		if (duration <= 0) {
			foreach (IWeapon weap in  attachedUnit.myWeapon) {
				weap.removeNotifyTrigger (this);
			}
			Destroy (this.gameObject);
		}

	}

	public void initialize (UnitManager attachUnit)
	{
		attachedUnit = attachUnit;
		foreach (IWeapon weap in  attachedUnit.myWeapon) {
			weap.addNotifyTrigger (this);
		
		}

	}


	public float trigger(GameObject source, GameObject projectile,UnitManager target, float damage)
	{
		//Debug.Log ("Triggering");

		Health -= damage;

		Vector3 direction = source.transform.position +  (target.gameObject.transform.position - source.transform.position).normalized *9;
		if (projectile) {

			Projectile proj = projectile.GetComponent<Projectile> ();
			if (proj) {
				proj.Despawn ();
				if (proj.explosionO) {



					//GameObject explode = (GameObject)Instantiate (proj.explosionO , transform.transform.position, Quaternion.identity);
					//Debug.Log ("INstantiating explosion");

					attachedUnit.myStats.TakeDamage (damage,source,DamageTypes.DamageType.Regular);
					explosion Escript =proj.explosionO.GetComponent<explosion> ();
					if (Escript) {
						
						Instantiate (Escript.particleEff , direction, Quaternion.identity);
					}



				}


			}
		} 

	
		Instantiate (Effect, direction, Quaternion.identity);


		if (Health <= 0) {
			foreach (IWeapon weap in  attachedUnit.myWeapon) {
				weap.removeNotifyTrigger (this);
			}
			if(this && this.gameObject)
			Destroy (this.gameObject);
		}
		return 0;
	}
	/*
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {


			Projectile proj = other.GetComponent<Projectile> ();
			if (proj.Source.GetComponent<UnitManager> ().PlayerOwner != 1) {

				//float dist = Vector3.Distance (this.gameObject.transform.position, other.transform.position);

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
*/


}
