using UnityEngine;
using System.Collections;

public class EMPblast : MonoBehaviour, Notify {


	private explosion myexplode;
	public float damageAmount;

	// Use this for initialization
	void Start () {
		if (this.gameObject.GetComponent<explosion> ()) {
			myexplode = this.gameObject.GetComponent<explosion>();
			myexplode.triggers.Add (this);
		}



	}





	public float trigger(GameObject source,GameObject proj, UnitManager target, float damage)
	{UnitManager manage = target.GetComponent<UnitManager> ();
		if (manage && source != target) {


			foreach (Ability ab in manage.abilityList) {
				if (ab.myCost) {
					ab.myCost.cooldownTimer += 8;
				}
		
			}
			}
		return damage;
	}


}
