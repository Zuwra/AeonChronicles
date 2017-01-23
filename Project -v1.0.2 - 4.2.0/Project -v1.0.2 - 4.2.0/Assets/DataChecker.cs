using UnityEngine;
using System.Collections;

public class DataChecker : MonoBehaviour {

	public int LevelCountDisable;
	public bool enable;

	public int showOnlyOn = -1;
	void Start () {
		if (showOnlyOn != -1) {
			if (LevelData.getHighestLevel () == showOnlyOn) {
				this.gameObject.SetActive (true);
			} else {
				this.gameObject.SetActive (false);
			}
		}

		else if (LevelData.getHighestLevel () > LevelCountDisable) {
			this.gameObject.SetActive (enable);
		} else {
			this.gameObject.SetActive (!enable);
		}


	}

}
