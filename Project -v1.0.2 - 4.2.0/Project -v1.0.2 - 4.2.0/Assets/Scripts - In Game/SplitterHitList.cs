using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitterHitList : MonoBehaviour,Notify{


	public float chargeCount;
	public List<UnitManager> hitTargets = new List<UnitManager>();



	void Start()
	{this.gameObject.GetComponent<IWeapon> ().triggers.Add (this);
		hitTargets = new List<UnitManager> ();

		this.gameObject.GetComponent<IWeapon> ().attackPeriod = chargeCount * 2;
	}

	void Update()
	{}



	public void trigger(GameObject source,GameObject proj, UnitManager target, float damage)
		{
		proj.GetComponent<SplitterShot> ().chargesRemaning = chargeCount;
		hitTargets.Clear ();
		hitTargets.Add (target);
	}

	public bool isValidEnemy(UnitManager obj)
	{
		//Debug.Log ("Does orignal contain it?" +hitTargets.Contains (obj));
		return !hitTargets.Contains (obj);
		
	}

	public void increaseChargeCount()
		{chargeCount ++;
		this.gameObject.GetComponent<IWeapon> ().attackPeriod = chargeCount * 2;
	}
	public void decreaseChargeCount()
	{chargeCount --;
		this.gameObject.GetComponent<IWeapon> ().attackPeriod = chargeCount * 2;}
}
