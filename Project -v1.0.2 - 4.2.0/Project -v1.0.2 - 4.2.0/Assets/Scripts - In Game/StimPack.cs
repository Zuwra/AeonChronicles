using UnityEngine;
using System.Collections;

public class StimPack : Ability {


	private bool on;
	public float duration;
	public float speedBoost;

	private float timer;

	// Use this for initialization
	void Start () {description = "Uses life to give a short burst of speed";
	
	}
	
	// Update is called once per frame
	void Update () {
		if (on && timer > 0) {
			timer -= Time.deltaTime;
		
			if (timer <= 0) {
				Deactivate ();
			}
		} 

	}

	override
	public bool canActivate ()
		{return myCost.canActivate ();
		}

	override
	public bool Activate()
	{Debug.Log ("Activating stim");
		if (myCost.canActivate ()) {

			if(!on)
			{
			this.gameObject.GetComponent<customMover>().MaxSpeed +=speedBoost;
			
			}
			myCost.payCost();
			on = true;
			timer = duration;

		
		}
		return true;//next unit should also do this.
	}

	public void Deactivate()
	{on = false;
		timer = 0;

		this.gameObject.GetComponent<customMover>().MaxSpeed -=speedBoost;

	}


}
