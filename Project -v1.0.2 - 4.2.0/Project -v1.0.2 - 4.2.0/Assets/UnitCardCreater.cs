using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitCardCreater : MonoBehaviour {

	public Image unitIcon;
	public Text UnitName;
	public Text health;
	public Text armor;
	public Text SpellResist;
	public Image SpellIcon;
	public Text speed;

	public List<GameObject> weaponIcons = new List<GameObject> ();

	public Text energyText;
	public Image energyIcon;

	public Text ArmorTypes;
	public Text SizeTypes;
	public Text OtherTypes;

	public Image damageIcon;
	public Image rangeIcon;
	public Image attackSpeedIcon;

	public Text UnitDescription;
	public UnitManager currentUnit;

	private bool hasUnit;

	private BuilderUI builder;

	public Text VetStats;
	public GameObject VetCanvas;


	// Use this for initialization
	void Start () {
		builder = GetComponent<BuilderUI> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentUnit) {
			
			health.text = "  "+ (int)currentUnit.myStats.health + "/" + (int)currentUnit.myStats.Maxhealth;
			if (currentUnit.myStats.MaxEnergy > 0) {
				energyText.text = "  "+ (int)currentUnit.myStats.currentEnergy + "/" + currentUnit.myStats.MaxEnergy;	

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
	{if (currentUnit) {
			VetCanvas.SetActive (!VetCanvas.activeSelf);
			if (!currentUnit.myStats.isHero) {
				VetStats.text = RaceNames.getInstance ().getRank (currentUnit.myStats.veternStat.kills) + " ";
			} else {
				VetStats.text = "";
			}
			VetStats.text +=  currentUnit.myStats.veternStat.UnitName + 
			"\nKills: " + currentUnit.myStats.veternStat.kills +
			"\nDamage Dealt: " + currentUnit.myStats.veternStat.damageDone +
			"\nHealing Done: " + currentUnit.myStats.veternStat.healingDone +

			"\nEnergy Charged: " + currentUnit.myStats.veternStat.energyGained+
			"\nArmor Mitigated Damage: " + currentUnit.myStats.veternStat.mitigatedDamage ;
		}
	}


	public void CreateCard(RTSObject obj)
	{VetCanvas.SetActive (false);
		UnitManager manager = obj.gameObject.GetComponent<UnitManager> ();

		obj.gameObject.GetComponent<Selected> ().setIcon (unitIcon.gameObject);
		UnitDescription.text = manager.myStats.UnitDescription;
		currentUnit = manager;
		hasUnit = true;
		unitIcon.sprite = manager.myStats.Icon;
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
			speed.text = " " + manager.cMover.getMaxSpeed();
		} else {speed.text = "";
		}

		ArmorTypes.text = "A: " + manager.myStats.armorType;
		SizeTypes.text = "S: " + manager.myStats.sizeType;
		string s = "";

		if(manager.myStats.otherTags.Count > 0){

		s = "Types - ";
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

		// Change this if a unit ever has more than 5 weapons;
		for (int i = 0; i < 5; i++) {
	
				weaponIcons [i].SetActive (manager.myWeapon.Count > i);
			if (manager.myWeapon.Count > i) {
				weaponIcons [i].transform.FindChild("DamageIcon").GetComponent<Image> ().sprite = manager.myWeapon [i].myIcon;
			}

		}

		builder.loadUnit (obj);

	}



}
