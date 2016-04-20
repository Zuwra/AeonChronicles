using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MassCapture : MonoBehaviour {

	public List<CapturableUnit> captures = new List<CapturableUnit>();



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

		if (manage) {
			if (manage.PlayerOwner == 1) {
				foreach (CapturableUnit u in captures) {
					if (u != null) {
						u.capture ();
					}
				}
			}
		}
	}

}
