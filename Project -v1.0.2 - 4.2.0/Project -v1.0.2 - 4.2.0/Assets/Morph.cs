using UnityEngine;
using System.Collections;

public class Morph :  UnitProduction {




	private Selected mySelect;

	private BuildingInteractor myInteractor;

	private UnitManager myManager;
	private RaceManager racer;

	//public float buildTime;
	private float timer =0;
	private bool Morphing = false;
	private HealthDisplay HD;
	private BuildManager buildMan;

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

	public override void setAutoCast(bool offOn){}

	public override void DeQueueUnit()
	{
		myCost.refundCost ();

	}


	public override void cancelBuilding ()
	{	HD.stopBuilding ();
		mySelect.updateCoolDown (0);
		timer = 0;
		Morphing = false;
		myCost.refundCost ();
		racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply, null);
		racer.stopBuildingUnit (this);
		myManager.setStun (false, this, false);
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
				GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitCreated (unitToBuild.GetComponent<UnitStats> ().supply);
				Morphing = true;
				racer.buildingUnit (this);
				myManager.changeState (new ChannelState ());
			myManager.setStun (true, this, false);
			if (mySelect.IsSelected) {
				SelectedManager.main.updateUI ();
			}
		} 
		
		//return true;//next unit should also do this.
	}



	public override void startBuilding(){}

	public void createUnit()
	{HD.stopBuilding ();


		Vector3 posit = new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y+5,gameObject.transform.position.z);
		GameObject unit = (GameObject)Instantiate(unitToBuild, posit, Quaternion.identity);

		UnitManager tempManage = unit.GetComponent<UnitManager> ();
		tempManage.setInteractor();
		tempManage.interactor.initialize ();

		if (myInteractor != null) {
			if (myInteractor.rallyUnit != null) {

				tempManage.GiveOrder (Orders.CreateInteractCommand(myInteractor.rallyUnit));
			} 
			else if (myInteractor.rallyPoint != Vector3.zero) {
				tempManage.GiveOrder (Orders.CreateMoveOrder (myInteractor.rallyPoint));
			}
		}
		racer.stopBuildingUnit (this);
		RaceManager.removeUnitSelect (myManager);
		GameManager.main.playerList[myManager.PlayerOwner-1].UnitDying (myManager, null, false);
		unit.GetComponent<Selected> ().Initialize ();
	

		if (myManager.GetComponent<Selected> ().IsSelected) {
			SelectedManager.main.AddObject (unit.GetComponent<UnitManager>());
		}
		tempManage.myStats.SetHealth(myManager.myStats.health/myManager.myStats.Maxhealth );
			Morphing = false;
		myManager.myStats.kill (this.gameObject);
		//Destroy (this.gameObject);
	}



}
