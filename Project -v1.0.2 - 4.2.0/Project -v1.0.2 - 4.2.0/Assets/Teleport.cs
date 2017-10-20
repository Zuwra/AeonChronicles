using UnityEngine;
using System.Collections;

public class Teleport :  TargetAbility {

	//private UnitManager manage;
	public GameObject effect;
	// Use this for initialization
	void Start () {
		//manage = this.gameObject.GetComponent<UnitManager> ();
		myType = type.target;
	}



	override
	public continueOrder canActivate(bool showError){

		continueOrder order = new continueOrder ();

		if (!myCost.canActivate (this)) {
			order.canCast = false;
		} else {
			order.nextUnitCast = false;
		}
		return order;
	}

	override
	public void Activate()
	{
	}  // returns whether or not the next unit in the same group should also cast it


	override
	public  void setAutoCast(bool offOn){}

	public override bool isValidTarget (GameObject target, Vector3 location){
		
		return true;

	}


	override
	public  bool Cast(GameObject target, Vector3 location)
	{

		myCost.payCost ();
		if (effect) {
			Instantiate (effect, this.transform.position, Quaternion.identity);
		
		}
		//GameObject proj = null;
		if (GetComponent<airmover> ()) {
			this.gameObject.transform.position = location + Vector3.up * GetComponent<airmover> ().flyerHeight;
		} else {
			this.gameObject.transform.position = location+ Vector3.up *3;
		}

		GetComponent<FogOfWarUnit> ().AutoUpdate ();
		return true;

	}
	override
	public void Cast(){


		if (effect) {
			Instantiate (effect, this.transform.position, Quaternion.identity);
			Instantiate (effect, this.transform.position, Quaternion.identity);

		}
		myCost.payCost ();

		//GameObject proj = null;
		if (GetComponent<airmover> ()) {
			this.gameObject.transform.position = location + Vector3.up * GetComponent<airmover> ().flyerHeight;
			if (effect) {

				Instantiate (effect, this.transform.position, Quaternion.identity);

			}
		} else {
			this.gameObject.transform.position = location+ Vector3.up *3;
			if (effect) {
				
				Instantiate (effect, this.transform.position, Quaternion.identity);

			}
		}

		GetComponent<FogOfWarUnit> ().AutoUpdate ();


	}


}
