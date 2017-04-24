using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildUnit : UnitProduction {

	private RaceManager racer;


	private Selected mySelect;

	private BuildingInteractor myInteractor;

	private float timer =0;
	private bool buildingUnit = false;
	//private UnitManager manage;
	private HealthDisplay HD;
	private BuildManager buildMan;


	private int QueueNum;

	[Tooltip("object that shows up while the unit is building, can be null")]
	public GameObject constObject;
	// Use this for initialization

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.activated;
	}



	void Start () {
		buildMan = GetComponent<BuildManager> ();
		racer = GameManager.main.activePlayer;
		myInteractor = GetComponent <BuildingInteractor> ();
		mySelect = GetComponent<Selected> ();

		HD = GetComponentInChildren<HealthDisplay>();
	}
	
	// Update is called once per frame
	void Update () {
		if (buildingUnit) {

			timer -= Time.deltaTime * buildRate;

			mySelect.updateCoolDown (1- timer/buildTime);
			if(timer <=0)
			{mySelect.updateCoolDown (0);
				
				buildingUnit = false;
				createUnit();
			}
		}
	
	}

	public bool isBuilding()
	{
		return buildingUnit;
	}

	public override void setAutoCast(bool offOn){}

	public override void DeQueueUnit()
	{myCost.refundCost ();
		PopUpMaker.CreateGlobalPopUp ("+" + myCost.ResourceOne, Color.white, this.transform.localPosition + Vector3.up * 8);
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
		//myCost.refundCost ();deq

		if (!buildMan.waitingOnSupply) {
	
			racer.UnitDied (unitToBuild.GetComponent<UnitStats> ().supply, null);
		}

		racer.stopBuildingUnit (this);
		if (constObject) {
			constObject.SetActive (false);
		}
	}



	override
	public continueOrder canActivate (bool showError)
	{
		
		continueOrder order = new continueOrder();


		order.nextUnitCast = false;


		if (myCost && !myCost.canActivate (this, order, showError)) {
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

				PopUpMaker.CreateGlobalPopUp ("-" + myCost.ResourceOne, Color.white, this.transform.localPosition + Vector3.up * 8);

			
			}
		}

	}


	override
	public  void startBuilding()
	{

		foreach (Transform obj in this.transform) {

			obj.SendMessage ("ActivateAnimation", SendMessageOptions.DontRequireReceiver);
		}
		if (constObject) {
			constObject.SetActive (true);
		}

		HD.loadIMage(unitToBuild.GetComponent<UnitStats> ().Icon);
		timer = buildTime;
		GameManager.main.activePlayer.UnitCreated (unitToBuild.GetComponent<UnitStats> ().supply);

		buildingUnit = true;
		racer.buildingUnit (this);



	}



	public void createUnit()
	{

		if (constObject) {
			constObject.SetActive (false);
		}
		HD.stopBuilding ();
		Vector3 location = new Vector3(this.gameObject.transform.position.x + 4,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z -7);

		GameObject unit = (GameObject)Instantiate(unitToBuild, location, Quaternion.identity);
		unit.transform.LookAt (location + Vector3.right + Vector3.back);
		UnitManager unitMan = unit.GetComponent<UnitManager> ();
		unitMan.setInteractor();
		unitMan.interactor.initialize ();
		if (myInteractor != null) {

			//Sends units outside of the Construction yard, so it looks like they were built inside.
			unitMan.GiveOrder (Orders.CreateMoveOrder(new Vector3(this.gameObject.transform.position.x +10,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z -16)));
			//Debug.Log ("ordering to move " + new Vector3(this.gameObject.transform.position.x +10,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z -16));
			//Queue a command if they have a rally point or unit
			if (myInteractor.rallyUnit != null) {
				
				unitMan.GiveOrder (Orders.CreateFollowCommand(myInteractor.rallyUnit,true));
			} 
			else if (myInteractor.rallyPoint != Vector3.zero) {
				unitMan.GiveOrder (Orders.CreateMoveOrder (myInteractor.rallyPoint,true));

			//	Debug.Log ("Giving Rally Command");
			}
			timer = buildTime;
		}

		foreach (Transform obj in this.transform) {

			obj.SendMessage ("DeactivateAnimation",SendMessageOptions.DontRequireReceiver);
		}
		racer.stopBuildingUnit (this);
		//racer.applyUpgrade (unit);
		buildingUnit = false;
		buildMan.unitFinished (this);
	
	//	manage.changeState (new DefaultState ());//.enQueueState (new DefaultState ());
		}




}
