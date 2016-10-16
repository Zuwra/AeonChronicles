using UnityEngine;
using System.Collections;

public class RepairTurret : Ability, Modifier{


	private UnitManager mymanager;


	private GameObject target;

	public MultiShotParticle particleEff;
	public int repairRate = 8;
	private float nextActionTime;
	private repairReturn returner;

	public GameObject drone;
	RepairDrone droneScript;
	bool DroneAway;
	bool commandRepair;

	// Use this for initialization
	void Start () {
		mymanager = gameObject.transform.parent.GetComponentInParent<UnitManager> ();
		//chargeCount = maxRepair;
		nextActionTime = Time.time;
		mymanager.myStats.addDeathTrigger (this);
		droneScript = drone.GetComponent<RepairDrone> ();
		droneScript.repairRate = repairRate;
	
	}
	public UnitManager getManager()
	{
		return mymanager;
	}

	public override void setAutoCast(bool offOn){
	}


	// Update is called once per frame
	void Update () {

		//Debug.Log ("Mystate is " + mymanager.getState() + "  " + DroneAway);
		if (Time.time > nextActionTime && !DroneAway) {
			nextActionTime = Time.time + 1;

			if (mymanager.getState () is MoveState) {
				if (!((MoveState)mymanager.getState ()).assumedMove) {

					return;
				}
			}
		
			if(target &&  Vector3.Distance (target.transform.position, this.gameObject.transform.position) < 40)
			{
				mymanager.cMover.stop ();
				mymanager.changeState (new DefaultState());
				droneScript.setTarget (target);
				DroneAway = true;
				drone.transform.SetParent (null);

			}

			else if (!target || (Vector3.Distance (target.transform.position, this.gameObject.transform.position) > 65 && !commandRepair)) {
				
				
				if (mymanager.allies.Count > 0) {
					
					target = findHurtAlly ();
				
					commandRepair = false;
					if (target) {
				
						mymanager.changeState (new FollowState (target, null));
							

					} 
				} 
			} else if (target){
				
					mymanager.changeState (new FollowState (target, null));

				}
			

		}

		if (target && !DroneAway) {
			
				if (Vector3.Distance (this.gameObject.transform.position, target.transform.position) < 40) {
					mymanager.cMover.stop ();
					mymanager.changeState (new DefaultState());
					droneScript.setTarget (target);
					DroneAway = true;
					drone.transform.SetParent (null);
				//Debug.Log ("Stopping " + mymanager);
			


			}
			}


	}

	public void possibleStop()
	{

		if (mymanager.getState () is MoveState) {
			if (!((MoveState)mymanager.getState ()).assumedMove) {

				return;
			}
		}
		mymanager.changeState (new DefaultState());
	}


	public void setTarget(GameObject obj)
	{commandRepair = true;

		target = obj;
		DroneAway = false;
		if (Vector3.Distance (target.transform.position, this.gameObject.transform.position) < 40) {
			mymanager.cMover.stop ();
			mymanager.changeState (new DefaultState ());
			droneScript.setTarget (target);
			DroneAway = true;
			drone.transform.SetParent (null);

		} else {
			mymanager.changeState (new FollowState (target, null));
		}

	
	}

	public void doneRepairing(GameObject t)
	{if (t == target) {
			target = null;
			commandRepair = false;
		}
	
		DroneAway = false;

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

		return false;
	}




	public void trigger(GameObject source, GameObject projectile,GameObject target)	{



	}

	override
	public continueOrder canActivate(bool showError)
	{continueOrder order = new continueOrder();

		return order;


	}


	override
	public void Activate()
	{
		

	}




	public float modify(float damage, GameObject source)
	{ 

		Destroy (drone);
		//foreach (TurretMount turr in transform.parent.GetComponentsInParent<TurretMount> ()) {

			//if (turr.turret != null) {

				// What does this do again?
				//mymanager.myWeapon= (turr.turret.GetComponent<IWeapon> ());
				//return 0 ;
			//}

		//}
		return 0 ;

	}



}
