using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResearchUpgrade:  Ability, Upgradable{

		private Selected mySelect;
		private bool researching;
		private float timer =0;

		private int currentUpgrade = 0;


		public List<Upgrade> upgrades;

		public float buildTime;
		// Use this for initialization
		void Start () {
			mySelect = GetComponent<Selected> ();


		}

		// Update is called once per frame
		void Update () {

			if (researching) {
			

				timer -= Time.deltaTime;
				mySelect.updateCoolDown (1 - timer/buildTime);
				if(timer <=0)
				{mySelect.updateCoolDown (0);
					
				researching = false;
				GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.addUpgrade (upgrades[currentUpgrade], GetComponent<UnitManager>().UnitName);

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

	
		return order;
		}

		override
		public void Activate()
		{
			if (myCost.canActivate (this)) {
			timer = buildTime;
				myCost.payCost();
			foreach (Transform obj in this.transform) {

				obj.SendMessage ("ActivateAnimation",SendMessageOptions.DontRequireReceiver);
			}
				researching = true;

				//return false;
			}
			//return true;//next unit should also do this.
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
				this.gameObject.GetComponent<UnitManager> ().removeAbility (this);

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




}
