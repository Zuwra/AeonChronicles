using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyMine : MonoBehaviour {


	public int playerOwner;

	Coroutine myroutine;

	GameObject target;
	public float damage =200;
	public GameObject explosion;
	public GameObject RedLine;

	public Collider terrainCollider;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.GetComponent<TerrainCollider> ()) {
			detonate ();
		}
	}



	void OnTriggerEnter(Collider col)
	{
		if (col.isTrigger) {
			return;
		}

		UnitManager manage = col.GetComponent<UnitManager> ();
		if (manage && manage.PlayerOwner != 3 && manage.PlayerOwner != playerOwner) {
			if (myroutine == null) {
				target = col.gameObject;

				BoxCollider box = GetComponent<BoxCollider> ();
				if ( box) {
					box.center = new Vector3 (0, 0, 0);
					box.size = new Vector3 (7, 11, 7);
				} else {
					
					SphereCollider sphere = GetComponent<SphereCollider> ();
					if (sphere && sphere.isTrigger) {
						sphere.radius = 4;

					}
				}

				myroutine = StartCoroutine (attackTarget());
				return;
			
			}
		}

		if (myroutine != null) {
			if (manage || col.gameObject.GetComponent<TerrainCollider>()) {
			//	Debug.Log ("Detonating " + manage + "   " + col);
				detonate ();
			}
		}


	}

	void detonate()
	{
		
		if (this.gameObject) {
			if (explosion) {
				GameObject obj =  (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
				obj.GetComponent<explosion> ().damageAmount = damage;
				obj.GetComponent<explosion> ().sourceInt = playerOwner;
			}

			Destroy (this.gameObject);
		}
	}

	public void attack(GameObject obj)
	{
		if (myroutine == null) {
			target = obj;

			BoxCollider box = GetComponent<BoxCollider> ();
			if ( box) {
				box.center = new Vector3 (0, 0, 0);
				box.size = new Vector3 (7, 11, 7);
			} else {

				SphereCollider sphere = GetComponent<SphereCollider> ();
				if (sphere && sphere.isTrigger) {
					sphere.radius = 4;

				}
			}

			myroutine = StartCoroutine (attackTarget());
			return;

		}
	}

	IEnumerator attackTarget()
	{	


		GetComponent<AudioSource> ().Play ();
		RedLine.SetActive (true);
		float speed = 15;
		for (float i = 0; i < 1.6f; i += Time.deltaTime) {
			if (!target) {
				break;}
			Vector3 turnAmount = target.transform.position - transform.position;

			turnAmount.y = 0;
			Quaternion toTurn = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (turnAmount),.6f);

			if (!float.IsNaN (toTurn.x) && !float.IsNaN (toTurn.y) && !float.IsNaN (toTurn.z) && !float.IsNaN (toTurn.w)) {
				transform.rotation = toTurn;
			}
			speed += Time.deltaTime * 5;
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
			yield return  null;
		}
		Vector3 scale = RedLine.transform.localScale;
		scale.x *= .45f;
		scale.y *= .45f;
		RedLine.transform.localScale = scale;


		for (float i = 0; i < 3.5f; i += Time.deltaTime) {
			speed += Time.deltaTime*15;
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
			yield return null;
		}
		detonate ();
	}

}
