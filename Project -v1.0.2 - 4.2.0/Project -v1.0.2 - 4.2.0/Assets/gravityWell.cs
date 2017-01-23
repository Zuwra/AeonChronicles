using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gravityWell : MonoBehaviour {


	//List<GameObject> SuckingIn = new List<GameObject>();

	//int playerOwner = 1;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {


			Projectile proj = other.GetComponent<Projectile> ();
			if (proj.Source.GetComponent<UnitManager> ().PlayerOwner !=1) {
				Destroy (other.gameObject);
			}
		}


	}



}
