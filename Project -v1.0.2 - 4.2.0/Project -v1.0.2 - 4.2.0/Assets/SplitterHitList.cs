using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitterHitList : MonoBehaviour,Notify{


	public float chargeCount;
	public List<GameObject> hitTargets = new List<GameObject>();



	void Start()
	{this.gameObject.GetComponent<IWeapon> ().triggers.Add (this);
		hitTargets = new List<GameObject> ();

		this.gameObject.GetComponent<IWeapon> ().attackPeriod = chargeCount * 2;
	}

	void Update()
	{}



	public void trigger(GameObject source,GameObject proj, GameObject target)
		{
		proj.GetComponent<SplitterShot> ().chargesRemaning = chargeCount;
		hitTargets.Clear ();
		hitTargets.Add (target);
	}

	public bool isValidEnemy(GameObject obj)
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
