using UnityEngine;
using System.Collections;

public class BuildStructure:  UnitProduction {


	private Selected mySelect;

	private BuildingInteractor myInteractor;

	private UnitManager myManager;
	private RaceManager racer;

	public float buildTime;
	private float timer =0;
	private bool Morphing = false;
	private HealthDisplay HD;
	private BuildManager buildMan;
	Vector3 targetLocation;

	void Awake()
	{audioSrc = GetComponent<AudioSource> ();
		myType = type.building;
	}


	// Use this for initialization
	void Start () {
		buildMan = GetComponent<BuildManager> ();
		myManager = this.gameObject.GetComponent<UnitManager> ();
		myInteractor = GetComponent <BuildingInteractor> ();
		mySelect = GetComponent<Selected> ();
		myCost.cooldown = buildTime;
		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ();
		HD = GetComponentInChildren<HealthDisplay>();
	}

	// Update is called once per frame
	void Update () {
		if (Morphing) {


			timer -= Time.deltaTime;
			mySelect.updateCoolDown (1 - timer/buildTime);
			if(timer <=0)
			{mySelect.updateCoolDown (0);
				HD.stopBuilding ();
				Morphing = false;
				createUnit();
			}
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
		timer = 0;
		Morphing = false;
		//myCost.refundCost ();
		//racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply, null);
		racer.stopBuildingUnit (this);
		myManager.setStun (false, this);
		myManager.changeState(new DefaultState());

		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}
	}

	public override float getProgress ()
	{return (1 - timer/buildTime);}

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
			timer = buildTime;

			Morphing = true;
			racer.buildingUnit (this);
			myManager.changeState (new ChannelState ());
			myManager.setStun (true, this);
			if (mySelect.IsSelected) {
				SelectedManager.main.updateUI ();
			}
		} 

		//return true;//next unit should also do this.
	}



	public override void startBuilding(){}

	public void createUnit()
	{HD.stopBuilding ();

		timer = 0;
		mySelect.updateCoolDown (0);
		GameObject unit = (GameObject)Instantiate(unitToBuild, targetLocation, Quaternion.identity);
		GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitCreated (unitToBuild.GetComponent<UnitStats> ().supply);
		UnitManager tempManage = unit.GetComponent<UnitManager> ();
		tempManage.setInteractor();
		tempManage.interactor.initialize ();

		myManager.setStun (false, this);
		myManager.changeState(new DefaultState());
		racer.stopBuildingUnit (this);

		unit.GetComponent<Selected> ().Initialize ();


		buildMan.unitFinished (this);
		Morphing = false;
		if (mySelect.IsSelected) {
			SelectedManager.main.updateUI ();
		}

	}



}
