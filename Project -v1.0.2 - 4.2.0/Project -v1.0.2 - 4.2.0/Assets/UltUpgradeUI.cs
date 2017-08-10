using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltUpgradeUI : MonoBehaviour {

	public string UltName;  //HyperCharge  Nimbus   BarrierDome   Firestorm
	public List<UltCategory> myCategories;

	public static int availableArb;
	public Text ArbText;

	public Button BuyButton;
	public Text notEnoughMoney;

	void Awake()
	{
		UltUpgradeUI.availableArb = LevelData.getArbitronium ();
	}

	void Start()
	{

		for (int n = 0; n < myCategories.Count + 1; n++) {
			for (int i = 0; i < PlayerPrefs.GetInt (UltName + ""+n); i++) {
				myCategories[n].upLevel ();
				UltUpgradeUI.availableArb--;
			}
		}
		
		foreach (UltCategory cat in myCategories) {
			cat.UpdateSprites ();
		}
		updateArbText ();
	}

	public void openBuyArbWindow()
	{
		if (LevelData.getMoney () > 4) {
			BuyButton.interactable = true;
			notEnoughMoney.enabled = false;
		} else {
			BuyButton.interactable = false;
			notEnoughMoney.enabled = true;
		}
	}

	public void increaseUltLevel(int index)
	{if (UltUpgradeUI.availableArb > 0) {
			if (myCategories [index].upLevel ()) {
				UltUpgradeUI.availableArb--;
			}
			PlayerPrefs.SetInt (UltName + "" + index, myCategories [index].currentLevel);
			updateArbText ();

		}
	}

	public void decreaseUltLevel(int index)
	{
		if (myCategories [index].downLevel ()) {
		
			UltUpgradeUI.availableArb++;
		}
		PlayerPrefs.SetInt (UltName +""+index, myCategories[index].currentLevel);
		updateArbText ();

	}

	public void BuyArbitronium()
	{
		UltUpgradeUI.availableArb++;
		//LevelData.addMoney (-5);
		LevelManager.main.changeMoney (-5);
		LevelData.addArbitronium (1);
		updateArbText ();
		openBuyArbWindow ();
	}

	public void ToggleUlt(int index)
	{
		if (UltUpgradeUI.availableArb == 0 && myCategories [index].currentLevel == 0) {
			return;
		}
		myCategories [index].Toggle ();
		PlayerPrefs.SetInt (UltName + "" + index, myCategories [index].currentLevel);
		updateArbText ();

	}

	public void updateArbText()
	{
		ArbText.text = ""+UltUpgradeUI.availableArb;
		if (UltUpgradeUI.availableArb == 0) {
			ArbText.color = Color.red;
		} else {
			ArbText.color = Color.green;
		}
	}

}

[System.Serializable]
public class UltCategory
{
	public int currentLevel;
	public List<Image> mySprites;
	public List<Image> miniSprites;
	public Sprite onSprite;
	public Sprite offSprite;
	public List<TextAffect> fillerStrings;

	public bool downLevel ()
	{if (currentLevel > 0) {
			currentLevel--;
			UpdateSprites ();
			return true;
		}
		return false;
	}

	public bool upLevel ()
	{
		if (currentLevel < mySprites.Count) {
			currentLevel++;
			UpdateSprites ();
			return true;
		}
		return false;
	}

	public void Toggle()
	{
		if (currentLevel == 0) {
			currentLevel = 1;
			UltUpgradeUI.availableArb--;
		} else {
			currentLevel = 0;
			UltUpgradeUI.availableArb++;
		}
		UpdateSprites ();
	}

	public void UpdateSprites()
	{
		for (int i = 0; i < mySprites.Count; i++) {
			if (currentLevel > i) {
				mySprites [i].sprite = onSprite;
				miniSprites [i].sprite = onSprite;
			} else {
				mySprites [i].sprite = offSprite;
				miniSprites [i].sprite = offSprite;
			}
		}
		foreach (TextAffect affect in fillerStrings) {
			affect.fillString (currentLevel);
		}
			
	}
}

[System.Serializable]
public class TextAffect
{
	public Text toAffect;
	public List<string> LevelStrings;
	public Color defaultColor = Color.yellow;
	public Color UpgradedColor = Color.yellow;

	public void fillString(int n)
	{
		toAffect.text = LevelStrings [n];
		if (n == 0) {
			toAffect.color = defaultColor;
		} else {
			toAffect.color = UpgradedColor;
		}
	}
}
	