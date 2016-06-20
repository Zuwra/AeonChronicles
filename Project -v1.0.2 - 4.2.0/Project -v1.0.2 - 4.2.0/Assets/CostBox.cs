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

	Color teal = new Color(.698f, .949f, 255);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setText(Ability input)
	{if (input == null) {
			return;}
		continueOrder order = input.canActivate ();
		
		MyName.text = input.Name;

	
		if (input.myCost) {
			// CLOCK ===========================================
			if (input.myCost.cooldown == 0) {
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



			if (input.myCost.ResourceOne > 0) {
				resOne.text = "Ore: " + input.myCost.ResourceOne;

				if (order.reasonList.Contains (continueOrder.reason.resourceOne)) {
					resOne.color = Color.red;
				} else {
					resOne.color = teal;
				}


			} else {
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

			if (input.myCost.ResourceTwo > 0) {

				if (order.reasonList.Contains (continueOrder.reason.resourceTwo)) {
					resTwo.color = Color.red;
				} else {
					resTwo.color = teal;
				}

				resTwo.text = "Gas: " + input.myCost.ResourceTwo;
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
			time.text = "";
			resOne.text = "";
			resTwo.text = "";
			health.text = "";
			requirements.text = "";
			clocker.enabled = false;
			BloodDrop.enabled = false;
		}
		description.text = input.Descripton;


	}




}
