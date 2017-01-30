using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeParts : MonoBehaviour {


	public List<Rigidbody> myParts;

	// Use this for initialization
	void Start () {
		foreach (Rigidbody rb in myParts) {

			rb.AddExplosionForce (Random.Range(200,500), this.gameObject.transform.position + Vector3.down ,30);

		}
		
	}
	

}
