using UnityEngine;
using System.Collections;

public class BloodFury : MonoBehaviour, Modifier, Notify {

	// Unit modifier that make them move faster the less health they have
	private IWeapon myWeapon;

	private float initialMoveSpeed;
	private UnitStats myStats;
	private IMover myMover;

	// Use this for initialization
	void Start () {
		myWeapon = GetComponent<IWeapon> ();

		myStats = GetComponent<UnitStats> ();
		myStats.addModifier (this);
		myWeapon.triggers.Add (this);
		myMover = GetComponent<UnitManager> ().cMover;
		initialMoveSpeed = myMover.MaxSpeed;
	}

	// Update is called once per frame
	void Update () {

	}




	public void trigger(GameObject source, GameObject projectile,GameObject target, float damage)
	{
		myMover.MaxSpeed = initialMoveSpeed + (1 - (myStats.health / myStats.Maxhealth)) * initialMoveSpeed;


	}

	public float modify(float damage, GameObject source)
	{

		myMover.MaxSpeed = initialMoveSpeed + (1 - (myStats.health / myStats.Maxhealth)) * initialMoveSpeed;


		return damage;
	}

}
