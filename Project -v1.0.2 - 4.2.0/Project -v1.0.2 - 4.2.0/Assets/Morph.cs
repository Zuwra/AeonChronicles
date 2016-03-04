using UnityEngine;
using System.Collections;

public class Morph :  UnitProduction {



	public GameObject unitToBuild;
	private Selected mySelect;

	private BuildingInteractor myInteractor;

	private UnitManager myManager;
	private RaceManager racer;

	public float buildTime;
	private float timer =0;
	private bool Morphing = false;


	// Use this for initialization
	void Start () {
		myManager = this.gameObject.GetComponent<UnitManager> ();
		myInteractor = GetComponent <BuildingInteractor> ();
		mySelect = GetComponent<Selected> ();
		myCost.cooldown = buildTime;
		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ();

	}

	// Update is called once per frame
	void Update () {
		if (Morphing) {


			timer -= Time.deltaTime;
			mySelect.updateCoolDown (1 - timer/buildTime);
			if(timer <=0)
			{mySelect.updateCoolDown (0);
				Morphing = false;
				createUnit();
			}
		}

	}

	public override void setAutoCast(){}

	public override void DeQueueUnit()
	{
		myCost.refundCost ();

	}


	public override void cancelBuilding ()
	{	mySelect.updateCoolDown (0);
		timer = 0;
		Morphing = false;
		myCost.refundCost ();
		racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply);
		racer.stopBuildingUnit (this);
	}

	public override float getProgress ()
	{return (1 - timer/buildTime);}

	override
	public continueOrder canActivate ()
		{continueOrder order = new continueOrder();
		if (Morphing) {
			
			order.nextUnitCast = true;
			order.canCast = false;
			return order;
		}

		if (!myCost.canActivate (this, order)) {
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
			 

				myCost.payCost ();


				timer = buildTime;
				GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitCreated (unitToBuild.GetComponent<UnitStats> ().supply);
				Morphing = true;
				racer.buildingUnit (this);
				myManager.changeState (new ChannelState ());

		} 
		
		//return true;//next unit should also do this.
	}



	public override void startBuilding(){}

	public void createUnit()
	{


		Vector3 posit = new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y+5,gameObject.transform.position.z);
		GameObject unit = (GameObject)Instantiate(unitToBuild, posit, Quaternion.identity);


		unit.GetComponent<UnitManager>().setInteractor();
		unit.GetComponent<UnitManager> ().interactor.initialize ();
		if (myInteractor != null) {
			if (myInteractor.rallyUnit != null) {

				unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateInteractCommand(myInteractor.rallyUnit));
			} 
			else if (myInteractor.rallyPoint != Vector3.zero) {
				unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateMoveOrder (myInteractor.rallyPoint));
			}
		}
		racer.stopBuildingUnit (this);
		Morphing = false;
		Destroy (this.gameObject);
	}



}
