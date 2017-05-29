using UnityEngine;
using System.Collections;

public class MenuverUpgrade: Upgrade {


	override
	public void applyUpgrade(GameObject obj)
	{
		evasiveMenuvers em = obj.GetComponent<evasiveMenuvers> ();
		if (em) {
			em.chanceMultiplier = 2;
			em.Descripton = "This pilot is so skilled... he has a 56% chance of dodging attacks if he is moving.";
		}
	}

	public override void unApplyUpgrade (GameObject obj){

		evasiveMenuvers em = obj.GetComponent<evasiveMenuvers> ();
		if (em) {
			em.chanceMultiplier = 1;
			em.Descripton = "This pilot is so skilled... he has a 28% chance of dodging attacks if he is moving.";
		}
	}
}
