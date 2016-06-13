using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitCardCreater : MonoBehaviour {

	public Image unitIcon;
	public Text UnitName;
	public Text health;
	public Text armor;
	public Text Mass;
	public Text speed;

	public List<GameObject> weaponIcons = new List<GameObject> ();

	public Text energyText;
	public Image energyIcon;

	public Text unitTypes;

	public Image damageIcon;
	public Image rangeIcon;
	public Image attackSpeedIcon;

	public Text kills;

	public Text UnitDescription;
	public UnitManager currentUnit;

	private bool hasUnit;

	private BuilderUI builder;


	// Use this for initialization
	void Start () {
		builder = GetComponent<BuilderUI> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentUnit) {
			
			health.text = "  "+ (int)currentUnit.myStats.health + "/" + currentUnit.myStats.Maxhealth;
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




	public void CreateCard(RTSObject obj)
	{
		UnitManager manager = obj.gameObject.GetComponent<UnitManager> ();

		obj.gameObject.GetComponent<Selected> ().setIcon (unitIcon.gameObject);
		UnitDescription.text = manager.myStats.UnitDescription;
		currentUnit = manager;
		hasUnit = true;
		unitIcon.sprite = manager.myStats.Icon;
		UnitName.text = manager.UnitName;
		health.text = "  "+manager.myStats.health + "/" + manager.myStats.Maxhealth;
		armor.text =  "  " +manager.myStats.armor;
		Mass.text = "  " + manager.myStats.mass;
		kills.text = "Kills: " + manager.myStats.kills;
		if (manager.cMover != null) {
			speed.text = "  " + manager.cMover.getMaxSpeed();
		} else {speed.text = "";
		}

		string s = "Types - ";
		foreach(UnitTypes.UnitTypeTag ut in manager.myStats.unitTags){
			s += ut + " - ";
		}
		unitTypes.text = s;
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
