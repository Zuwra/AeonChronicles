using UnityEngine;
using System.Collections;

public class BuildUnit :  Ability {



	private RaceManager racer;

	public GameObject unitToBuild;
	private Selected mySelect;

	public float buildTime;
	private BuildingInteractor myInteractor;

	private float timer =0;
	private bool buildingUnit = false;
	// Use this for initialization
	void Start () {

		racer = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<GameManager> ().activePlayer;
		myInteractor = GetComponent <BuildingInteractor> ();
		mySelect = GetComponent<Selected> ();
		myCost.cooldown = buildTime;
	
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

	public void cancelBuild ()
		{
		timer = 0;
		buildingUnit = false;
		myCost.refundCost ();
		racer.UnitDied(unitToBuild.GetComponent<UnitStats>().supply);
	}



	override
		public bool canActivate ()
	{
		if (buildingUnit) {

			return false;}
	
		return myCost.canActivate ();

	}
	
	override
		public bool Activate()
	{
		if (myCost.canActivate ()) {

			myCost.payCost();


			timer = buildTime;
			GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().UnitCreated(unitToBuild.GetComponent<UnitStats>().supply);
			buildingUnit = true;
			return false;
		}
		return true;//next unit should also do this.
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

		racer.applyUpgrade (unit);
		buildingUnit = false;
	}



}
