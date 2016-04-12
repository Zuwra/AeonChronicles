using UnityEngine;
using System.Collections;

public class CapturableUnit : MonoBehaviour {


	// In order to use this, set the units playerNumber(UnitManager) to 3
	// and disable the FogOfWarUnitScript
	// Set the Vision Range in the Unitmanger 5 more than what it should be,


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void capture()
	{UnitManager manage = GetComponent<UnitManager> ();
		manage.PlayerOwner = 1;
		manage.visionRange -= 5;
		GetComponent<SphereCollider> ().radius = manage.visionRange;
		FogOfWarUnit foggy = GetComponent<FogOfWarUnit> ();
		foggy.radius = manage.visionRange + 3;
		foggy.enabled = true;
		foggy.Initialize ();
		GameObject.FindObjectOfType<GameManager>().activePlayer.applyUpgrade (this.gameObject);
		GameObject.FindObjectOfType<GameManager>().activePlayer.UnitCreated (GetComponent<UnitStats>().supply);
		Destroy (this);
	}


	void OnTriggerEnter(Collider other)
	{
		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

		if (manage) {
			if (manage.PlayerOwner == 1) {
				capture ();
			}
		}
	}
}
