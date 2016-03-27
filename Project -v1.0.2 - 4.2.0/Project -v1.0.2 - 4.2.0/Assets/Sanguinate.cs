using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Sanguinate : MonoBehaviour {

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
			if (manage.PlayerOwner != playerNumber && manage.PlayerOwner != 3) {
				if (other.gameObject.GetComponent<SanguinAura> () == null) {
					other.gameObject.AddComponent<SanguinAura> ();
				} 
					
					other.GetComponent<SanguinAura> ().Initialize (source, myEffect);


			}


		}

	}






}

