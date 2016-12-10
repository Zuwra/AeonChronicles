using UnityEngine;
using System.Collections;

public class DataChecker : MonoBehaviour {

	public int LevelCountDisable;
	public bool enable;

	void Start () {

		Debug.Log (LevelData.getHighestLevel() + "   " + LevelCountDisable);
		if (LevelData.getHighestLevel () > LevelCountDisable) {
			this.gameObject.SetActive (enable);
		} else {
			this.gameObject.SetActive (!enable);
		}


	}

}
