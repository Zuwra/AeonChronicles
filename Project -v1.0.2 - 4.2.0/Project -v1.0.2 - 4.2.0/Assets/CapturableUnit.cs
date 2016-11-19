using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CapturableUnit : MonoBehaviour {

	public List<UnitManager> myManagers = new List<UnitManager>();
	public bool cutscene;
	//Add all managers in the unit to this list
	// In order to use this, set the units playerNumber(UnitManager) to 3
	// and disable the FogOfWarUnitScript
	// Set the Vision Range in the Unitmanger 5 more than what it should be,


	public void capture()
	{
		foreach(UnitManager manage in myManagers){
			if (!manage) {
				continue;}
		
		manage.PlayerOwner = 1;
		manage.visionRange -= 5;
		GetComponent<SphereCollider> ().radius = manage.visionRange;
		

		}
		FogOfWarUnit foggy = GetComponent<FogOfWarUnit> ();
		if (foggy) {
			foggy.radius = myManagers [0].visionRange + 3;
			foggy.enabled = true;
			foggy.Initialize ();
		}

		GameManager.main.activePlayer.applyUpgrade (this.gameObject);
		GameManager.main.activePlayer.UnitCreated (GetComponent<UnitStats>().supply);
		GameManager.main.activePlayer.addUnit (this.gameObject);
		GameManager.main.activePlayer.addVeteranStat (GetComponent<UnitStats>().veternStat);

		if (cutscene) {
			GameObject.FindObjectOfType<MainCamera> ().setCutScene (this.gameObject.transform.position, 120);
		}
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
