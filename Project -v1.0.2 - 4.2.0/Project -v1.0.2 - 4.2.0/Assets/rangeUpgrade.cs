using UnityEngine;
using System.Collections;

public class rangeUpgrade : Upgrade {


	public float rangeAmount;
	public string unitName;

	override
	public void applyUpgrade (GameObject obj){

		UnitManager manager = obj.GetComponent<UnitManager> ();

		if(manager){
			if (manager.UnitName == unitName ) {

				foreach (IWeapon weap in manager.myWeapon) {
					weap.range += rangeAmount;
				}

			}
		}
	}

	public override void unApplyUpgrade (GameObject obj){

	}
}
