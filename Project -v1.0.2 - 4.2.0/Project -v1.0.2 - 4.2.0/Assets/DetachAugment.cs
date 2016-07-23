using UnityEngine;
using System.Collections;

public class DetachAugment : Ability {

	Augmentor myAugmentor;
	UnitManager manager;

	// Use this for initialization
	void Start () {
		myType = type.activated;
		myAugmentor = GetComponent<Augmentor> ();
		manager = GetComponent<UnitManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void allowDetach(bool canDoit)
	{
		active = canDoit;
		if (GetComponent<Selected> ().IsSelected) {
			RaceManager.updateActivity ();
		}

	}



	public override continueOrder canActivate(bool error){
		continueOrder ord = new continueOrder ();
		ord.canCast = active;
		ord.nextUnitCast = false;
		return ord;

	}

	public override void Activate(){
		
		myAugmentor.Unattach ();
		RaycastHit objecthit;

		Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

		if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000, (~8))) {

			down =objecthit.point;

		}
		manager.changeState (new MoveState (down, manager));
		active = false;
		if (GetComponent<Selected> ().IsSelected) {
			Debug.Log ("Updating UIIIII");
			RaceManager.updateActivity ();
		}
		
	}  // returns whether or not the next unit in the same group should also cast it

	public override void setAutoCast(){}
}
