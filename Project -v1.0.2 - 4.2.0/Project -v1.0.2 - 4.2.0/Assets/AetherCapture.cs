using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherCapture : MonoBehaviour {
	public UnitManager myManager;
	public bool cutscene;
	//Add all managers in the unit to this list
	// In order to use this, set the units playerNumber(UnitManager) to 3
	// and disable the FogOfWarUnitScript
	// Set the Vision Range in the Unitmanger 5 more than what it should be,


	public AugmentAetherVictory myObjective;

	public List<GameObject> enemies = new List<GameObject>();


	void OnTriggerEnter(Collider other)
	{
		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

		if (manage) {
			if (manage.PlayerOwner == 2) {
				enemies.Add (other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{

		if (enemies.Contains (other.gameObject)) {
			enemies.Remove (other.gameObject);
		}
	}



	public void capture()
	{
		if (myManager) {
			myManager.PlayerOwner = 1;
			myManager.visionRange -= 5;
			myManager.GetComponent<SphereCollider> ().radius = myManager.visionRange;
		



			FogOfWarUnit foggy = GetComponent<FogOfWarUnit> ();
			if (foggy) {
				foggy.radius = myManager.visionRange + 3;
				foggy.enabled = true;
			}

			GameManager.main.activePlayer.applyUpgrade (myManager);
			GameManager.main.activePlayer.UnitCreated (myManager.GetComponent<UnitStats> ().supply);
			GameManager.main.activePlayer.addUnit (myManager);
			GameManager.main.activePlayer.addVeteranStat (myManager.GetComponent<UnitStats> ().veternStat);

			if (cutscene) {
				GameObject.FindObjectOfType<MainCamera> ().setCutScene (myManager.gameObject.transform.position, 120);
			}
		}
	}
	/*
	IEnumerator waitForWave()
	{



		myManager.PlayerOwner = 1;
		myManager.visionRange -= 5;
		myManager.GetComponent<SphereCollider> ().radius = myManager.visionRange;



		FogOfWarUnit foggy = GetComponent<FogOfWarUnit> ();
		if (foggy) {
			foggy.radius = myManager.visionRange + 3;
			foggy.enabled = true;
		}w

		GameManager.main.activePlayer.applyUpgrade (myManager.gameObject);
		GameManager.main.activePlayer.UnitCreated (myManager.GetComponent<UnitStats>().supply);
		GameManager.main.activePlayer.addUnit (myManager.gameObject);
		GameManager.main.activePlayer.addVeteranStat (myManager.GetComponent<UnitStats>().veternStat);

		if (cutscene) {
			GameObject.FindObjectOfType<MainCamera> ().setCutScene (myManager.gameObject.transform.position, 120);
		}
		//Destroy (this);


	}
*/

}
