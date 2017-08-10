using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyObjective : Objective {

	public Slider moneySlide;
	public RaceManager myRace;
	public Text myText;
	public List<int> halfwayVoiceLines;

	public float moneyVictory;

	bool playedHalfWay;

	// Use this for initialization
	new void Start () {
		base.Start ();
		InvokeRepeating ("UpdateMoney", .75f, .7f);
	}
	
	// Update is called once per frame
	void UpdateMoney () {

		myText.text = myRace.ResourceOne +"/"+ moneyVictory;
		moneySlide.value = myRace.ResourceOne / moneyVictory;

		if (!playedHalfWay && myRace.ResourceOne > moneyVictory / 2) {
			playedHalfWay = true;
			if (halfwayVoiceLines.Count > 0) {
				dialogManager.instance.playLine (UnityEngine.Random.Range(0,halfwayVoiceLines.Count));
			}
		}

		if (myRace.ResourceOne >= moneyVictory) {
			complete ();
		}

	}
}
