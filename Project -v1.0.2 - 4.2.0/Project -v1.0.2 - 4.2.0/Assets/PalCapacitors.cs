using UnityEngine;
using System.Collections;

public class PalCapacitors : Upgrade {


	public int chargeAmount;

	override
	public void applyUpgrade (GameObject obj){


		LaserNode wep = obj.GetComponent<LaserNode> ();
		if(wep){


			obj.GetComponent<LaserNode> ().maxCharges += chargeAmount;


		}
	}



}
