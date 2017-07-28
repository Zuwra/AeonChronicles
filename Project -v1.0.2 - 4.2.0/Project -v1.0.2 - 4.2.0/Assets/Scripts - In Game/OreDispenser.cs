using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OreDispenser : MonoBehaviour {


	public float OreRemaining;

	private bool inUse;


	public GameObject currentMinor;
	
	private Queue<GameObject> workers =  new Queue<GameObject >();
	// used for increased mining
	public float returnRate = 1;



	public bool requestWork(GameObject obj)
	{if (!currentMinor) {
			currentMinor = obj;
			return true;
		}
		return false;
	}



	public float getOre( float amount)
	{float giveBack = Mathf.Min (OreRemaining, amount * returnRate);
		OreRemaining -= giveBack;
		if (OreRemaining <= .5) {

			ErrorPrompt.instance.OreDepleted(transform.position);
			SelectedManager.main.DeselectObject (GetComponent<UnitManager> ());
			AugmentAttachPoint AAP = GetComponent<AugmentAttachPoint> ();
			if (AAP && AAP.myAugment) {
			AAP.myAugment.GetComponent<Augmentor> ().Unattach ();
			}
			Destroy (this.gameObject);
		}
		return Mathf.Min (OreRemaining, giveBack);

	}

	public void removeWorker(GameObject client)
	{
		workers.Enqueue(client);
	}

}
