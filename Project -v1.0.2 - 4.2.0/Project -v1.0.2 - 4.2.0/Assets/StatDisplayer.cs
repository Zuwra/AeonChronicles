using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplayer : MonoBehaviour {

	public GameObject sourceUnit;
	public Text myText;
	public Text secondText;
	UnitStats stats;
	IMover mover;
	public List<CampaignUpgrade> myCampaigns;

	// Used inthe campaign menu to show a units stats before and after upgrades are applied.
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();

		stats = sourceUnit.GetComponent<UnitStats> ();
		mover = sourceUnit.GetComponent < IMover> ();

		SetText ();


	}




	public void SetText()
	{

		foreach (Transform t in myText.transform) {
			Destroy(t.gameObject);
		}

		foreach (Transform t in secondText.transform) {
			Destroy(t.gameObject);
		}

		float f = setValue( stats.Maxhealth,"Health");
		bool b = f != stats.Maxhealth;
		addText ("Health: " + f, myText,b);



		if (stats.MaxEnergy > 0) {
			f =  setValue( stats.MaxEnergy,"Energy");
			b = f != stats.MaxEnergy;
			addText ("Energy: " + f, myText,b);
		}



		if (stats.armor > 0) {
			f =  setValue( stats.armor,"Armor");
			b = f != stats.armor;
			addText ("Armor: " + f, myText,b);
		}

		if (mover) {
			f =  setValue( mover.MaxSpeed,"Speed");
			b = f != mover.MaxSpeed;

			string originalSpeed = "Speed: " + mover.MaxSpeed;
			string newSpeed = addOnText ("Speed", originalSpeed);
			addText (newSpeed, myText,b || (originalSpeed != newSpeed));
		}
			
		foreach (IWeapon weap in sourceUnit.GetComponents<IWeapon>()) {

			addText ("Weapon: " + weap.Title, secondText,false);

			f =  setValue( weap.baseDamage,"Damage");
			b = f != weap.baseDamage;

			string damage = "Damage: " +f;
			foreach (IWeapon.bonusDamage bonus in weap.extraDamage) {
				damage += "(+ "+ bonus.bonus + "vs" + bonus.type +" )";
			}
			string original = damage;
			damage = addOnText ("Damage", damage);

			if (weap.numOfAttacks > 1) {
				original += " (X" + weap.numOfAttacks + ")";
				damage += " (X" + weap.numOfAttacks+")";
			}
			addText (damage, secondText,original != damage);

			f =  setValue( weap.range,"Range");
			b = f != weap.range;
			string range = "Range: " + f;
			if (weap.cantAttackTypes.Contains (UnitTypes.UnitTypeTag.Air)) {
				range += " (Ground)";
			} else if (weap.cantAttackTypes.Contains (UnitTypes.UnitTypeTag.Ground)) {
				range += "  (Air)";
			} else {
				range += "  (Ground & Air)";
			}

			addText (range, secondText,b);

			addText ("Attack Period: " + weap.attackPeriod, secondText,false);
		}


	}

	float setValue(float value, string m)
	{
		foreach (CampaignUpgrade upgrade in myCampaigns) {
			value = upgrade.changeText (m, value);
		}
		return value;
	}

	string addOnText(string m, string ToAddOn)
	{
		foreach (CampaignUpgrade upgrade in myCampaigns) {
			ToAddOn = upgrade.addText (m, ToAddOn);
		}
		return ToAddOn;
	}



	void addText(string s, Text column, bool yellow)
	{
		addTextCo(s,column,yellow);
	}

	void addTextCo(string s, Text column, bool yellow)
	{
		GameObject newChild = new GameObject ();
		newChild.transform.SetParent (column.transform);
		Text textComp = newChild.AddComponent<Text> ();

		((RectTransform)newChild.transform).sizeDelta = new Vector2 (250, 19);
		newChild.transform.localScale = Vector3.one;
		textComp.font = myText.font;
		textComp.text = s;
		textComp.fontStyle = FontStyle.Bold;
		textComp.text = s;
		textComp.fontSize = 16;
		newChild.AddComponent<Shadow> ();
		if (yellow) {
			textComp.color = Color.yellow;
		} else {
			textComp.color = new Color (.8f, 1, 1);
		}
	}






}
