using UnityEngine;
using System.Collections;

public class DamageUpgrade : Upgrade {


	public float damageAmount;

	override
	public void applyUpgrade (GameObject obj){

	
		IWeapon wep = obj.GetComponent<IWeapon> ();
		if(wep){
			
		
				obj.GetComponent<IWeapon> ().baseDamage += damageAmount;


		}
	}



}
