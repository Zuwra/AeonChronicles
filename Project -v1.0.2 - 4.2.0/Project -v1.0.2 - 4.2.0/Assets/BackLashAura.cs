using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackLashAura : MonoBehaviour, Notify {

	private int numberOfClouds = 0;
	List<IWeapon> myweap;
	UnitStats myStats;
	UnitManager manage;
	UnitStats source;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

	public void Initialize(UnitStats sor)
	{source = sor;
		if (numberOfClouds == 0) {
			manage =  this.gameObject.GetComponent<UnitManager> ();
			if (manage) {
				myStats = manage.myStats;
				UnitUtility.applyWeaponTrigger (manage, this);
			
			}

		} else {
			numberOfClouds++;
		}

	}


	public void UnApply()
	{
		if (numberOfClouds > 1) {

			numberOfClouds--;
		} else {

			UnitUtility.removeWeaponTrigger (manage, this);

			Destroy (this);

		}

	}


	public float trigger(GameObject sou, GameObject proj,UnitManager target, float damage)
	{if (source) {
			source.heal (damage / 4);
		}
		myStats.TakeDamage (damage / 2, source.gameObject, DamageTypes.DamageType.Regular);

		return damage;
	}
}
