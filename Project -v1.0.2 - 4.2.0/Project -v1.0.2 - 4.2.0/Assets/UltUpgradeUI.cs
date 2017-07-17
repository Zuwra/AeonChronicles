using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltUpgradeUI : MonoBehaviour {


	public Text ColOneDescription;
	public Text ColTwoDescription;
	public Text DescriptionText;
	public int UltNumber;

	public List<string> ColOneDescriptions;
	public List<string> ColTwoDescriptions;
	int colOneLevel;
	int colTwoLevel;

	// Use this for initialization
	void Start () {
		updateDescription ();
	}

	public void BuyColOneUlt(int levelNum)
	{
		colOneLevel = levelNum;
		//ColOneDescription.text = ColOneDescriptions [levelNum];
		updateDescription ();
	}

	public void BuyColTwoUlt(int levelNum)
	{
		colTwoLevel = levelNum;
		//ColTwoDescription.text = ColTwoDescriptions [levelNum];
		updateDescription ();
	}


	void updateDescription()
	{
		if (UltNumber == 1) {
			DescriptionText.text = getUltOneDescription ();
		}
	}

	public string getUltOneDescription()
	{
		string toReturn = "Targeted Friendly Units Energy systems are redirected into hyper charging their weapons for a short period.\nThey gain a " + (40 + colOneLevel * 10) + "% damage and attack speed boost but lose 10% of their maximum energy per second.\n Effect ends when no energy remains.";
		if (colTwoLevel == 1) {
			toReturn += "\n\nUnit's energy are recharged 33% when first cast.";
		}
		else if (colTwoLevel == 2) {
			toReturn += "\n\nUnit's energy are recharged 66% when first cast.";
		}
		else if (colTwoLevel == 3) {
			toReturn += "\n\nUnit's energy are recharged 100% when first cast.";
		}
		return toReturn;
		}


}
