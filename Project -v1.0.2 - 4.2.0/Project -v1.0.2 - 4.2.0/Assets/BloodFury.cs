using UnityEngine;
using System.Collections;

public class BloodFury : MonoBehaviour, Modifier, Notify {

	// Unit modifier that make them move faster the less health they have
	private IWeapon myWeapon;


	private UnitStats myStats;
	private IMover myMover;

	// Use this for initialization
	void Start () {
		myWeapon = GetComponent<IWeapon> ();

		myStats = GetComponent<UnitStats> ();
		myStats.addModifier (this);
		myWeapon.triggers.Add (this);
		myMover = GetComponent<UnitManager> ().cMover;
	
	}

	// Update is called once per frame
	void Update () {

	}




	public float trigger(GameObject source, GameObject projectile,UnitManager target, float damage)
	{
		myMover.removeSpeedBuff (this);
		myMover.changeSpeed ((1 - (myStats.health / myStats.Maxhealth)), 0, false, this );  
		return damage;
	}

	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{

		myMover.removeSpeedBuff (this);
		myMover.changeSpeed ((1 - (myStats.health / myStats.Maxhealth)), 0, false, this );  
		return damage;
	}

}
