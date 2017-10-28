using UnityEngine;
using System.Collections;

public class ThrasherBlade : Projectile {

	// Use this for initialization
	public float rollTime = 5;
	float trueROlltime;
	bool goingOut = true;
	Vector3 Origin;
	Vector3 EndTarget;
	RaycastHit objecthit;

	Vector3 dir;
	public override void setup()
	{base.Start ();
		Origin = this.transform.position;
		EndTarget = target.transform.position;
		dir = (EndTarget - transform.position);
		dir.y = 0;

		lastLocation = target.transform.position -this.gameObject.transform.position ;
		lookAtTarget ();
		trueROlltime = rollTime;
	}

	protected override void Update () {

		Vector3 tempDir = dir;
		//Make sure your the right height above the terrain

		if (Physics.Raycast (this.gameObject.transform.position + Vector3.up * 4, Vector3.down, out objecthit, 100, (~8))) {
			float h = Vector3.Distance (this.gameObject.transform.position, objecthit.point);
			if (h < 2f || h > 4) {

				tempDir.y -=   (this.gameObject.transform.position.y -(objecthit.point.y + 3f) ) *speed *8;
			}
		}


		tempDir.Normalize ();
		tempDir *= speed * Time.deltaTime;

		this.gameObject.transform.Translate (tempDir,Space.World);



		rollTime -= Time.deltaTime;
		if (rollTime <0) {
			
			if (goingOut) {
				dir = (Origin - this.transform.position);
				dir.y = 0;
				foreach (rotater rot in GetComponentsInChildren<rotater>()) {
					rot.speed *=-1; 
				}
				rollTime = trueROlltime;
				goingOut = false;
			}
			else{
				Destroy (this.gameObject);}


			}

		}
		



	void OnControllerColliderHit(ControllerColliderHit other)
	{
	}
	new void OnTriggerEnter(Collider other)
	{}

	 void OnTriggerExit(Collider other)
	{}
}
