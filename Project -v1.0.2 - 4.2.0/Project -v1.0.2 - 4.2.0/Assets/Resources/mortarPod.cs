using UnityEngine;
using System.Collections;

public class mortarPod : MonoBehaviour, Validator, Notify {




	public float totalShots;
	public float reloadRate;
	private float shotCount;
	private IWeapon weapon;

	public bool FireAll;

	private float nextActionTime;

	// Use this for initialization
	void Start () {


		shotCount = totalShots;
		weapon = this.gameObject.GetComponent<IWeapon> ();
		nextActionTime = Time.time;

		weapon.validators.Add (this);
		weapon.triggers.Add (this);
		if (FireAll) {
			weapon.attackPeriod = .05f;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextActionTime) {
			nextActionTime += reloadRate;
			if(shotCount < totalShots)
			{
				shotCount++;
			}
		
		}


	
	}

	public void toggleFireAll()
	{FireAll = ! FireAll;


	}


	public bool validate(GameObject source, GameObject target)
		{if (shotCount > 0) {
			return true;
		
		}
		return false;
	}

	public void trigger(GameObject source, GameObject proj, GameObject target)
		{
		shotCount --;
		if (FireAll) {
			weapon.attackPeriod = .05f;
		}


	}

}
