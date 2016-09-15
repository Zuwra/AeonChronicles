﻿using UnityEngine;
using System.Collections;

public class inflectionStarter: MonoBehaviour, Notify{

	public GameObject barrier;
	void Start () {
		GetComponent<Projectile> ().triggers.Add (this);
	}



	public void trigger(GameObject source,GameObject proj, GameObject target, float damage)
	{
		inflectionBarrier existingShield = target.GetComponentInChildren<inflectionBarrier> ();
		if (existingShield) {
			Destroy (existingShield.gameObject);
		}
		GameObject obj = (GameObject)Instantiate (barrier, target.transform.position, target.transform.rotation);
		obj.transform.SetParent (target.transform);
		obj.GetComponent<inflectionBarrier> ().setSource (GetComponent<Projectile> ().Source);

	}


}
