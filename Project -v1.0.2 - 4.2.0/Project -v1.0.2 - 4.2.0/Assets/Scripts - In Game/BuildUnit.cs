using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildUnit : UnitProduction {



	private RaceManager racer;

	public GameObject unitToBuild;
	private Selected mySelect;

	public float buildTime;
	private BuildingInteractor myInteractor;

	private float timer =0;
	private bool buildingUnit = false;
	private UnitManager manage;

	private int QueueNum;
	// Use this for initialization
	void Start () {

		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<GameManager> ().activePlayer;
		myInteractor = GetComponent <BuildingInteractor> ();
		mySelect = GetComponent<Selected> ();
		myCost.cooldown = buildTime;
		manage = GetComponent<UnitManager> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (buildingUnit) {

			timer -= Time.deltaTime;

			mySelect.updateCoolDown (1 - timer/buildTime);
			if(timer <=0)
			{mySelect.updateCoolDown (0);
				buildingUnit = false;
				createUnit();
			}
		}
	
	}

	public override void setAutoCast(){}


	public override float getProgress ()
	{return (1 - timer/buildTime);}


	public void cancelBuild ()
		{
		timer = 0;
		buildingUnit = false;
		myCost.refundCost ();
		racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply);
	}



	override
	public continueOrder canActivate ()
	{
		continueOrder order = new continueOrder();


		order.nextUnitCast = false;


		if (!myCost.canActivate ()) {
			order.canCast = true;
		}
		return order;


	}
	
	override
		public void Activate()
	{//Debug.Log ("casting " + QueueNum);

		if (myCost.canActivate ()) {
			Debug.Log ("Queue another unit" + manage.checkNextState() + "   " +manage.getStateCount() );
			//if(manage.getStateCount > 0){
				
			
			//	manage.enQueueState (new CastAbilityState (this));
			//} else {
				myCost.payCost();
			
				manage.enQueueState (new ChannelState ());
				//start animations in any children
				foreach (Transform obj in this.transform) {
				
					obj.SendMessage ("ActivateAnimation", SendMessageOptions.DontRequireReceiver);
				}

				timer = buildTime;

				buildingUnit = true;
				racer.buildingUnit (this);
				GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitCreated (unitToBuild.GetComponent<UnitStats> ().supply);
		//	}
		//	return false;
		}

	}


	public void createUnit()
	{
		
		Vector3 location = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z -20);
		
		GameObject unit = (GameObject)Instantiate(unitToBuild, location, Quaternion.identity);

	
		unit.GetComponent<UnitManager>().setInteractor();
		unit.GetComponent<UnitManager> ().interactor.initialize ();
		if (myInteractor != null) {
			if (myInteractor.rallyUnit != null) {

				unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateFollowCommand(myInteractor.rallyUnit));
			} 
			else if (myInteractor.rallyPoint != Vector3.zero) {
				unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateMoveOrder (myInteractor.rallyPoint));
			}
		}

		foreach (Transform obj in this.transform) {

			obj.SendMessage ("DeactivateAnimation",SendMessageOptions.DontRequireReceiver);
		}
		racer.stopBuildingUnit (this);
		racer.applyUpgrade (unit);
		buildingUnit = false;

	
		manage.changeState (new DefaultState ());//.enQueueState (new DefaultState ());
		}




}
