using UnityEngine;
using System.Collections;

public class WarpCore : Ability {

	public bool isKey;


	override
	public continueOrder canActivate(bool showError)
	{continueOrder order = new continueOrder ();
		order.nextUnitCast = false;
		if (isKey) {
			order.canCast = false;
			}

		return order;
	}
	override
	public void Activate()
	{
		GameObject.Find ("GameRaceManager").GetComponent<GarataiManager> ().setKeyWarpCore (this.gameObject);

		//return false;
	}

	public void setKey(bool input)
	{
		isKey = input;
	}

	public void deactivate()
	{isKey = false;

	}
	public override void setAutoCast(bool offOn){
	}


	// Use this for initialization
	void Start () {
		if (isKey) {
			//GameObject.Find ("GameRaceManager").GetComponent<GarataiManager> ().setKeyWarpCore (this.gameObject);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
