using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserNode : Ability {

	public int maxCharges;
	private bool ableToFire;

	private float nextActionTime;

	private UnitManager myManager;
	private Selected select;

	private List<GameObject> toDestroy = new List<GameObject>();
	// Use this for initialization
	void Start () {
		nextActionTime = Time.time + .6f;
		myManager = GetComponent<UnitManager> ();
		select = GetComponent<Selected> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			if (nextActionTime < Time.time) {

				nextActionTime += .6f;
				if (chargeCount < maxCharges) {

					chargeCount++;
				
					if (select.IsSelected) {
						RaceManager.upDateUI ();
					}
				}
			}


			if (toDestroy.Count > 0 && chargeCount > 0) {
			
				GameObject obj = toDestroy [0];
		
				toDestroy.Remove (obj);
				Destroy (obj, .1f);

				chargeCount--;
			

				if (select.IsSelected) {
					RaceManager.upDateUI ();
				}
		
			}
		}
	}

	override
	public continueOrder canActivate(bool showError)
	{return new continueOrder();


	}
	public override void setAutoCast(bool offOn){
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Projectile") {
			

			Projectile proj = other.GetComponent<Projectile> ();
			if (proj.Source.GetComponent<UnitManager> ().PlayerOwner != myManager.PlayerOwner) {
				toDestroy.Add (other.gameObject);
			
				
				}
		}
	

	}


	override
	public void Activate()
	{
	}


}
