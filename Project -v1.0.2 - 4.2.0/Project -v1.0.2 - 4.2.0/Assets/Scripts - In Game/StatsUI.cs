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

	public void loadUnit(UnitManager man, int number, string unitName)
		{
		OneText.text = unitName +"\n  HP: " + man.myStats.Maxhealth + "\nArmor: " + man.myStats.armor;
		if (man.myWeapon != null) {
			TwoText.text += "\n  Damage: " + man.myWeapon.baseDamage + "\n   Range: " + man.myWeapon.range;
		} 
		ThreeText.text = "\n# " + number;

	}

	public void clear()
	{ OneText.text = "";
		TwoText.text = "";
		ThreeText.text = "";}



}
