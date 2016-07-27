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
	private Text ThreeText;
	public Text UnitName;
	// Use this for initialization
	void Start () {
		OneText = columnOne.GetComponent<Text> ();
		TwoText = columnTwo.GetComponent<Text> ();
		ThreeText = columnThree.GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadUnit(UnitManager man, int number, string unitName)
		{
		
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
		if (number > 5) {
			ThreeText.text = "\n# " + number;
		} else {
			ThreeText.text = "\n";
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
		ThreeText.text = "";}



}
