using UnityEngine;
using System.Collections;

public class BloodMistAura : MonoBehaviour, Modifier {


	private int numberOfClouds = 0;
	UnitStats myStats;



	public void Initialize()
	{
		if (numberOfClouds == 0) {
			UnitManager manage =  this.gameObject.GetComponent<UnitManager> ();
			if (manage) {

				myStats = manage.myStats;
				myStats.addModifier (this);
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
			myStats.removeModifier (this);
			Destroy (this);

		}

	}


	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{
		if (myStats.health > damage) {
			return  damage / 2;
		}
		return damage;


	}
}
