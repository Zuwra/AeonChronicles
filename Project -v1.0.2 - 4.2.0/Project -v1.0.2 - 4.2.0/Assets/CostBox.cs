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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setText(Ability input)
	{MyName.text = input.Name;
		if (input.myCost) {
			if (input.myCost.cooldown == 0) {
				clocker.enabled = false;
				time.text = "";
			} else {
				clocker.enabled = true;
				time.text = "" + input.myCost.cooldown;
			}

			if (input.myCost.ResourceOne > 0) {
				resOne.text = "Ore: " + input.myCost.ResourceOne;
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
			} else {
				requirements.text = "";
			}

			if (input.myCost.ResourceTwo > 0) {
				resTwo.text = "Gas: " + input.myCost.ResourceTwo;
			} else {
				resTwo.text = "";
			}

			if (input.myCost.health > 0) {
				health.text = "Health: " + input.myCost.health;
			} else {
				health.text = "";
			}
		} else {
			time.text = "";
			resOne.text = "";
			resTwo.text = "";
			requirements.text = "";
			clocker.enabled = false;
		}
		description.text = input.Descripton;


	}




}
