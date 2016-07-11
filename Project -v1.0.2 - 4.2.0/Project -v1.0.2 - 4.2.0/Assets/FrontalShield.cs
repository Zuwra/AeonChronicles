﻿using UnityEngine;
using System.Collections;

public class FrontalShield : Ability,Modifier {

	[Tooltip("between -90 and 90. 0 is to the side.")]
	public float frontAngle;
	public float FrontArmorAmount;
	public UnitStats myStats;


	public GameObject shieldEffect;


	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.passive;
	}


	// Use this for initialization
	void Start () {
		myStats = GetComponent<UnitStats> ();
		myStats.addModifier (this);

		//This makes it so all childed turrets get their incoming damage reduced by the tanks shields. 
		foreach (IWeapon obj in GetComponent<UnitManager>().myWeapon) {

			obj.gameObject.GetComponent<UnitManager> ().myStats.addModifier (this);

		}


	}

	// Update is called once per frame
	void Update () {

	}


	public float modify(float amount, GameObject src)
	{

		Vector3 direction = src.transform.position - this.gameObject.transform.position;
		Debug.Log ("Angle " + Vector3.Dot (direction, this.transform.forward));
		if (Vector3.Dot (direction, this.transform.forward) > frontAngle) {

			Debug.Log ("In front of");
			amount -= FrontArmorAmount;
			if (amount < 1) {
				amount = 1;
			}
			if (shieldEffect) {
				GameObject obj = (GameObject)Instantiate (shieldEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
				obj.transform.SetParent (this.gameObject.transform);
			}


		}


		return amount;
	}



	public override void setAutoCast(){
	}


	override
	public continueOrder canActivate (bool showError)
	{

		continueOrder order = new continueOrder ();
		return order;
	}

	override
	public void Activate()
	{
		//return true;//next unit should also do this.
	}

}
