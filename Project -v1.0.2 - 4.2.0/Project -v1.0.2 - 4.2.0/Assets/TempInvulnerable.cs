using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInvulnerable : MonoBehaviour, Modifier {


	private float nextActionTime;
	private UnitStats mystats;
	public float TimeBetween;
	public float TimeInvulnerable;

	public GameObject Effect;

	// Use this for initialization
	void Start () {
		nextActionTime = -10;
		mystats = GetComponent<UnitStats> ();
		mystats.addModifier (this);
	}
		




	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{

		if (Time.time > nextActionTime) {
			StartCoroutine (InVulnerable());
			return 0;
		}


		return damage;
	}


	IEnumerator InVulnerable()
	{
		if (Effect) {
			Effect.SetActive (true);
		}
		mystats.otherTags.Add (UnitTypes.UnitTypeTag.Invulnerable);
		mystats.SetTags ();
		nextActionTime = Time.time + TimeBetween + TimeInvulnerable;

		yield return new WaitForSeconds (TimeInvulnerable);
		if (Effect) {
			Effect.SetActive (false);
		}
		mystats.otherTags.Remove(UnitTypes.UnitTypeTag.Invulnerable);
		mystats.SetTags ();


	}
}
