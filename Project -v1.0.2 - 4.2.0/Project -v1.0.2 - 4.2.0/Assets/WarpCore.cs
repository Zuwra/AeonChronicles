using UnityEngine;
using System.Collections;

public class WarpCore : Ability {

	public bool isKey;


	override
	public bool canActivate()
	{if (isKey) {
			return false;}
		return true;
	}
	override
	public bool Activate()
	{
		GameObject.Find ("GameRaceManager").GetComponent<GarataiManager> ().setKeyWarpCore (this.gameObject);

		return false;
	}

	public void setKey(bool input)
	{
		isKey = input;
	}

	public void deactivate()
	{isKey = false;

	}


	// Use this for initialization
	void Start () {
		if (isKey) {
			GameObject.Find ("GameRaceManager").GetComponent<GarataiManager> ().setKeyWarpCore (this.gameObject);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
