using UnityEngine;
using System.Collections;

public class BuildStructure:  UnitProduction {


	private Selected mySelect;

	//private BuildingInteractor myInteractor;

	private UnitManager myManager;
	private RaceManager racer;

	public float buildTime;

	private bool Morphing = false;
	private HealthDisplay HD;
	private BuildManager buildMan;
	Vector3 targetLocation;

	private UnitManager inConstruction;
	private BuildingInteractor builder;

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.building;
	}


	// Use this for initialization
	void Start () {
		buildMan = GetComponent<BuildManager> ();
		myManager = this.gameObject.GetComponent<UnitManager> ();
		//myInteractor = GetComponent <BuildingInteractor> ();
		mySelect = GetComponent<Selected> ();
		myCost.cooldown = buildTime;
		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ();
		HD = GetComponentInChildren<HealthDisplay>();
	}

	// Update is called once per frame
	void Update () {
		if (Morphing) {



			float percent = builder.construct (Time.deltaTime / buildTime);
			if (percent >= 1) {
				mySelect.updateCoolDown (0);
				HD.stopBuilding ();
				Morphing = false;
				createUnit ();
			} else {
				mySelect.updateCoolDown (percent);

			}
			//inConstruction.myStats.heal (inConstruction.myStats.Maxhealth * Time.deltaTime/ buildTime);

			
		}

	}

	public void cancel()
	{
		mySelect.updateCoolDown (0);
		HD.stopBuilding ();
		Morphing = false;
		myManager.setStun (false, this);
		myManager.changeState(new DefaultState());
		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}
	}

	public override void setAutoCast(){}

	public void setBuildSpot(Vector3 buildSpot)
	{targetLocation = buildSpot;
	}


	public override void DeQueueUnit()
	{
		myCost.refundCost ();

	}


	public override void cancelBuilding ()
	{	HD.stopBuilding ();
		mySelect.updateCoolDown (0);

		Morphing = false;
		//myCost.refundCost ();
		//racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply, null);
		racer.stopBuildingUnit (this);
		myManager.setStun (false, this);
		myManager.changeState(new DefaultState());

		Destroy (inConstruction.gameObject);

		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}
	}

	public override float getProgress ()
	{return builder.getProgess();}

	override
	public continueOrder canActivate (bool showError)
	{

		continueOrder order = new continueOrder();
		if (Morphing) {

			order.nextUnitCast = true;
			order.canCast = false;
			return order;
		}

		if (!myCost.canActivate (this, order,showError)) {
			order.canCast = false;
		}
		if (!active) {
			order.reasonList.Add (continueOrder.reason.requirement);
		}

		order.nextUnitCast = false;
		return order;
	}

	override
	public void Activate()
	{if (!Morphing) {
			HD.loadIMage (iconPic);

	
			//Debug.Log ("Activating");
			myCost.payCost ();
			buildMan.buildUnit (this);
			myManager.cMover.stop ();


			Morphing = true;
			racer.buildingUnit (this);
			myManager.changeState (new ChannelState ());
			myManager.setStun (true, this);
			if (mySelect.IsSelected) {
				SelectedManager.main.updateUI ();
			}
			inConstruction = ((GameObject)Instantiate(unitToBuild, targetLocation + Vector3.up, Quaternion.identity)).GetComponent<UnitManager>();
			builder = inConstruction.GetComponent<BuildingInteractor> ();
			builder.startConstruction (unitToBuild);
			/*
			foreach (Ability ab in inConstruction.abilityList) {
				ab.active = false;
				//ab.enabled = false;
			}*/
			inConstruction.setInteractor();
			inConstruction.interactor.initialize ();
			inConstruction.GetComponent<Selected> ().Initialize ();
			inConstruction.myStats.SetHealth (.02f);


		} 

		//return true;//next unit should also do this.
	}



	public override void startBuilding(){}

	public void createUnit()
	{HD.stopBuilding ();

	
		mySelect.updateCoolDown (0);
		//GameObject unit = (GameObject)Instantiate(unitToBuild, targetLocation, Quaternion.identity);

		//UnitManager tempManage = unit.GetComponent<UnitManager> ();
		//tempManage.setInteractor();
		//tempManage.interactor.initialize ();
		GameManager.main.playerList[myManager.PlayerOwner-1].UnitCreated(unitToBuild.GetComponent<UnitStats> ().supply);
		myManager.setStun (false, this);
		myManager.changeState(new DefaultState());
		racer.stopBuildingUnit (this);
		/*
		UnitManager template = unitToBuild.GetComponent<UnitManager> ();
		for (int i = 0; i < inConstruction.abilityList.Count; i++) {
	
			if (template.abilityList [i].active) {
				inConstruction.abilityList [i].active = true;
			}
			if (template.abilityList [i].enabled) {

			inConstruction.abilityList [i].enabled = true;
		}
		}*/
		//unit.GetComponent<Selected> ().Initialize ();


		buildMan.unitFinished (this);
		Morphing = false;
		if (inConstruction.GetComponent<Selected>().IsSelected || GetComponent<Selected>().IsSelected){
			SelectedManager.main.updateUI ();
		}
		inConstruction = null;

	}

	public void resumeBuilding(GameObject obj)
	{
		
		HD.loadIMage (iconPic);


		//Debug.Log ("Activating");

		buildMan.buildUnit (this);
		myManager.cMover.stop ();


		Morphing = true;

		myManager.changeState (new ChannelState ());
		myManager.setStun (true, this);
		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}
		inConstruction = obj.GetComponent<UnitManager>();
		builder = inConstruction.GetComponent<BuildingInteractor> ();
	
	}



}
