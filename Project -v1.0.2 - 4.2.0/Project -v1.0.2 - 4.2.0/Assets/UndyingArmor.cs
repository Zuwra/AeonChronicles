using UnityEngine;
using System.Collections;

public class UndyingArmor :  IEffect,Modifier {


	private bool onTarget;
	public GameObject myEffect;
	private GameObject effectOnChar;
	private UnitStats mystat;
	private float endtime;

	// Update is called once per frame
	void Update () {
		if (onTarget) {

			if (Time.time > endtime) {

				mystat.removeModifier (this);
				Destroy (effectOnChar);
				Destroy (this);
				return;
			}
		}
	}

	public void initialize(GameObject source, GameObject e){
		if (!onTarget) {
			Vector3 loc = this.gameObject.transform.position;
			loc.y += 4;
			effectOnChar = (GameObject) Instantiate (e, loc, Quaternion.identity);
			effectOnChar.transform.SetParent (this.gameObject.transform);
		}

		onTarget = true;
		endtime = Time.time + 10;
		mystat = GetComponent<UnitManager> ().myStats;
		mystat.addModifier (this);



	}

	public override void apply (GameObject source, GameObject target)
	{Debug.Log ("Applying to " + target);

		target.AddComponent<UndyingArmor> ();

		target.GetComponent<UndyingArmor> ().initialize (source,myEffect);


	}


	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{
		endtime -= .25f;

		damage = Mathf.Min (damage, mystat.health -1);

		Debug.Log ("Returning " + damage);
		return damage;
	}


}
