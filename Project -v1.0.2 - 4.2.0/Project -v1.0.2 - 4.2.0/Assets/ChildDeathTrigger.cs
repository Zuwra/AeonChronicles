using UnityEngine;
using System.Collections;

public class ChildDeathTrigger : MonoBehaviour, Modifier {



	// Use this for initialization
	void Start () {
		transform.root.GetComponentInParent<UnitManager> ().myStats.addDeathTrigger (this);
	//	Debug.Log ("Trigger " + transform.root.GetComponentInParent<UnitManager> ().gameObject.name);
	}



	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{

		if (this) {
			GetComponent<UnitManager> ().myStats.kill (source);
		}
		return 0;
	}


}
