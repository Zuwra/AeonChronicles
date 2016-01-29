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

		OneText.text = unitName +"\n" + man.myStats.Maxhealth + "\n" + man.myStats.armor;
		if (man.myWeapon != null) {
			damageIcon.enabled = true;
			rangeIcon.enabled = true;
			TwoText.text += "\n" + man.myWeapon.baseDamage + "\n" + man.myWeapon.range;
		} 
		ThreeText.text = "\n# " + number;

	}

	public void clear()
	{ 
		damageIcon.enabled = false;
		rangeIcon.enabled = false;
		healthIcon.enabled = false;
		armorIcon.enabled = false;


		OneText.text = "";
		TwoText.text = "";
		ThreeText.text = "";}



}
