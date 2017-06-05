using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeteranVicDisplayer : MonoBehaviour {


	public Text unitName;
	public Image icon;
	public Text Award;
	public Text Description;
	public Text status;



	public void SetStats(VeteranStats stat, string reward)
	{
		unitName.text = stat.UnitName;
		if (stat.DeathTime > 0) {
			status.text = "Dead";
		}
		else
		{
			status.text = "";
		}


		Award.text = reward;
		Description.text = stat.unitType + "\n"
		+ "Damage Dealt: " + stat.damageDone + "\n"
		+ "Kills: " + stat.kills + "\n"
		+ "Repairs Done: " + stat.healingDone + "\n"
		+ "Energy Regenerated: " + stat.energyGained + "\n"
		+ "Enemy Damage Reduced by Armor: " + stat.mitigatedDamage;
	}
}
