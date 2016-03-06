using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretPlacerManager : MonoBehaviour {


	public List<TurretMount> mounts = new List<TurretMount>();
	public TurretMountTwo two;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public void deactivate()
	{
		foreach (TurretMount x in mounts) {
			if (x.hasDisplayer != null) {
				x.hasDisplayer.ToggleOn ();
			}
		}

		if (two.hasDisplayer != null) {
			two.hasDisplayer.ToggleOn ();
		}
	}



}
