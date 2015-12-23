using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class buildTurret :Ability{



	public UnitManager manager;
	public GameObject unitToBuild;

	public float buildTime;

	private float timer =0;
	private bool buildingUnit = false;
	public int numberOfTurrets;
	public bool autoPlaceTurrets;


	private List<TurretMount> turretMounts = new List<TurretMount>();

	// Use this for initialization
	void Start () {
		manager = GetComponent<UnitManager> ();

	}

	// Update is called once per frame
	void Update () {
		if (buildingUnit) {

			timer -= Time.deltaTime;
			if (timer <= 0) {
				buildingUnit = false;
				numberOfTurrets++;

			}
		}

		if (turretMounts.Count > 0) {
			if (numberOfTurrets > 0) {
				foreach (TurretMount obj in turretMounts) {
					if (numberOfTurrets > 0) {
						obj.placeTurret (createUnit ());
					}

				}
			}
	



		}
	}



	void OnTriggerEnter(Collider other)
	{
		//need to set up calls to listener components
		//this will need to be refactored for team games
		if (other.isTrigger) {
				return;}


		UnitManager manage = other.gameObject.GetComponent<UnitManager>();

			if (manage == null) {
			return;
		}

			if (manage.PlayerOwner == manager.PlayerOwner) {

				
				TurretMount mount = other.gameObject.GetComponentInChildren<TurretMount> ();
				if (mount && mount.turret == null) {
		
					if (autoPlaceTurrets) {
						turretMounts.Add (mount);


					}
				
				

			}
			

		}
	}




	public void cancelBuild ()
	{
		timer = 0;
		buildingUnit = false;
		myCost.refundCost ();
		GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().UnitDied(unitToBuild.GetComponent<UnitStats>().supply);
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
		}
		return true;//next unit should also do this.
	}





	public GameObject createUnit()
	{

		Vector3 location = new Vector3(this.gameObject.transform.position.x + 25,this.gameObject.transform.position.y+4,this.gameObject.transform.position.z);
		numberOfTurrets--;
	GameObject tur = (GameObject)Instantiate(unitToBuild, location, Quaternion.identity);
	return tur;
	}
}
