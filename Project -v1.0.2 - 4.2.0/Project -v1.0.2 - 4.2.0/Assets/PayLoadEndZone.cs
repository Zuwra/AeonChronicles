using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayLoadEndZone : MonoBehaviour {

	public string unitName = "Payload";
	public int numToLoseOn =1;

	int numEntered;
	void OnTriggerEnter(Collider col)
	{
		UnitManager manage = col.gameObject.GetComponent<UnitManager> ();
		if(manage)
			{
			if (manage.UnitName == unitName) {
				numEntered++;
				manage.gameObject.SetActive (false);
				if (numEntered >= numToLoseOn) {
					VictoryTrigger.instance.Lose ();
				}
			
			} else {
				manage.gameObject.SetActive (false);
			}

		}


	}
}
