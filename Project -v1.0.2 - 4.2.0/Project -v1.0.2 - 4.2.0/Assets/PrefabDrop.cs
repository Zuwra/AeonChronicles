using UnityEngine;
using System.Collections;

public class PrefabDrop : TargetAbility{

	private RaceManager racer;
	public GameObject prefab;
	// Use this for initialization
	void Start () {
		racer = GameObject.FindObjectOfType<GameManager> ().activePlayer;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	override
	public continueOrder canActivate(){

		continueOrder order = new continueOrder ();

		if (!myCost.canActivate (this)) {
			order.canCast = false;
		} else {
			order.nextUnitCast = false;
		}
		return order;
	}

	override
	public void Activate()
	{
	}  // returns whether or not the next unit in the same group should also cast it


	override
	public  void setAutoCast(){}




	override
	public  bool Cast(GameObject target, Vector3 location)
	{



	//	myCost.payCost ();

		GameObject proj = null;

		Vector3 loc1 = location;
		loc1.x += 9;
		proj = (GameObject)Instantiate (prefab, loc1, Quaternion.identity);
		proj.GetComponent<UnitManager>().setInteractor();
		proj.GetComponent<UnitManager> ().interactor.initialize ();
		racer.applyUpgrade (proj);

		Vector3 loc2 = location;
		loc2.x -= 9;
		proj = (GameObject)Instantiate (prefab, loc2, Quaternion.identity);
		proj.GetComponent<UnitManager>().setInteractor();
		proj.GetComponent<UnitManager> ().interactor.initialize ();
		racer.applyUpgrade (proj);

		Vector3 loc3 = location;
		loc3.z += 9;
		proj = (GameObject)Instantiate (prefab, loc3, Quaternion.identity);
		proj.GetComponent<UnitManager>().setInteractor();
		proj.GetComponent<UnitManager> ().interactor.initialize ();
		racer.applyUpgrade (proj);

		Vector3 loc4 = location;
		loc4.z -= 9;
		proj = (GameObject)Instantiate (prefab, loc4, Quaternion.identity);
		proj.GetComponent<UnitManager>().setInteractor();
		proj.GetComponent<UnitManager> ().interactor.initialize ();
		racer.applyUpgrade (proj);


		return false;

	}



	override
	public void Cast(){



		//myCost.payCost ();

		GameObject proj = null;

		Vector3 pos = this.gameObject.transform.position;
	
		proj = (GameObject)Instantiate (prefab, pos, Quaternion.identity);
	



	}


}