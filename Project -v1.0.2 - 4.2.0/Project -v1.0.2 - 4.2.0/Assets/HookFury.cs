using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HookFury : MonoBehaviour,  Notify {

	// Unit modifier that make them attack faster the less health they have
	public List<IWeapon> myWeapon;

	private UnitStats myStats;
	// Use this for initialization
	void Start () {
		myWeapon = new List<IWeapon>(GetComponents<IWeapon> ());
		//Debug.Log ("Length is " + myWeapon.Count);

		myStats = GetComponent<UnitStats> ();

		foreach (IWeapon weap in myWeapon) {
			weap.addNotifyTrigger (this);
		}
	}
	



	public float trigger(GameObject source, GameObject projectile,UnitManager target, float damage)
	{
		float toChange = -(.5f - (myStats.health / myStats.Maxhealth) / 2);
		//Debug.Log ("Changing " + toChange);
		foreach (IWeapon weap in myWeapon) {
			weap.removeAttackSpeedBuff (this);
			weap.changeAttackSpeed (toChange, 0, false, this);
		}
		return damage;
	}


}
