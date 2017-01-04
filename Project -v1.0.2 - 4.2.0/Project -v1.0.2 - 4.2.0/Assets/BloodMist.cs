using UnityEngine;
using System.Collections;

public class BloodMist : TargetAbility {


	public GameObject BloodMistObj;
	// Use this for initialization
	void Start () {
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

		Vector3 pos = location;
		pos.y += 5;
		GameObject proj = (GameObject)Instantiate (BloodMistObj, pos, Quaternion.identity);


		proj.SendMessage ("setSource", this.gameObject);


		return false;

	}
	override
	public void Cast(){



		myCost.payCost ();

		GameObject proj = null;

		Vector3 pos = location;
		pos.y += 5;
		proj = (GameObject)Instantiate (BloodMistObj, pos, Quaternion.identity);


		proj.SendMessage ("setSource", this.gameObject,SendMessageOptions.DontRequireReceiver);




	}


}
