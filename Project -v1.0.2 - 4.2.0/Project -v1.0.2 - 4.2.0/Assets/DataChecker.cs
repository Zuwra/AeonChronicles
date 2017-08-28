using UnityEngine;
using System.Collections;

public class DataChecker : MonoBehaviour {

	public int LevelCountDisable;
	public bool enable;

	public int showOnlyOn = -1;

	public UnityEngine.Events.UnityEvent toChange;

	void Start () {

		//Debug.Log ("Level count " + LevelData.getHighestLevel() + "  " + LevelCountDisable + "  " + this.gameObject);

		if (showOnlyOn != -1) {
			if (LevelData.getHighestLevel () == showOnlyOn) {
				if (toChange.GetPersistentEventCount () == 0) {
					this.gameObject.SetActive (true);
				} else {
				//	toChange.Invoke ();
				}
			} else {
				if (toChange.GetPersistentEventCount () == 0) {
				this.gameObject.SetActive (false);
				} else {
					toChange.Invoke ();
				}
			}
		}

		else if (LevelData.getHighestLevel () > LevelCountDisable) {
			if (toChange.GetPersistentEventCount () == 0) {
				this.gameObject.SetActive (enable);
			} else {
				//Debug.Log ("Invoking");
			//toChange.Invoke ();
		}
		} else {
			if (toChange.GetPersistentEventCount () == 0) {
			this.gameObject.SetActive (!enable);
			} else {
			//	Debug.Log ("Invoking 2");
			toChange.Invoke ();
			}
		}


	}

}
