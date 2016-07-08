using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OreDispenser : MonoBehaviour {


	public float OreRemaining;

	private bool inUse;


	public GameObject currentMinor;
	
	private Queue<GameObject> workers =  new Queue<GameObject >();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public bool requestWork(GameObject obj)
	{if (!currentMinor) {
			currentMinor = obj;
			return true;
		}
		return false;
	}



	public float getOre( float amount)
	{float giveBack = Mathf.Min (OreRemaining, amount);
		OreRemaining -= giveBack;
		if (OreRemaining <= .5) {

			ErrorPrompt.instance.showError ("Ore Deposit Depleted");
			SelectedManager.main.DeselectObject (GetComponent<UnitManager> ());

			Destroy (this.gameObject);
		}
		return Mathf.Min (OreRemaining, amount);

	}

	public void removeWorker(GameObject client)
	{
		workers.Enqueue(client);
	}

}
