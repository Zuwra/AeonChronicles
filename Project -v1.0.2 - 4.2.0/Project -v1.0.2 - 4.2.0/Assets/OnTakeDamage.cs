using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTakeDamage : MonoBehaviour, Modifier {

	[Tooltip("Flat speed change")]
	public float speedChange;
	public float duration =1;
	public bool refreshOnHit;

	private UnitStats mystats;
	UnitManager myManager;

	float turnOffTime;
	Coroutine myCroutine;

	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
		mystats = GetComponent<UnitStats> ();
		mystats.addModifier (this);
	}




	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{
		if (myCroutine == null) {
			myCroutine = StartCoroutine (Changer());
		}
		if (refreshOnHit) {
			turnOffTime = Time.time + duration;
		}

		return damage;
	}

	IEnumerator Changer()
	{
		turnOffTime = Time.time + duration;
		myManager.cMover.changeSpeed (0,speedChange,false,this);


		while (Time.time < turnOffTime) {
		
			yield return null;
		}
		myManager.cMover.removeSpeedBuff(this);
		myCroutine = null;

	}
}
