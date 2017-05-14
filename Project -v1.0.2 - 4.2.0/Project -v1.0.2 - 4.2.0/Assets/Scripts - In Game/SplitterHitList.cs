using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitterHitList : MonoBehaviour,Notify{


	public float chargeCount;
	public List<UnitManager> hitTargets = new List<UnitManager>();



	void Start()
	{this.gameObject.GetComponent<IWeapon> ().triggers.Add (this);
		hitTargets = new List<UnitManager> ();

	//	this.gameObject.GetComponent<IWeapon> ().attackPeriod = chargeCount * 2;
	}




	public float trigger(GameObject source,GameObject proj, UnitManager target, float damage)
		{

		//Debug.Log ("Clearing the list");
		proj.GetComponent<SplitterShot> ().chargesRemaning = chargeCount;
		hitTargets.Clear ();
		hitTargets.Add (target);

		return damage;
	}

	public bool isValidEnemy(UnitManager obj)
	{
		//Debug.Log ("Does orignal contain it?" +hitTargets.Contains (obj) + "   " + obj.gameObject);
		return !hitTargets.Contains (obj);
		
	}

	public void increaseChargeCount()
	{
		chargeCount++;
	}
		//this.gameObject.GetComponent<IWeapon> ().attackPeriod = chargeCount * 2;

	public void decreaseChargeCount()
	{
		chargeCount--;
	}
		//this.gameObject.GetComponent<IWeapon> ().attackPeriod = chargeCount * 2;}
}
