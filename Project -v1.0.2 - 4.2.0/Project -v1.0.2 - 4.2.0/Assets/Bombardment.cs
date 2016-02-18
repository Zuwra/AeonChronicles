using UnityEngine;
using System.Collections;

public class Bombardment : TargetAbility{


	public GameObject Explosion;
	// Use this for initialization
	void Start () {
		

	}

	// Update is called once per frame
	void Update () {

	}
	override
	public continueOrder canActivate(){

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
	public  void setAutoCast(){}




	override
	public  bool Cast(GameObject target, Vector3 location)
	{



		//	myCost.payCost ();

		for (int i = 0; i < 35; i++) {
		
			StartCoroutine( Fire ((i * .1f), location));
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
		proj.SendMessage ("setSource", this.gameObject);


		script.damage = 30;

		script.Source = this.gameObject;
		script.setLocation (location);

		
	}



	override
	public void Cast(){





	}


}