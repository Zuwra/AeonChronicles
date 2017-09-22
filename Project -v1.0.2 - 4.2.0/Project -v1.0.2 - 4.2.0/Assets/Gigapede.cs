using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gigapede : VisionTrigger, Modifier {

	public List<GameObject> segments = new List<GameObject> ();

	public GameObject toSpawn;
	int segmentLeft = 5;
	float DamageAmount;
	private UnitStats mystats;

	// Use this for initialization
	void Start () {

		mystats = GetComponent<UnitStats> ();
		mystats.addModifier (this);
		DamageAmount = mystats.Maxhealth / segmentLeft;
	}



	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{

		if (segmentLeft > 1 && (mystats.health - (damage -mystats.armor)) < (segmentLeft - 1) * DamageAmount) {
			segmentLeft--;
		
			mystats.armor--;
			Instantiate (toSpawn, segments[segmentLeft].transform.position, Quaternion.identity);
			segments [segmentLeft].SetActive (false);

			foreach (UnitManager unit in InVision) {
				updateDamageBuff (unit);
			}
		}


		return damage;
	}


	public override void  UnitEnterTrigger(UnitManager manager)
	{
		if(segmentLeft >1 && manager.gameObject != gameObject){
			foreach (IWeapon weap in manager.myWeapon) {
				weap.AdjustAttack (0, segmentLeft - 1, false, this);
			}
		}
	}

	public override void  UnitExitTrigger(UnitManager manager)
	{
		foreach (IWeapon weap in manager.myWeapon) {
			weap.removeAttackBuff (this);
		}
	}


	public void updateDamageBuff(UnitManager manager )
	{

		foreach (IWeapon weap in manager.myWeapon) {
			weap.removeAttackBuff (this);
		}


		if(this &&manager && gameObject && segmentLeft >1 && manager.gameObject != gameObject){
			foreach (IWeapon weap in manager.myWeapon) {
				weap.AdjustAttack (0, segmentLeft - 1, false, this);
			}
		}
	}

}
