using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarataiManager : MonoBehaviour {


	public GameObject KeyWarpCore;


	private List<Capacitor> myCapacitors = new List<Capacitor>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setKeyWarpCore(GameObject obj)
	{
		if (KeyWarpCore) {
			KeyWarpCore.GetComponent<WarpCore> ().setKey (false);
		}
		KeyWarpCore = obj;
		KeyWarpCore.GetComponent<WarpCore> ().setKey (true);
	}


	public void addCapacitor(Capacitor cap)
	{
		myCapacitors.Add (cap);
	}


}
