using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResearchUpgrade:  Ability, Upgradable{

		private Selected mySelect;
		private bool researching;
		private float timer =0;
		private bool buildingUnit = false;
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
					buildingUnit = false;
				researching = false;
				GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer.addUpgrade (upgrades[currentUpgrade], GetComponent<UnitManager>().UnitName);

				RaceManager.upDateUI ();
					//createUnit();
				}
			}

		}




		override
		public bool canActivate ()
		{
			if (researching) {

				return false;}

			return myCost.canActivate ();

		}

		override
		public bool Activate()
		{
			if (myCost.canActivate ()) {
			timer = buildTime;
				myCost.payCost();

				researching = true;

				return false;
			}
			return true;//next unit should also do this.
		}

		public override void setAutoCast(){}



	public void researched (Upgrade otherUpgrade)
	{

		if (upgrades [currentUpgrade].GetType () == otherUpgrade.GetType ()) {
			if (upgrades.Count > currentUpgrade + 1) {
				currentUpgrade++;

				iconPic = upgrades [currentUpgrade].iconPic;
				Name = upgrades [currentUpgrade].Name;
				myCost = upgrades [currentUpgrade].myCost;
				Descripton = upgrades [currentUpgrade].Descripton;
			} else {
				this.gameObject.GetComponent<UnitManager> ().removeAbility (this);

				foreach (Upgrade up in upgrades) {

					if (up.myCost != null)
						Destroy (up.myCost);
					Destroy (up);
				}
				Destroy (this);
			}
		}
	}




}
