using UnityEngine;
using System.Collections;

public class BallisticMissile:  TargetAbility {


	public GameObject missile;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

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

		GameObject proj = null;

		Vector3 pos = this.gameObject.transform.position;
		pos.y += this.gameObject.GetComponent<CharacterController> ().radius;
		proj = (GameObject)Instantiate (missile, pos, Quaternion.identity);

		Projectile script = proj.GetComponent<Projectile> ();
		proj.SendMessage ("setSource", this.gameObject);
		proj.SendMessage ("setTarget", target);
		proj.SendMessage ("setDamage", 10);


		script.target = target.GetComponent<UnitManager>();
		script.Source = this.gameObject;





		return false;

	}
	override
	public void Cast(){


		myCost.payCost ();

		GameObject proj = null;

		Vector3 pos = this.gameObject.transform.position;
		pos.y += this.gameObject.GetComponent<CharacterController> ().radius;
		proj = (GameObject)Instantiate (missile, pos, Quaternion.identity);

		Projectile script = proj.GetComponent<Projectile> ();
		proj.SendMessage ("setSource", this.gameObject);
		proj.SendMessage ("setLocation", location);
		if (target) {
			proj.SendMessage ("setTarget", target);
		}
		proj.SendMessage ("setDamage", 10);


		script.target = target.GetComponent<UnitManager> ();
		script.Source = this.gameObject;




	}


}
