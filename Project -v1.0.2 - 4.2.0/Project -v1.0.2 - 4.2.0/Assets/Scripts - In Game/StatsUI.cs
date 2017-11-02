using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {


	public GameObject columnOne;
	public GameObject columnTwo;
	public GameObject columnThree;
	public Image damageIcon;
	public Image rangeIcon;
	public Image healthIcon;
	public Image armorIcon;

	private Text OneText;
	private Text TwoText;
	public Text UnitName;
	public Button SelectButton;
	// Use this for initialization
	void Awake () {
		OneText = columnOne.GetComponent<Text> ();
		TwoText = columnTwo.GetComponent<Text> ();
	
	}
	


	public void loadUnit(UnitManager man, int number, string unitName)
		{
		SelectButton.enabled = true;
		healthIcon.enabled = true;
		armorIcon.enabled = true;

		UnitName.text = unitName;
		OneText.text = man.myStats.Maxhealth + "\n" + man.myStats.armor;
		if (man.myWeapon != null) {
			damageIcon.enabled = true;
			rangeIcon.enabled = true;

			TwoText.text += "";
			for (int i = 0; i < man.myWeapon.Count; i++) {
				TwoText.text += man.myWeapon[i].baseDamage;
				if (man.myWeapon [i].baseDamage < 10) {
					TwoText.text += "  ";
				}
				if (i < man.myWeapon.Count - 1) {
					TwoText.text += "/";
				
				}

			}
			TwoText.text += "\n";
			for (int i = 0; i < man.myWeapon.Count; i++) {
				TwoText.text += man.myWeapon[i].range;
				if (i < man.myWeapon.Count - 1) {
					TwoText.text += "/";

				}
			}
		} 
		if (number > 4) {
			UnitName.text += " ( " + number + " )";

		} 

	}

	public void clear()
	{ 
		damageIcon.enabled = false;
		rangeIcon.enabled = false;
		healthIcon.enabled = false;
		armorIcon.enabled = false;

		UnitName.text = "";
		OneText.text = "";
		TwoText.text = "";
		SelectButton.enabled = false;
	}

	public void SelectAllUnits()
	{

		string text = UnitName.text;
		if (text.Contains ("(")) {
			text = text.Substring (0, text.IndexOf ("(")-1);
		}
		Debug.Log ("Selecting all " + text);
		SelectedManager.main.selectAllUnitType (null,text);
	}

}
