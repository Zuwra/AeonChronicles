using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitCardCreater : MonoBehaviour {

	public Image unitIcon;
	public Text UnitName;
	public Text health;
	public Text armor;
	public Text Mass;
	public Text speed;

	public Text damage;
	public Text attackSpeed;
	public Text range;

	public Text energyText;
	public Image energyIcon;


	public Image damageIcon;
	public Image rangeIcon;
	public Image attackSpeedIcon;


	public Text UnitDescription;
	private UnitManager currentUnit;

	private bool hasUnit;

	private BuilderUI builder;


	// Use this for initialization
	void Start () {
		builder = GetComponent<BuilderUI> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentUnit) {
			
			health.text = " "+currentUnit.myStats.health + "/" + currentUnit.myStats.Maxhealth;
			if (currentUnit.myStats.MaxEnergy > 0) {
				energyText.text = " "+currentUnit.myStats.currentEnergy + "/" + currentUnit.myStats.MaxEnergy;	

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
		UnitDescription.text = manager.myStats.UnitDescription;
		currentUnit = manager;
		hasUnit = true;
		unitIcon.material = manager.myStats.Icon;
		UnitName.text = manager.UnitName;
		health.text = " "+manager.myStats.health + "/" + manager.myStats.Maxhealth;
		armor.text =  " " +manager.myStats.armor;
		Mass.text = " " + manager.myStats.mass;

		if (manager.cMover != null) {
			speed.text = " " + manager.cMover.MaxSpeed;
		} else {speed.text = "";
		}



		if (currentUnit.myStats.MaxEnergy > 0) {
			energyIcon.enabled = true;
			energyText.enabled = true;
			energyText.text = currentUnit.myStats.currentEnergy + "/" + currentUnit.myStats.MaxEnergy;	

		} else {
			energyIcon.enabled = false;
			energyText.enabled = false;
		}

		if (manager.myWeapon != null) {
			
			damageIcon.enabled = true;
			rangeIcon.enabled= true;
			attackSpeedIcon.enabled = true;
			damage.text = " " + manager.myWeapon.baseDamage;
			attackSpeed.text = " " + manager.myWeapon.attackPeriod;
			range.text = " " + manager.myWeapon.range;
			if (manager.myWeapon.numOfAttacks > 1) {
				damage.text = " " + manager.myWeapon.baseDamage + " (X" + manager.myWeapon.numOfAttacks +")";
			}
			if (manager.myWeapon.extraDamage.Length > 0) {
				damage.text = damage.text + " ( +" + manager.myWeapon.extraDamage [0].bonus + " v " + manager.myWeapon.extraDamage [0].type + ")";
			}

		
		} else {
			damageIcon.enabled = false;
			rangeIcon.enabled= false;
			attackSpeedIcon.enabled= false;

			damage.text = "" ;
			attackSpeed.text = "";
			range.text = "" ;
		}
		builder.loadUnit (obj);

	}



}
