using UnityEngine;
using System.Collections;

public class LifeSteal : MonoBehaviour, Notify {

	// Use this for initialization
	private UnitStats myStats;
	public float percentage = .5f;

	void Start () {
		myStats = GetComponent<UnitStats> ();
		this.GetComponent<IWeapon> ().triggers.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void trigger(GameObject source, GameObject projectile,GameObject target, float damage)
	{
		myStats.heal (damage * percentage);

	}


}
