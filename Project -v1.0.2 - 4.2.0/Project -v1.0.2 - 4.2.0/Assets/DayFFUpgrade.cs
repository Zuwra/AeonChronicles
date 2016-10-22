using UnityEngine;
using System.Collections;

public class DayFFUpgrade : Upgrade {

	public GameObject mortarShot;
	public GameObject UltShot;

	bool appliedToUlt;

	override
	public void applyUpgrade (GameObject obj){
		mortarPod mPod = obj.GetComponent<mortarPod>();
		//Debug.Log ("Checking " + obj);
		if (mPod) {
			obj.GetComponent<IWeapon> ().projectile = mortarShot;
		}


		if (!appliedToUlt) {
			appliedToUlt = true;
			GameObject.FindObjectOfType<GameManager> ().GetComponent<Bombardment> ().Explosion = UltShot;
		
		}

	}

	public override void unApplyUpgrade (GameObject obj){

	}

}
