﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitCardCreater : MonoBehaviour {


	public GameObject unitIcon;
	public Text UnitName;
	public Text health;
	public Text armor;
	public Text SpellResist;
	public Image SpellIcon;
	public Text speed;


	public List<GameObject> weaponIcons = new List<GameObject> ();
	public Text weaponTitle;
	public Text energyText;
	public Image energyIcon;

	public Text ArmorTypes;
	public Text SizeTypes;
	public Text OtherTypes;

	public Image damageIcon;
	public Image rangeIcon;
	public Image attackSpeedIcon;

	public Image speedIcon;

	public Text UnitDescription;
	public UnitManager currentUnit;

	private bool hasUnit;

	private BuilderUI builder;

	public Text VetStats;
	public Text VetBackStory;
	public GameObject VetCanvas;

	public OreDispenser myDispense;

	public Canvas ORECANVAS;
	public Text OreText;

	public GameObject BuffList;

	string blankText = "";

	// Use this for initialization
	void Start () {
		builder = GetComponent<BuilderUI> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentUnit) {
			if (myDispense) {
				OreText.text = "Remaining Ore: "+ myDispense.OreRemaining;
			} else {

				health.text = "  " + (int)currentUnit.myStats.health + "/" + (int)currentUnit.myStats.Maxhealth;
				if (currentUnit.myStats.MaxEnergy > 0) {
					energyText.text = "  " + (int)currentUnit.myStats.currentEnergy + "/" + currentUnit.myStats.MaxEnergy;	

				}
			}

		} else {
			if (hasUnit) {
				this.GetComponent<Canvas> ().enabled = false;
				hasUnit = false;
				//RaceManager.updateUIUnitcount();
			}
		}
	}


	public void toggleStats ()
	{VetStats.text = blankText;
		if (currentUnit) {
			VetCanvas.SetActive (!VetCanvas.activeSelf);
			if (!currentUnit.myStats.isHero) {
				if (currentUnit.myStats.veternStat.UnitName != "" && currentUnit.PlayerOwner ==1) {
					VetStats.text = RaceNames.getInstance ().getRank (currentUnit.myStats.veternStat.kills) + " ";
				}
			} else {
				VetStats.text = blankText;
			}
			VetStats.text +=  currentUnit.myStats.veternStat.UnitName + 
			"\nKills: " + currentUnit.myStats.veternStat.kills +
			"\nDamage Dealt: " + currentUnit.myStats.veternStat.damageDone +
			"\nHealing Done: " + currentUnit.myStats.veternStat.healingDone +

			"\nEnergy Charged: " + currentUnit.myStats.veternStat.energyGained+
			"\nArmor Mitigated\nDamage: " + currentUnit.myStats.veternStat.mitigatedDamage ;

			if (currentUnit.PlayerOwner == 1) {
				VetBackStory.text = currentUnit.myStats.veternStat.backstory;
			} else {
				VetBackStory.text = blankText;
			}
		}
	}


	public void CreateCard(RTSObject obj)
	{
		UnitManager manager = obj.gameObject.GetComponent<UnitManager> ();
		currentUnit = manager;
		myDispense = obj.GetComponent<OreDispenser> (); 
		if (myDispense) {
			setForOre (obj);
			return;
		} 
		ORECANVAS.enabled = false;
		VetCanvas.SetActive (false);
	

		obj.gameObject.GetComponent<Selected> ().setIcon (unitIcon.gameObject);
		UnitDescription.text = manager.myStats.UnitDescription;

		hasUnit = true;
		unitIcon.GetComponentInChildren<Image>().sprite = manager.myStats.Icon;
		UnitName.text = manager.UnitName;
		health.text = " "+(int)manager.myStats.health + "/" + (int)manager.myStats.Maxhealth;
		armor.text =  " " +manager.myStats.armor;
		if (manager.myStats.spellResist > 0) {
			SpellResist.gameObject.SetActive (true);
			SpellIcon.gameObject.SetActive (true);
			SpellResist.text = " " + (int)(100 * manager.myStats.spellResist) + "%";

		} else {
			SpellResist.gameObject.SetActive (false);
			SpellIcon.gameObject.SetActive (false);
		}
	
		if (manager.cMover != null) {
			if (manager.cMover.getMaxSpeed () > 0) {
				speedIcon.enabled = true;
				speed.text = " " + manager.cMover.getMaxSpeed ();
			} else {
				speed.text = blankText;
				speedIcon.enabled = false;
			}
		} else {
			speedIcon.enabled = false;
			speed.text = blankText;
		}

		ArmorTypes.text = blankText + manager.myStats.armorType;
		SizeTypes.text = blankText + manager.myStats.sizeType;
		string s = "  ";

		if(manager.myStats.otherTags.Count > 0){

		s = " - ";
			foreach(UnitTypes.UnitTypeTag ut in manager.myStats.otherTags){
			s += ut + " - ";
			}
		}
		OtherTypes.text = s;
	
		if (currentUnit.myStats.MaxEnergy > 0) {
			energyIcon.enabled = true;
			energyText.enabled = true;
			energyText.text = currentUnit.myStats.currentEnergy + "/" + currentUnit.myStats.MaxEnergy;	

		} else {
			energyIcon.enabled = false;
			energyText.enabled = false;
		}

		if (manager.myWeapon.Count == 0) {
			weaponTitle.enabled = false;
		} else {
			weaponTitle.enabled = true;
		}

		// Change this if a unit ever has more than 5 weapons;
		for (int i = 0; i < 5; i++) {
	
				weaponIcons [i].SetActive (manager.myWeapon.Count > i);
			if (manager.myWeapon.Count > i) {
				weaponIcons [i].transform.FindChild("DamageIcon").GetComponent<Image> ().sprite = manager.myWeapon [i].myIcon;
				if (manager.myWeapon [i].getUpgradeLevel () > 0) {
					weaponIcons [i].transform.GetComponentInChildren<Text> ().text = "" + manager.myWeapon [i].getUpgradeLevel ();
				} else {
					weaponIcons [i].transform.GetComponentInChildren<Text> ().text = "";
				}
			}

		}

		foreach (Transform child in BuffList.transform) {
			Destroy (child.gameObject);
		}

		foreach (Buff dabuff in manager.myStats.goodBuffs) {
			if (dabuff != null) {
			
			
				GameObject ic = (GameObject)Instantiate (Resources.Load ("BuffIcon"));
				ic.transform.SetParent (BuffList.transform);
				ic.transform.FindChild ("BuffHelp").GetComponentInChildren<Text> ().text = dabuff.toolDescription;
				ic.GetComponent<Image> ().sprite = dabuff.HelpIcon;
			}
		}

		foreach (Buff dabuff in manager.myStats.badBuffs) {
			if (dabuff != null) {
				GameObject ic = (GameObject)Instantiate (Resources.Load ("BuffIcon"));
				ic.transform.SetParent (BuffList.transform);
				ic.transform.FindChild ("BuffHelp").GetComponentInChildren<Text> ().text = dabuff.toolDescription;
				ic.GetComponent<Image> ().sprite = dabuff.HelpIcon;
			}
		}

		builder.loadUnit (obj);

	}

	public void turnOff()
	{
		if (this) {
			Canvas c = GetComponent<Canvas> ();
			if (c) {
				c.enabled = false;
			}
			if (ORECANVAS) {
				ORECANVAS.enabled = false;
			}
		}
	}

	public void setForOre(RTSObject obj)
	{

		ORECANVAS.enabled = true;
		OreText.text = "Remaining Ore: "+ myDispense.OreRemaining;
		GetComponent<Canvas> ().enabled = false;



	}


}
