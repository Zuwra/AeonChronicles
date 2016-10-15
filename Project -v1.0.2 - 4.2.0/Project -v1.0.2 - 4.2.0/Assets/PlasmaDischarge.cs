using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlasmaDischarge : Ability {


	private bool on;
	public float duration;
	public float damagePerSecond;


	private float timer;
	//private Selected select;
	public MultiShotParticle BoostEffect;
	UnitManager myManager;

	private GameObject popUp;

	// Use this for initialization

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.activated;
	}

	void Start () {
		//select = GetComponent<Selected> ();
		myManager = GetComponent<UnitManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (on && Time.time > timer) {

			Deactivate ();

		} else if (on) {
			if (popUp) {
				popUp.GetComponentInChildren<Text> ().text = ""+(int)((duration - (timer - Time.time)) * damagePerSecond);
			}
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
				popUp = PopUpMaker.CreateGlobalPopUp ("0", Color.red, this.gameObject.transform.position,duration);
				popUp.transform.SetParent (this.gameObject.transform);

				}
			} else {

				if (timer - Time.time < (duration - 1)) {
					Deactivate ();
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
		foreach(GameObject obj in myManager.enemies)
		{
			UnitStats stat = obj.GetComponent<UnitStats> ();
			stat.TakeDamage ((duration -  (timer -Time.time )) * damagePerSecond, this.gameObject, DamageTypes.DamageType.Regular);
			PopUpMaker.CreateGlobalPopUp ("-" + (int)((duration - (timer - Time.time)) * damagePerSecond), Color.red, obj.transform.position);
		}
		Destroy (popUp);

	}


}
