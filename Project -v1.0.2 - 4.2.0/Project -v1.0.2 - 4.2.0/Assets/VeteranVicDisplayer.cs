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
		if (transform.GetSiblingIndex () < 3) {
			((RectTransform)Description.transform.parent).anchoredPosition  = ((RectTransform)Description.transform.parent).anchoredPosition + new Vector2 (0, 292);
		}

		unitName.text = stat.UnitName;
		if (stat.DeathTime > 0) {
			status.text = "Dead";
		}
		else
		{
			status.text = "";
		}

		icon.sprite = stat.mySprite;
		Award.text = reward;
		Description.text = stat.unitType + "\n";
		if(stat.damageDone > 25){
			Description.text += "Damage Dealt: " + (int)stat.damageDone + "\n";}
		
		if (stat.kills > 0) {
			Description.text += "Kills: " + stat.kills + "\n";
		}
		if (stat.healingDone > 50) {
			Description.text += "Repairs Done: " +  (int)stat.healingDone + "\n";
		}
		if (stat.energyGained > 10) {
			Description.text += "Energy Regenerated: " +  (int)stat.energyGained + "\n";}
		
		if (stat.mitigatedDamage > 5) {
			Description.text += "Enemy Damage Reduced by Armor: " +  (int)stat.mitigatedDamage + "\n";
		}

		if (stat.damageTaken > 5) {
			Description.text += "Damage Taken: " +  (int)stat.damageTaken;
		}
	}
}
