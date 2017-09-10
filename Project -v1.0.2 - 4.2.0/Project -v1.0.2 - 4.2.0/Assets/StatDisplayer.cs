using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplayer : MonoBehaviour {

	public GameObject sourceUnit;
	public Text myText;
	public Text secondText;
	UnitManager manager;
	UnitStats stats;
	IMover mover;

	// Used inthe campaign menu to show a units stats before and after upgrades are applied.
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		manager = sourceUnit.GetComponent<UnitManager> ();
		stats = sourceUnit.GetComponent<UnitStats> ();
		mover = sourceUnit.GetComponent < IMover> ();

		SetText ();


	}



	public void SetText()
	{
		myText.text =  "Health: " + stats.Maxhealth + "\n";
		if (stats.MaxEnergy > 0) {
			myText.text += "Energy: " + stats.MaxEnergy+ "\n";
		}
		if (stats.armor > 0) {
			myText.text += "Armor: " + stats.armor+ "\n";
		}
		if (mover) {
			myText.text += "Speed: " + mover.MaxSpeed+ "\n";
		}
		myText.text += "\n";

		secondText.text = "";
		foreach (IWeapon weap in sourceUnit.GetComponents<IWeapon>()) {
			secondText.text += "Weapon: " + weap.Title + "\n";
			secondText.text += "Damage: " + weap.baseDamage;
			foreach (IWeapon.bonusDamage bonus in weap.extraDamage) {
				secondText.text += "(+ "+ bonus.bonus + "vs" + bonus.type +" )";
			}
			if (weap.numOfAttacks > 1) {
				secondText.text += " (X" + weap.numOfAttacks+")";
			}
			secondText.text += "\n";
			secondText.text += "Range: " + weap.range + "   ";
			if (weap.cantAttackTypes.Contains (UnitTypes.UnitTypeTag.Air)) {
				secondText.text += "(Ground)\n";
			} else if (weap.cantAttackTypes.Contains (UnitTypes.UnitTypeTag.Ground)) {
				secondText.text += "(Air)\n";
			} else {
				secondText.text += "(Ground & Air)\n";
			}

			secondText.text += "Attack Period: " + weap.attackPeriod +"\n";
		}


	}






}
