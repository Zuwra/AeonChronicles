using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillage : MonoBehaviour , Notify {


	public float moneyPerAttack;
	public bool showPopup = true;


	void Start () {
		this.GetComponent<IWeapon> ().triggers.Add (this);
	}




	public float trigger(GameObject source, GameObject projectile,UnitManager target, float damage)
	{
		if (!target.myStats.isUnitType (UnitTypes.UnitTypeTag.Invulnerable)) {
			//myStats.heal (damage * percentage);
			if (showPopup) {
				PopUpMaker.CreateGlobalPopUp ("+" + +moneyPerAttack, Color.white, this.gameObject.transform.position);
			}
			GameManager.main.activePlayer.updateResources (moneyPerAttack, 0, true);
		}
		//popper.CreatePopUp ("+" + (int)(damage * percentage), Color.gray); 
		return damage;
	}


}