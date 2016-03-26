using UnityEngine;
using System.Collections;

public class BackLashAura : MonoBehaviour, Notify {

	private int numberOfClouds = 0;
	IWeapon myweap;
	UnitStats myStats;

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
			UnitManager manage =  this.gameObject.GetComponent<UnitManager> ();
			if (manage) {
				myStats = manage.myStats;
				myweap= manage.myWeapon;
				myweap.triggers.Add (this);
			}

		} else {
			numberOfClouds++;
		}

	}


	public void UnApply()
	{
		if (numberOfClouds > 1) {

			numberOfClouds--;}

		else{
			myweap.triggers.Remove(this);
			Destroy (this);

		}

	}


	public void trigger(GameObject sou, GameObject proj, GameObject target, float damage)
	{if (source) {
			source.heal (damage / 4);
		}
		myStats.TakeDamage (damage / 2, source.gameObject, DamageTypes.DamageType.Regular);
	
	}
}
