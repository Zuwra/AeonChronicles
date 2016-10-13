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
		foreach (unitAmount ua in unitsToUpgrade) {
			if (manager.UnitName.Contains(ua.UnitName)) {
				for (int i = 0; i < manager.myWeapon.Count; i++)
					if (manager.myWeapon [i]) {
						
						manager.myWeapon [i].changeAttack(0, ua.amount[i],true,null);
						if (ua.mySpecial.Count > 0) {
							IWeapon.bonusDamage bonus = new IWeapon.bonusDamage ();
							bonus.bonus =  ua.mySpecial [i].amount;
							bonus.type = ua.mySpecial [i].myType;
							manager.myWeapon [i].extraDamage.Add (bonus);
						}
					}
			
			}
		}

	
	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
