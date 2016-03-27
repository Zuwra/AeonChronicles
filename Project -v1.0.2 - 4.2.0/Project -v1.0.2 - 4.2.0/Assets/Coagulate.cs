using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Coagulate : MonoBehaviour {

	private GameObject source;
	private int playerNumber;
	public GameObject myEffect;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void setSource(GameObject obj)
	{
		source = obj;
		playerNumber = source.GetComponent<UnitManager> ().PlayerOwner;

	}


	void OnTriggerEnter(Collider other)
	{
		UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
		if (manage) {
			if (manage.PlayerOwner != playerNumber) {
				if (other.gameObject.GetComponent<CoagulateAura> () == null) { 
					other.gameObject.AddComponent<CoagulateAura> ();
				} 
				if (other.GetComponent<CoagulateAura> ()) {
					other.GetComponent<CoagulateAura> ().Initialize (source, myEffect);

				}
			}


		}

	}






}

