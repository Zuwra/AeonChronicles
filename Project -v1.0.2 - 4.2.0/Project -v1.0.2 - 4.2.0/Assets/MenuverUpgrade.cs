using UnityEngine;
using System.Collections;

public class MenuverUpgrade: Upgrade {


	override
	public void applyUpgrade(GameObject obj)
	{
		evasiveMenuvers em = obj.GetComponent<evasiveMenuvers> ();
		if (em) {
			em.chanceMultiplier = 2;
		}
	}

	public override void unApplyUpgrade (GameObject obj){

		evasiveMenuvers em = obj.GetComponent<evasiveMenuvers> ();
		if (em) {
			em.chanceMultiplier = 1;
		}
	}
}
