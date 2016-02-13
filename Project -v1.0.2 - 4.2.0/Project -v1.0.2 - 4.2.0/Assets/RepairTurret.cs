using UnityEngine;
using System.Collections;

public class RepairTurret : Ability, Modifier{


	public int maxRepair = 600;
	private UnitManager mymanager;

	private UnitStats TargetHealth;
	private GameObject target;

	public int repairRate = 8;
	private float nextActionTime;
	private repairReturn returner;
	// Use this for initialization
	void Start () {
		mymanager = gameObject.transform.parent.GetComponentInParent<UnitManager> ();
		chargeCount = maxRepair;
		nextActionTime = Time.time;
		mymanager.myStats.addDeathTrigger (this);
	}

	public override void setAutoCast(){
	}


	// Update is called once per frame
	void Update () {

		if (chargeCount <= 0) {
			
			return;}

		if (mymanager.isIdle ()) {

			if (!target) {
				if (mymanager.allies.Count > 0) {
					
					target = findHurtAlly ();

					if (target) {

						mymanager.changeState (new FollowState (target, null, null, null));
						TargetHealth = target.GetComponent<UnitStats> ();

						if (returner == null) {
							returner = gameObject.transform.parent.GetComponentInParent<repairReturn> ();
						}
					}
				}
			} else {
				mymanager.changeState (new FollowState (target, null, null, null));
			}
		} 



		if (target) {
			if(Time.time > nextActionTime){
				if (Vector3.Distance (this.gameObject.transform.position, target.transform.position) < 18) {
				
					nextActionTime += 1;
					TargetHealth.health += repairRate;
					returner.chargeCount -= repairRate;
					chargeCount -= repairRate;
					RaceManager.upDateUI ();
					if (TargetHealth.atFullHealth ()) {
						target = null;
						TargetHealth = null;
					}

				}
			}
		
		}
	}



	private GameObject findHurtAlly()
	{

			GameObject best = null;
			float distance = 1000000;


		for (int i = 0; i <mymanager.allies.Count; i ++) {
			if (mymanager.allies[i] != null) {
				if(!mymanager.allies[i].GetComponent<UnitStats>().atFullHealth()){

					float currDistance = Vector3.Distance(mymanager.allies[i].transform.position, this.gameObject.transform.position);
					if(currDistance < distance)
						{best = mymanager.allies[i];

						distance = currDistance;
						}
					}
				}
			}
			return best;

		}




	public bool validate(GameObject source, GameObject target)
	{if (!active) {
			return false;}
		if (chargeCount > 0) {
			return true;
		}
		return false;
	}




	public void trigger(GameObject source, GameObject projectile,GameObject target)	{



	}

	override
	public continueOrder canActivate()
	{continueOrder order = new continueOrder();

		return order;


	}


	override
	public void Activate()
	{
		

	}




	public float modify(float damage, GameObject source)
	{ 

		foreach (TurretMount turr in transform.parent.GetComponentsInParent<TurretMount> ()) {

			if (turr.turret != null) {
				mymanager.myWeapon = turr.turret.GetComponent<IWeapon> ();
				return 0 ;
			}

		}
		return 0 ;

	}



}
