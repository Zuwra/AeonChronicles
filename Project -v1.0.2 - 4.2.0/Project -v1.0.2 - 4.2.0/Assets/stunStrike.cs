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




	// Update is called once per frame
	void Update () {


	}

	public void trigger(GameObject source,GameObject proj, GameObject target, float damage)
	{Debug.Log ("Getting triggered");

		UnitManager manage = target.GetComponent<UnitManager> ();
		if (manage && source != target) {
			Debug.Log ("In here");
			if (mustTarget != UnitTypes.UnitTypeTag.Dead) {
				if (!manage.myStats.isUnitType (mustTarget)) {
					return;}
			}

			manage.StunForTime (this, stunTime);

		}

	}


}
