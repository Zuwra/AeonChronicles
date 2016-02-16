﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResearchUpgrade: UnitProduction, Upgradable{

		private Selected mySelect;
		private bool researching;
		private float timer =0;

		private int currentUpgrade = 0;

	private BuildManager buildMan;
		public List<Upgrade> upgrades;

		public float buildTime;
		// Use this for initialization
		void Start () {
			mySelect = GetComponent<Selected> ();
		buildMan = GetComponent<BuildManager> ();

		}

		// Update is called once per frame
		void Update () {

			if (researching) {
			

				timer -= Time.deltaTime;
				mySelect.updateCoolDown (1 - timer/buildTime);
				if(timer <=0)
				{mySelect.updateCoolDown (0);
				buildMan.unitFinished (this);
				researching = false;
				GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.addUpgrade (upgrades[currentUpgrade], GetComponent<UnitManager>().UnitName);
				active = true;
				RaceManager.upDateUI ();
					//createUnit();
				}
			}

		}




		override
	public continueOrder canActivate ()
		{continueOrder order = new continueOrder();

		if (researching || !myCost.canActivate (this)) {
			order.canCast = false;
			order.nextUnitCast = false;
				return order;}
		if (active == false) {
			order.canCast = false;
		}
	
		return order;
		}

		override
		public void Activate()
		{
			if (myCost.canActivate (this)) {

	
			timer = buildTime;
			active = false;
			if (mySelect.IsSelected) {
				RaceManager.updateActivity ();
			}

			myCost.payCost();
			myCost.resetCoolDown ();

			buildMan.buildUnit (this);
		

			}

		}

		public override void setAutoCast(){}



	public void researched (Upgrade otherUpgrade)
	{

		if (upgrades [currentUpgrade].GetType () == otherUpgrade.GetType ()) {
			if (upgrades.Count > currentUpgrade + 1) {
				currentUpgrade++;

				//this is all here for replaceable or scaling upgrades
				iconPic = upgrades [currentUpgrade].iconPic;
                buildTime = upgrades[currentUpgrade].buildTime;
				Name = upgrades [currentUpgrade].Name;
				myCost = upgrades [currentUpgrade].myCost;
				Descripton = upgrades [currentUpgrade].Descripton;
			} else {
				this.gameObject.GetComponent<UnitManager> ().abilityList[GetComponent<UnitManager> ().abilityList.IndexOf(this)] = null;
			//	this.gameObject.GetComponent<UnitManager> ().removeAbility (this);

				foreach (Upgrade up in upgrades) {

				
					Destroy (up);
				}

				foreach (Transform obj in this.transform) {

					obj.SendMessage ("DeactivateAnimation",SendMessageOptions.DontRequireReceiver);
				}

				Destroy (this);
			}
		}
	}


	public override float getProgress ()
	{return (1 - timer/buildTime);}


	public override void startBuilding(){ 
		timer = buildTime;

		myCost.resetCoolDown ();

		foreach (Transform obj in this.transform) {

			obj.SendMessage ("ActivateAnimation",SendMessageOptions.DontRequireReceiver);
		}
		researching = true;
	}

	public override void cancelBuilding(){

		mySelect.updateCoolDown (0);
		timer = 0;
		researching = false;
		myCost.refundCost ();
	}


}
