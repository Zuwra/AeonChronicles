using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackLashZone : MonoBehaviour {

	private GameObject source;
	private int playerNumber;
	private List<BackLashAura> myAuras = new List<BackLashAura>();
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void setSource(GameObject obj)
	{
		source = obj;
		playerNumber = source.GetComponent<UnitManager> ().PlayerOwner;

	}


	void OnTriggerEnter(Collider other)
	{
		UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
		if (manage) {
			if (manage.PlayerOwner != playerNumber) {
				myAuras.Add (other.gameObject.AddComponent<BackLashAura> ());
				if (source) {
					other.GetComponent<BackLashAura> ().Initialize (source.GetComponent<UnitManager> ().myStats);
				} else {
					other.GetComponent<BackLashAura> ().Initialize (null);
				}

			}


		}

	}


	void OnDestroy()
	{
		Debug.Log ("This is being destroyed");
		foreach (BackLashAura a in myAuras) {

			a.UnApply ();}
	}

	void OnTriggerExit(Collider other)
	{
		BackLashAura manage = other.gameObject.GetComponent<BackLashAura> ();
		if (manage) {

			myAuras.Remove	(other.GetComponent<BackLashAura> ());
			other.GetComponent<BackLashAura> ().UnApply ();

		}

	}

}

