using UnityEngine;
using System.Collections;

public class Bombardment : TargetAbility{


	public GameObject Explosion;
	public int shotCount = 35;
	public float myDamage = 40;
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

		//	myCost.payCost ();

		for (int i = 0; i < shotCount; i++) {
		
			StartCoroutine( Fire ((i * .07f), location));
		}


		return false;

	}




	IEnumerator Fire (float time, Vector3 location)
	{

		yield return new WaitForSeconds(time);
		GameObject proj = null;


		Vector3 spawnLoc = location;
		spawnLoc.y += 200;


		proj = (GameObject)Instantiate (Explosion, spawnLoc, Quaternion.identity);

		Projectile script = proj.GetComponent<Projectile> ();
		proj.SendMessage ("setSource", this.gameObject, SendMessageOptions.DontRequireReceiver);


		script.damage = myDamage;

		script.Source = this.gameObject;
		script.setLocation (location);

		
	}



	override
	public void Cast(){





	}


}