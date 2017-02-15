using UnityEngine;
using System.Collections;

public class stunStrike : MonoBehaviour, Notify {


	private explosion myexplode;

	public float stunTime;
	public UnitTypes.UnitTypeTag mustTarget;


	// Use this for initialization
	void Start () {
		if (GetComponent<Projectile> ()) {
			GetComponent<Projectile> ().triggers.Add (this);
		}
		if (this.gameObject.GetComponent<explosion> ()) {
			myexplode = this.gameObject.GetComponent<explosion>();
			myexplode.triggers.Add (this);
		}



	}





	public float trigger(GameObject source,GameObject proj, UnitManager target, float damage)
	{//Debug.Log ("Getting triggered");


		if (target && source != target) {
			//Debug.Log ("In here");
			if (mustTarget != UnitTypes.UnitTypeTag.Dead) {
				if (!target.myStats.isUnitType (mustTarget)) {
					return damage;}
			}

			target.StunForTime (this, stunTime);

		}
		return damage;
	}


}
