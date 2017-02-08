using UnityEngine;
using System.Collections;

public class BloodMist : TargetAbility {


	protected Selected mySelect;
	public GameObject BloodMistObj;
	Coroutine currentCharger;

	// Use this for initialization
	void Start () {
		myType = type.target;
		mySelect = GetComponent<Selected> ();

		if (chargeCount >-1) {
			if(chargeCount < maxChargeCount){
				currentCharger = StartCoroutine (increaseCharges ());
			}
		}
	}

	public void UpMaxCharge()
	{
		maxChargeCount++;
	
			if (currentCharger == null) {
				currentCharger = StartCoroutine (increaseCharges ());

		}
	}

	public int maxChargeCount;


	override
	public continueOrder canActivate(bool showError){

		continueOrder order = new continueOrder ();
		if (chargeCount == 0) {
			order.canCast = false;
		}

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
		
		if (chargeCount >-1) {
			changeCharge (-1);
			if (currentCharger == null) {
				currentCharger = StartCoroutine (increaseCharges ());
			}
		}

		myCost.payCost ();

		if (chargeCount > 0) {
			myCost.resetCoolDown ();
		}

		Vector3 pos = location;
		pos.y += 5;
		GameObject proj = (GameObject)Instantiate (BloodMistObj, pos, Quaternion.identity);


		proj.SendMessage ("setSource", this.gameObject);


		return false;

	}
	override
	public void Cast(){

		if (chargeCount >-1) {
			changeCharge (-1);
			if (currentCharger == null) {
				currentCharger = StartCoroutine (increaseCharges ());
			}
		}

		myCost.payCost ();
		if (chargeCount > 0) {
			myCost.resetCoolDown ();
		}
		GameObject proj = null;

		Vector3 pos = location;
		pos.y += 5;
		proj = (GameObject)Instantiate (BloodMistObj, pos, Quaternion.identity);


		proj.SendMessage ("setSource", this.gameObject,SendMessageOptions.DontRequireReceiver);




	}


	IEnumerator increaseCharges()
	{
		
		if (chargeCount == 0) {
			active = false;
			myCost.startCooldown ();
		}
		yield return new WaitForSeconds (myCost.cooldown);
		active = true;
		changeCharge (1);

		if (chargeCount < maxChargeCount) {
			currentCharger = StartCoroutine (increaseCharges ());
		} else {
			currentCharger = null;
		}
	}
	public void changeCharge(int n)
	{
		chargeCount += n;
		if (chargeCount == 0) {
			active = false;

		}
		if (mySelect.IsSelected) {
			RaceManager.upDateUI ();
			RaceManager.updateActivity ();

		}
	}

}
