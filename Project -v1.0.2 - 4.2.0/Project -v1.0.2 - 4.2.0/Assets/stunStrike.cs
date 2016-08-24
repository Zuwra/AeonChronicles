using UnityEngine;
using System.Collections;

public class stunStrike : MonoBehaviour, Notify {


	private explosion myexplode;
	public float damageAmount;

	// Use this for initialization
	void Start () {
		if (this.gameObject.GetComponent<explosion> ()) {
			myexplode = this.gameObject.GetComponent<explosion>();
			myexplode.triggers.Add (this);
		}



	}




	// Update is called once per frame
	void Update () {


	}

	public void trigger(GameObject source,GameObject proj, GameObject target, float damage)
	{UnitManager manage = target.GetComponent<UnitManager> ();
		if (manage && source != target && manage.myStats.isUnitType(UnitTypes.UnitTypeTag.Structure)) {


			manage.setStun (true, this);
			StartCoroutine (delayUnStun (manage));
		}

	}

	IEnumerator delayUnStun(UnitManager man)
	{
		yield return new WaitForSeconds (10);
		man.setStun (false, this);
			
	}


}
