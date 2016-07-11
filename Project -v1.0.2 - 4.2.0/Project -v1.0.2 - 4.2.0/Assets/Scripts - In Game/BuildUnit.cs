using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildUnit : UnitProduction {




	private RaceManager racer;


	private Selected mySelect;

	public float buildTime;
	private BuildingInteractor myInteractor;

	private float timer =0;
	private bool buildingUnit = false;
	//private UnitManager manage;
	private HealthDisplay HD;
	private BuildManager buildMan;

	private int QueueNum;
	// Use this for initialization

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.activated;
	}



	void Start () {
		buildMan = GetComponent<BuildManager> ();
		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<GameManager> ().activePlayer;
		myInteractor = GetComponent <BuildingInteractor> ();
		mySelect = GetComponent<Selected> ();
		myCost.cooldown = buildTime;
		//manage = GetComponent<UnitManager> ();
		HD = GetComponentInChildren<HealthDisplay>();
	}
	
	// Update is called once per frame
	void Update () {
		if (buildingUnit) {

			timer -= Time.deltaTime;

			mySelect.updateCoolDown (1- timer/buildTime);
			if(timer <=0)
			{mySelect.updateCoolDown (0);
				
				buildingUnit = false;
				createUnit();
			}
		}
	
	}

	public override void setAutoCast(){}

	public override void DeQueueUnit()
	{myCost.refundCost ();
		//racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply, null);
	
	}

	public override float getProgress ()
	{return (1 - timer/buildTime);}


	public override void cancelBuilding ()
	{HD.stopBuilding ();
		mySelect.updateCoolDown (0);
		timer = 0;
		buildingUnit = false;
		foreach (Transform obj in this.transform) {

			obj.SendMessage ("DeactivateAnimation",SendMessageOptions.DontRequireReceiver);
		}
		//myCost.refundCost ();
		racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply,null);
		racer.stopBuildingUnit (this);
	}



	override
	public continueOrder canActivate (bool showError)
	{
		continueOrder order = new continueOrder();


		order.nextUnitCast = false;


		if (!myCost.canActivate (this, order, showError)) {
			order.canCast = false;
		}
		if (!active) {
			order.reasonList.Add (continueOrder.reason.requirement);
		}

		return order;


	}
	
	override
		public void Activate()
	{

		if (myCost.canActivate (this)) {


			if (buildMan.buildUnit (this)) {
				myCost.payCost();
				myCost.resetCoolDown ();

			
			}
		}

	}


	override
	public  void startBuilding()
	{

		foreach (Transform obj in this.transform) {

			obj.SendMessage ("ActivateAnimation", SendMessageOptions.DontRequireReceiver);
		}

		HD.loadIMage(unitToBuild.GetComponent<UnitStats> ().Icon);
		timer = buildTime;
		GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitCreated (unitToBuild.GetComponent<UnitStats> ().supply);

		buildingUnit = true;
		racer.buildingUnit (this);



	}



	public void createUnit()
	{
		HD.stopBuilding ();
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
		buildMan.unitFinished (this);
	
	//	manage.changeState (new DefaultState ());//.enQueueState (new DefaultState ());
		}




}
