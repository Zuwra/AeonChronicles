using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivateObjAbil : Ability {


	private bool on;
	public float duration;



	private float timer;
	//private Selected select;
	public MultiShotParticle BoostEffect;
	//UnitManager myManager;
	public GameObject ThingToTurnOn;
	//private GameObject popUp;

	// Use this for initialization

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.activated;
	}

	void Start () {
		//select = GetComponent<Selected> ();
		//myManager = GetComponent<UnitManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (on && Time.time > timer) {

			Deactivate ();

		} 
	}

	public override void setAutoCast(bool offOn){
	}


	override
	public continueOrder canActivate (bool showError)
	{
		continueOrder order = new continueOrder ();

		if (myCost.canActivate (this) || on) {
			order.canCast = true;
		}
		return order;
	}

	override
	public void Activate()
	{

		if (!on) {
			if (myCost.canActivate (this)) {
				if (BoostEffect) {
					BoostEffect.continueEffect ();
				}
				myCost.payCost ();
				on = true;
				timer = Time.time + duration;
				ThingToTurnOn.SetActive (true);
			
			}
		} 


		//return true;//next unit should also do this.
	}

	public void Deactivate()
	{on = false;
		Debug.Log ("Deactivating");
		if (BoostEffect) {
			BoostEffect.stopEffect ();
		}
		ThingToTurnOn.SetActive (false);
	//	Destroy (popUp);

	}


}
