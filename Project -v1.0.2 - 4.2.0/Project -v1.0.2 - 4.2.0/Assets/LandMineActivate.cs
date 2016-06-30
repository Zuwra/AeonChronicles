using UnityEngine;
using System.Collections;

public class LandMineActivate : MonoBehaviour {

	public IWeapon explosion;
	
	public bool activateOnAll;//as opposed to activateOnEnemyOnly - will it blow up if ANYBODY 'steps' on it
	public int activationsRemaining = 5;// implement this later for multi-use mines


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		GameObject otherObj = other.gameObject;

		UnitManager otherManager = otherObj.GetComponent<UnitManager> ();
		if (otherManager) {

			bool activate = false;
			//if we should target them 
			if (activateOnAll || !otherManager.PlayerOwner.Equals (this.GetComponent<UnitManager> ().PlayerOwner)) {
				if (explosion.canAttack (otherObj)) {
					activate = true;
				}
			}

			if (activate) {
				explosion.attack (otherObj, null);
				Destroy (this.gameObject);
			}
		}

	}

}
