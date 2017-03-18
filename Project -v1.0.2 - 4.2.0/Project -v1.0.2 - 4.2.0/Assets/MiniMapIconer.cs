using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIconer : MonoBehaviour, Modifier {

	public Sprite myIcon;

	[Tooltip("Update period for changing minimap position, set to 0 so it doesn't update at all")]
	public float updateRate;
	List<GameObject> myIcons = new List<GameObject>();

	// Use this for initialization
	void Start () {

		Invoke ("AddIcon", 1f);
	}

	void AddIcon()
	{
		GetComponent<UnitStats> ().addDeathTrigger (this);

		foreach (MiniMapUIController mini in GameManager.main.MiniMaps) {

			if (mini.enabled) {
				myIcons.Add (mini.showUnitIcon (this.transform.position, myIcon));
			}
		}

		if (updateRate > 0) {
			InvokeRepeating ("updatePosition", updateRate, updateRate);
		}

	}




	public float modify ( float amount,GameObject deathSource, DamageTypes.DamageType typ)
	{
		foreach (GameObject obj in myIcons) {
			foreach (MiniMapUIController mini in GameManager.main.MiniMaps) {
				if (obj && mini.enabled) {
					mini.deleteUnitIcon (obj);
				}
			}
		}

		return amount;
	}

	void updatePosition()
	{
		foreach (GameObject obj in myIcons) {
			foreach (MiniMapUIController mini in GameManager.main.MiniMaps) {
				if (obj && mini.enabled) {
					mini.updateUnitPos(obj,this.transform.position);
				}
			}
		}

	}


}
