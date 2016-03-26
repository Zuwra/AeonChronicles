using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodMistCloud : MonoBehaviour {

	private GameObject source;
	private int playerNumber;
	private List<BloodMistAura> myAuras = new List<BloodMistAura>();
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
			if (manage.PlayerOwner == playerNumber) {
				myAuras.Add (other.gameObject.AddComponent<BloodMistAura> ());
				other.GetComponent<BloodMistAura> (). Initialize();
			
			
			}
		
		
		}

	}


	void OnDestroy()
	{
		Debug.Log ("This is being destroyed");
		foreach (BloodMistAura a in myAuras) {
		
			a.UnApply ();}
	}

	void OnTriggerExit(Collider other)
	{
		BloodMistAura manage = other.gameObject.GetComponent<BloodMistAura> ();
		if (manage) {
			
			myAuras.Remove	(other.GetComponent<BloodMistAura> ());
			other.GetComponent<BloodMistAura> ().UnApply ();

			}

		}

	}

