using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretPlacerManager : MonoBehaviour {


	public List<TurretMount> mounts = new List<TurretMount>();
	public bool centerOn;


	public void deactivate(bool offOn)
	{centerOn = offOn;
		
		foreach (TurretMount x in mounts) {
			if (x.hasDisplayer != null) {
				x.hasDisplayer.ToggleOn (offOn);//Debug.Log ("Flipping switches");
			}
		}
			
	}



}
