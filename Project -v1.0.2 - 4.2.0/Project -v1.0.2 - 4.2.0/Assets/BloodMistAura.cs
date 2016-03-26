using UnityEngine;
using System.Collections;

public class BloodMistAura : MonoBehaviour, Modifier {


	private int numberOfClouds = 0;
	UnitStats myStats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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


	public float modify(float damage, GameObject source)
	{
		if (myStats.health > damage) {
			return  damage / 2;
		}
		return damage;


	}
}
