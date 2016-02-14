using UnityEngine;
using System.Collections;

public class CooledJacket : Upgrade {


	public float damageAmount;

	override
	public void applyUpgrade (GameObject obj){


		GatlingGun wep = obj.GetComponent<GatlingGun> ();
		if(wep){


			wep.totalHeat = 3;

		}
	}



}
