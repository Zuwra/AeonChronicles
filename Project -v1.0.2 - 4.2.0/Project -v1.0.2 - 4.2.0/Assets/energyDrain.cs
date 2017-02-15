using UnityEngine;
using System.Collections;

public class energyDrain : MonoBehaviour, Notify{


	public float drainAmount;
	public float percentage;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Projectile> ().triggers.Add (this);


	}

	// Update is called once per frame
	void Update () {

	}


	public float trigger(GameObject source,GameObject proj, UnitManager target, float damage)
	{
		UnitStats stats = target.GetComponent<UnitStats> ();
		if (stats != null) {
			Debug.Log ("Draining "+drainAmount + "   " + stats.MaxEnergy * percentage);
			stats.changeEnergy (-(drainAmount + stats.MaxEnergy * percentage));



		} 
		return damage;
	}


}
