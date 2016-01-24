﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitCardCreater : MonoBehaviour {

	public Image unitIcon;
	public Text UnitName;
	public Text health;
	public Text armor;
	public Text Mass;
	public Text speed;

	public Text damage;
	public Text attackSpeed;
	public Text range;





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public void CreateCard(RTSObject obj)
	{
		UnitManager manager = obj.gameObject.GetComponent<UnitManager> ();

		unitIcon.material = manager.myStats.Icon;
		UnitName.text = manager.UnitName;
		health.text = manager.myStats.health + "/" + manager.myStats.Maxhealth;
		armor.text =  "" +manager.myStats.armor;
		Mass.text = "" + manager.myStats.mass;

		if (manager.cMover != null) {
			speed.text = "" + manager.cMover.MaxSpeed;
		} else {speed.text = "";
		}


		if (manager.myWeapon != null) {
			damage.text = "" + manager.myWeapon.baseDamage;
			attackSpeed.text = "" + manager.myWeapon.attackPeriod;
			range.text = "" + manager.myWeapon.range;
			if (manager.myWeapon.numOfAttacks > 1) {
				damage.text = "" + manager.myWeapon.baseDamage + " (X" + manager.myWeapon.numOfAttacks +")";
			}
		
		} else {
			damage.text = "" ;
			attackSpeed.text = "";
			range.text = "" ;
		}

	}



}
