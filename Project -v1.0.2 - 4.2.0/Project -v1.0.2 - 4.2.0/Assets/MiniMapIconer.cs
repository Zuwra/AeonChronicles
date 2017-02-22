using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIconer : MonoBehaviour, Modifier {

	public Sprite myIcon;


	List<GameObject> myIcons = new List<GameObject>();
	// Use this for initialization
	void Start () {

		GetComponent<UnitStats> ().addDeathTrigger (this);

		foreach (MiniMapUIController mini in GameObject.FindObjectsOfType<MiniMapUIController>()) {
			myIcons.Add (mini.showUnitIcon (this.transform.position, myIcon));
		}
	}


	public float modify ( float amount,GameObject deathSource, DamageTypes.DamageType typ)
	{
		foreach (GameObject obj in myIcons) {
			foreach (MiniMapUIController mini in GameObject.FindObjectsOfType<MiniMapUIController>()) {
				if (obj) {
					mini.deleteUnitIcon (obj);
				}
			}
		}

		return amount;
	}

}
