using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageUpgrade : Upgrade {

	[System.Serializable]
	public struct unitAmount
	{
		public string UnitName;
		public List<float> amount;
		public List<specialDamage> mySpecial;
	}

	[System.Serializable]
	public struct specialDamage
	{
		public UnitTypes.UnitTypeTag myType;
		public float amount;
	
	}

	public List<unitAmount> unitsToUpgrade = new List<unitAmount> ();


	override
	public void applyUpgrade (GameObject obj){

        UnitManager manager = obj.GetComponent<UnitManager>();
		//if (obj.GetComponentInChildren<TurretMount> ()) {
			//return;}

		bool doubleTheDamage = false;
		if (obj.GetComponent<DoubleUpgradeApp> () && obj.GetComponent<DoubleUpgradeApp> ().doubleIt) {
			doubleTheDamage = true;}

	
		foreach (unitAmount ua in unitsToUpgrade) {
			if (manager.UnitName.Contains(ua.UnitName)) {
			//Debug.Log ("Applying to "+ ua.UnitName + "  " + obj.name);
				for (int i = 0; i < manager.myWeapon.Count; i++)
					if (manager.myWeapon [i]) {

					
						manager.myWeapon [i].changeAttack(0, ua.amount[i],true,null);
					
						if (doubleTheDamage) {
							manager.myWeapon [i].changeAttack(0, ua.amount[i],true,null);
						}
						manager.myWeapon [i].incrementUpgrade ();
						manager.gameObject.SendMessage ("upgrade", Name,SendMessageOptions.DontRequireReceiver);
						if (ua.mySpecial.Count > 0) {

							IWeapon.bonusDamage foundOne = new IWeapon.bonusDamage();
							bool found = false;
							foreach (IWeapon.bonusDamage bonusA in manager.myWeapon[i].extraDamage) {
								if (bonusA.type == ua.mySpecial [i].myType) {
									foundOne = bonusA;
									found = true;
								}
							}
							if (found) {
								foundOne.bonus += ua.mySpecial [i].amount;
							}

							else{
								IWeapon.bonusDamage bonus = new IWeapon.bonusDamage ();
								bonus.bonus = ua.mySpecial [i].amount;
								bonus.type = ua.mySpecial [i].myType;
								manager.myWeapon [i].extraDamage.Add (bonus);
							}
						}
					}

				if (manager.GetComponent<Selected> ().IsSelected) {
					RaceManager.upDateUI ();
				}
			
			}
		}



	
	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
