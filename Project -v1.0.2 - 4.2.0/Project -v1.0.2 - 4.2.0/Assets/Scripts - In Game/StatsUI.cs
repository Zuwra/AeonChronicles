using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {


	public GameObject columnOne;
	public GameObject columnTwo;
	public GameObject columnThree;


	private Text OneText;
	private Text TwoText;
	private Text ThreeText;
	// Use this for initialization
	void Start () {
		OneText = columnOne.GetComponent<Text> ();
		TwoText = columnTwo.GetComponent<Text> ();
		ThreeText = columnThree.GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadUnit(UnitStats stats, IWeapon weapon, int number)
		{
		OneText.text = stats.name +"\n    HP: " + stats.Maxhealth + "\nArmor: " + stats.armor;
		if (weapon != null) {
			TwoText.text += "\nDamage: " + weapon.baseDamage + "\n   Range: " + weapon.range;
		}
		ThreeText.text = "\n# " + number;

	}

	public void clear()
	{ OneText.text = "";
		TwoText.text = "";
		ThreeText.text = "";}



}
