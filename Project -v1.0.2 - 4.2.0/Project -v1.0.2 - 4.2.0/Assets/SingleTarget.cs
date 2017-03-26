using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleTarget:  TargetAbility {

	private UnitManager manage;
	public GameObject missile;
	protected Selected mySelect;
	public List<UnitTypes.UnitTypeTag> cantTarget = new List<UnitTypes.UnitTypeTag>();
	public List<UnitTypes.UnitTypeTag> mustTarget = new List<UnitTypes.UnitTypeTag> ();
	public bool canTargetSelf;
	public enum sideTarget{ally, enemy, all}
	public sideTarget who;
	public int maxChargeCount;
	public IEffect myEffect;

	Coroutine currentCharger;
	// Use this for initialization
	void Start () {
		myType = type.target;
		mySelect = GetComponent<Selected> ();
		manage = this.gameObject.GetComponent<UnitManager> ();
		if (chargeCount >-1) {
			if(chargeCount < maxChargeCount){
				currentCharger = StartCoroutine (increaseCharges ());
			}
		}
	}


	public void UpMaxCharge()
	{
		maxChargeCount = 3;

		if (currentCharger == null) {
			currentCharger = StartCoroutine (increaseCharges ());

		}
	}


	override
	public continueOrder canActivate(bool showError){

		continueOrder order = new continueOrder ();

		if (!myCost.canActivate (this)) {
			order.canCast = false;
			if (myCost.energy == 0 && myCost.ResourceOne == 0 && chargeCount > 0) {
				order.canCast = true;
			}
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
	public  void setAutoCast( bool offOn){}

	public override bool isValidTarget (GameObject target, Vector3 location){
		if (target == null) {
			return false;
		}

		UnitManager m = target.GetComponent<UnitManager> ();
		if (m == null) {
			return false;}
		switch (who) {

		case sideTarget.ally:
			if (manage.PlayerOwner != m.PlayerOwner) {
				return false;
			}
			break;

		case sideTarget.enemy:
			if (manage.PlayerOwner == m.PlayerOwner) {
				return false;
			}
			break;

		}
		if (!canTargetSelf && target == this.gameObject) {
			return false;

		}

		foreach (UnitTypes.UnitTypeTag t in cantTarget) {
			
			if (manage.myStats.isUnitType (t)) {
				return false;}
		}

		if (mustTarget.Count == 0) {
			return true;
		}
		foreach (UnitTypes.UnitTypeTag t in mustTarget) {
				if (manage.myStats.isUnitType (t)) {
					return true;}
			}
	
		return false;

	}


	override
	public  bool Cast(GameObject tar, Vector3 location)
	{target = tar;
		
		if (target) {


			if (chargeCount >-1) {
				changeCharge (-1);
				if (currentCharger == null) {
					currentCharger = StartCoroutine (increaseCharges ());
				}
			}
			myCost.payCost ();
		
			GameObject proj = null;

			if (missile) {
				Vector3 pos = this.gameObject.transform.position;
				pos.y += this.gameObject.GetComponent<CharacterController> ().radius;
				proj = (GameObject)Instantiate (missile, pos, Quaternion.identity);

				Projectile script = proj.GetComponent<Projectile> ();
				proj.SendMessage ("setSource", this.gameObject);
				proj.SendMessage ("setTarget", tar);
				if (script) {
					script.target = target.GetComponent<UnitManager>();
					script.Source = this.gameObject;
				}

			} else {
				
				myEffect.apply (this.gameObject, tar);
			}
		}



		return false;

	}
	override
	public void Cast(){


	//	Debug.Log ("Casting in other");
		if (target) {
			if (chargeCount >-1) {
				changeCharge (-1);
				if (currentCharger == null) {
					currentCharger = StartCoroutine (increaseCharges ());
				}
			}
			myCost.payCost ();

			GameObject proj = null;

			if (missile) {
				Vector3 pos = this.gameObject.transform.position;
				pos.y += this.gameObject.GetComponent<CharacterController> ().radius;
				proj = (GameObject)Instantiate (missile, pos, Quaternion.identity);

				Projectile script = proj.GetComponent<Projectile> ();
				proj.SendMessage ("setSource", this.gameObject);
				proj.SendMessage ("setTarget", target.GetComponent<UnitManager>());
				if (script) {
					script.target = target.GetComponent<UnitManager>();
					script.Source = this.gameObject;
				}

			} else {

				myEffect.apply (this.gameObject, target);
			}
		}



	}




	IEnumerator increaseCharges()
	{

		if (chargeCount == 0) {
			active = false;
		
		}
		myCost.startCooldown ();
		yield return new WaitForSeconds (myCost.cooldown-.2f);
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
		if (chargeCount > maxChargeCount) {
			chargeCount = maxChargeCount;
		}
		if (mySelect.IsSelected) {
			RaceManager.upDateUI ();
			RaceManager.updateActivity ();

		}
	}

}
