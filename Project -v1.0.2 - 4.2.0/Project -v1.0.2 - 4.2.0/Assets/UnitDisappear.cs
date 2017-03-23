using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDisappear : MonoBehaviour {

	public int player = 1;

	public List<string> specificUnits;




	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<UnitManager> ())
		if (other.GetComponent<UnitManager> ().PlayerOwner == player) {

			if (specificUnits.Count > 0) {
				if (specificUnits.Contains (other.GetComponent<UnitManager> ().UnitName)) {
					UnitLostDefeat defeat = GameObject.FindObjectOfType<UnitLostDefeat> ();
					if (defeat && defeat.heros.Contains (other.gameObject)) {
					
						defeat.heros.Remove (other.gameObject);
						other.gameObject.GetComponent<UnitManager> ().myStats.kill (null);

					}

				}
			} else {
				UnitLostDefeat defeat = GameObject.FindObjectOfType<UnitLostDefeat> ();
				if (defeat && defeat.heros.Contains (other.gameObject)) {

					defeat.heros.Remove (other.gameObject);
					other.gameObject.GetComponent<UnitManager> ().myStats.kill (null);

				}
			}
		}
	}

}
