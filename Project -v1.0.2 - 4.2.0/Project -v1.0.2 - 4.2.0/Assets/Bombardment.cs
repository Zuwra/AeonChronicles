using UnityEngine;
using System.Collections;

public class Bombardment : TargetAbility{


	public GameObject Explosion;
	public int shotCount = 35;
	public float myDamage = 40;



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
		
			StartCoroutine( Fire ((i * .07f), location, i));
		}


		return false;

	}




	IEnumerator Fire (float time, Vector3 location, int index)
	{

		yield return new WaitForSeconds(time);
		GameObject proj = null;


		Vector3 hitzone = location;
		float radius = ((float)index/(float)shotCount )* 43;// Random.Range (0, 40);
		float angle = index * 15;// Random.Range (0, 360);

		if (index % 2 == 1) {
			angle += 180;
		}
		hitzone.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
		hitzone.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

	

		Vector3 spawnLoc = hitzone;
		spawnLoc.y += 200;


		proj = (GameObject)Instantiate (Explosion, spawnLoc, Quaternion.identity);

		Projectile script = proj.GetComponent<Projectile> ();
		proj.SendMessage ("setSource", this.gameObject, SendMessageOptions.DontRequireReceiver);

		if (script) {
			script.damage = myDamage;
			script.sourceInt = 1;
			script.Source = this.gameObject;
			script.setLocation (hitzone);
		}
		
	}



	override
	public void Cast(){
	}


}