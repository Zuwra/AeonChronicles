using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gravityWell : MonoBehaviour, Modifier {


	public List<GameObject> Protecters = new List<GameObject>();
	public float duration;


	int playerOwner = 1;

	void Start()
	{

		Invoke ("End", duration);

	}

	void End()
	{
		foreach (GameObject obj in Protecters) {
			if (obj) {
			
				obj.GetComponent<UnitManager> ().myStats.removeModifier (this);
			}
		
		}
	}

	public float modify (float amount, GameObject source, DamageTypes.DamageType theType){
	//	Debug.Log ("Modifying damage " + theType);
		if (theType == DamageTypes.DamageType.Energy) {
			return 0;
		}	
		return amount;
	}

	void OnTriggerEnter(Collider other)
	{
		explosion e = other.GetComponent<explosion> ();
		if (e && e.sourceInt != playerOwner) {
			Destroy (other.gameObject);
			return;
		}
	
		UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
		if (manage && manage.PlayerOwner == playerOwner) {
			other.GetComponent<UnitStats> ().addHighPriModifier (this);
			Protecters.Add (other.gameObject);
		
		
		}

		AreaDamage area = other.GetComponent<AreaDamage> ();
		if (area) {
			Destroy (area.transform.root.gameObject);
		}

	}

	void OnTriggerExit(Collider other)
	{

		UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
		if (manage && Protecters.Contains(other.gameObject)) {
			manage.myStats.removeModifier (this);
			Protecters.Remove (other.gameObject);
		
		}
	}



}
