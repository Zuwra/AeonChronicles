using UnityEngine;
using System.Collections;

public class ThrasherBlade : Projectile {

	// Use this for initialization
	public float rollTime = 5;
	float trueROlltime;
	bool goingOut = true;
	void Start()
	{
		base.Start ();
		lastLocation = target.transform.position -this.gameObject.transform.position ;


		trueROlltime = rollTime;
	}

	void Update () {
		


		Vector3 dir;
		if (!goingOut) {
			dir = (transform.position-lastLocation);
		} else {
			dir = (transform.position- lastLocation )*-1;
		}

		//Make sure your the right height above the terrain
		RaycastHit objecthit;
		Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

		if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, (~8))) {
			float h = Vector3.Distance (this.gameObject.transform.position, objecthit.point);
			if (h < 2f || h > 4) {

				dir.y -=   (this.gameObject.transform.position.y -(objecthit.point.y + 3f) ) *speed *8;
			}


		}
		dir.Normalize ();
		dir *= speed * Time.deltaTime ;

		dir = Quaternion.AngleAxis (-90, Vector3.up) * dir;
		this.gameObject.transform.Translate (dir);


		rollTime -= Time.deltaTime;
		if (rollTime <0) {
			
			if (goingOut) {
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
	void OnTriggerEnter(Collider other)
	{
	}

}
