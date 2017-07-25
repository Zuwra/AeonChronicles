using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyObjective : Objective {

	public Slider moneySlide;
	public RaceManager myRace;
	public Text myText;

	public float moneyVictory;
	// Use this for initialization
	void Start () {
		base.Start ();
		InvokeRepeating ("UpdateMoney", .75f, .7f);
	}
	
	// Update is called once per frame
	void UpdateMoney () {

		myText.text = myRace.ResourceOne +"/"+ moneyVictory;
		moneySlide.value = myRace.ResourceOne / moneyVictory;
		if (myRace.ResourceOne >= moneyVictory) {
			complete ();
		}

	}
}
