using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CostBox : MonoBehaviour {


	public Text MyName;
	public Text time;
	public Text resOne;
	public Text resTwo;
	public Text health;
	public Text description;
	public Text requirements;
	public Image clocker;
	public Image BloodDrop;
	public Text Population;
	public Image EnergyPic;
	public Text EnergyText;
	public Image OreIcon;

	Color teal = new Color(.698f, .949f, 255);


	public void setText(Ability input)
	{if (input == null) {
			return;}
		continueOrder order = input.canActivate (false);
		
		MyName.text = input.Name;

	
		if (input.myCost) {
			// CLOCK ===========================================

			if (input is UnitProduction) {
				
				clocker.enabled = true;
				time.text = "" + ((UnitProduction)input).buildTime;
			}

			else if (input.myCost.cooldown == 0) {
				clocker.enabled = false;
				time.text = "";
			} else {
				if (order.reasonList.Contains (continueOrder.reason.cooldown)) {
					time.color = Color.red;

				} else {
					
					time.color =teal;
				}
				clocker.enabled = true;
				time.text = "" + input.myCost.cooldown;
			}
			if (input is UnitProduction && ((UnitProduction)input).unitToBuild) {
				UnitStats mwertqert = ((UnitProduction)input).unitToBuild.GetComponent<UnitStats> ();

				float sup =	mwertqert.supply;
				if (sup > 0) {
					
						Population.text = "Pop: " + sup;

				} else {
					Population.text = "";
				}
			} else {
				Population.text = "";
			}


			if (input.myCost.ResourceOne > 0) {
				resOne.text = "" + (int)input.myCost.ResourceOne;
				OreIcon.enabled =true;
				if (order.reasonList.Contains (continueOrder.reason.resourceOne)) {
					resOne.color = Color.red;
				} else {
					resOne.color = teal;
				}


			} else {
			//	Debug.Log ("Disabling");
				OreIcon.enabled = false;
				resOne.text = "";
			}




			if (input.RequiredUnit.Count > 0) {
				string s = "Req: ";
				foreach (string n in input.RequiredUnit) {
					if (!s.Equals ("Req: ")) {
						s += ", ";
					}
					s += n;
				}
				requirements.text = s;
				if (order.reasonList.Contains (continueOrder.reason.requirement)) {
					requirements.color = Color.red;
				} else {
					requirements.color = teal;
				}



			} else {
				requirements.text = "";
			}

			if (input.myCost.energy > 0) {
				EnergyPic.enabled = true;
				EnergyText.text = "" + input.myCost.energy;

			} else {
				EnergyPic.enabled = false;
				EnergyText.text = "" ;
			}

			if (input.myCost.ResourceTwo > 0) {

				if (order.reasonList.Contains (continueOrder.reason.resourceTwo)) {
					resTwo.color = Color.red;
				} else {
					resTwo.color = teal;
				}

				resTwo.text = "Gas: " + (int)input.myCost.ResourceTwo;
			} else {
				resTwo.text = "";
			}

			if (input.myCost.health > 0) {
				health.text = ""+ input.myCost.health;
				BloodDrop.enabled = true;
				if (order.reasonList.Contains (continueOrder.reason.health)) {
					health.color = Color.red;
				} else {
					health.color = teal;
				}

			} else {
				BloodDrop.enabled = false;
				health.text = "";
			}
		} else {
			Population.text = "";
			EnergyText.text = "";
			EnergyPic.enabled = false;
			time.text = "";
			resOne.text = "";
			resTwo.text = "";
			health.text = "";
			requirements.text = "";
			clocker.enabled = false;
			BloodDrop.enabled = false;
			OreIcon.enabled = false;
		}
		description.text = input.Descripton;


	}




}
