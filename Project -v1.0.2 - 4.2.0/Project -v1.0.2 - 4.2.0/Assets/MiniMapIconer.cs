using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIconer : MonoBehaviour, Modifier {

	public Sprite myIcon;

	[Tooltip("Update period for changing minimap position, set to 0 so it doesn't update at all")]
	public float updateRate;
	List<GameObject> myIcons = new List<GameObject>();

	// Use this for initialization
	IEnumerator Start () {

		yield return new WaitForSeconds (1);
		try{
		GetComponent<UnitStats> ().addDeathTrigger (this);
		}catch{
		}
		OnEnable ();
	}

	void OnDisable()
	{
		foreach (GameObject obj in myIcons) {
			if (GameManager.main) {
				foreach (MiniMapUIController mini in GameManager.main.MiniMaps) {
					if (obj) {
						//Debug.Log ("I died");
						if (mini) {
							mini.deleteUnitIcon (obj);
						}
					}
				}
			}
		}
	}

	void OnEnable()
	{
		StartCoroutine (waitASec ());
	}

	IEnumerator waitASec()
	{
		yield return null;
		foreach (MiniMapUIController mini in GameManager.main.MiniMaps) {

			if (mini) {
			//	Debug.Log ("Adding MiniIcon");
				myIcons.Add (mini.showUnitIcon (this.transform.position, myIcon));
			}
		}

		if (updateRate > 0) {
			InvokeRepeating ("updatePosition", updateRate, updateRate);
		}
	}



	public float modify ( float amount,GameObject deathSource, DamageTypes.DamageType typ)
	{
		OnDisable ();

		return amount;
	}

	void updatePosition()
	{
		//Debug.Log ("Updating Position");
		foreach (GameObject obj in myIcons) {
			foreach (MiniMapUIController mini in GameManager.main.MiniMaps) {
				if (obj && mini && mini.enabled) {
					mini.updateUnitPos(obj,this.transform.position);
				}
			}
		}

	}


}
