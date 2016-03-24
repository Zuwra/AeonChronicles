using UnityEngine;
using System.Collections;

public class LifeSteal : MonoBehaviour, Notify {

	// Use this for initialization
	private UnitStats myStats;

	private PopUpMaker popper;
	public float percentage = .5f;

	void Start () {
		myStats = GetComponent<UnitStats> ();
		this.GetComponent<IWeapon> ().triggers.Add (this);
		popper = GetComponent<PopUpMaker> ();
	}
	
	// Update is called once per frame
	void Update () {

	}



	public void trigger(GameObject source, GameObject projectile,GameObject target, float damage)
	{
		myStats.heal (damage * percentage);
		popper.CreatePopUp ("+" + (int)damage * percentage, Color.green);

	}


}
