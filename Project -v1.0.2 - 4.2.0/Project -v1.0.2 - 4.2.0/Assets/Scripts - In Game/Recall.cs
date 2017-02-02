using UnityEngine;
using System.Collections;

public class Recall : Ability, Modifier {

	private GarataiManager garataiCenter;

	public float channelTime;
	private float timer;
	private bool isChanneling;

	override
	public  continueOrder canActivate(bool showError)
	{

		return new continueOrder();
	
	}


	override
	public  void Activate()
	{
		isChanneling = !isChanneling;

		timer = channelTime;


		//The units needs to be immobile while recalling

		//return true;
	}

	public override void setAutoCast(bool offOn){
	}

	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{
		if (isChanneling) {
			isChanneling = false;

		}
		return damage;
	}

	public void recall()
	{isChanneling = false;
		Vector3 recallLocation = garataiCenter.KeyWarpCore.transform.position;
		recallLocation.x += 25;
		recallLocation.y += 10;
		
		this.gameObject.transform.position = recallLocation;
	}

	// Use this for initialization
	void Start () {
		garataiCenter = GameObject.Find ("GameRaceManager").GetComponent<GarataiManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isChanneling) {
			timer-=Time.deltaTime;
			if(timer<=0)
				{
				recall();
			}
		}
	
	}
}
