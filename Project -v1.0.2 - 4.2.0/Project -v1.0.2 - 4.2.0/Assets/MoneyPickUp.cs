using UnityEngine;
using System.Collections;

public class MoneyPickUp : MonoBehaviour {

	public float resOne;
	public float resTwo;




	public void OnTriggerEnter(Collider other)
	{
		UnitManager man = other.GetComponent<UnitManager> ();
		if (man) {
			if (man.PlayerOwner == 1) {
				GameManager gm = GameObject.FindObjectOfType<GameManager> ();
				gm.activePlayer.updateResources (resOne, resTwo, false);

				if (resOne > 0) {

					PopUpMaker.CreateGlobalPopUp ("+" + resOne + " Ore", Color.white, this.gameObject.transform.position);
				} else {
					PopUpMaker.CreateGlobalPopUp ("+" + resTwo, Color.cyan, this.gameObject.transform.position);
				}
				Destroy (this.gameObject);
			
			}
		
		}
	}

}
