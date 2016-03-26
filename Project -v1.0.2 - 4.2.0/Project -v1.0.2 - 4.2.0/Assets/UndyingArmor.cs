using UnityEngine;
using System.Collections;

public class UndyingArmor :  IEffect,Modifier {


	private bool onTarget;

	private UnitStats mystat;
	private float endtime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (onTarget) {
			Debug.Log (Time.time + "   " + endtime);
			if (Time.time > endtime) {

				mystat.removeModifier (this);
				Destroy (this);
				return;
			}
		}
	}

	public void initialize(GameObject source){
		onTarget = true;
		endtime = Time.time + 10;
		mystat = GetComponent<UnitManager> ().myStats;
		mystat.addModifier (this);


	}

	public override void apply (GameObject source, GameObject target)
	{Debug.Log ("Applying to " + target);

		target.AddComponent<UndyingArmor> ();

		target.GetComponent<UndyingArmor> ().initialize (source);


	}


	public float modify(float damage, GameObject source)
	{
		endtime -= .25f;

		damage = Mathf.Min (damage, mystat.health -1);

		Debug.Log ("Returning " + damage);
		return damage;
	}


}
