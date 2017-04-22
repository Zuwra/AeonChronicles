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
	public continueOrder canActivate(bool showError){

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
	public  void setAutoCast(bool offOn){}

	public override bool isValidTarget (GameObject target, Vector3 location){
		if (target == null) {
			return true;
		}

		return (!target.GetComponent<UnitManager>());

	}



	override
	public  bool Cast(GameObject target, Vector3 location)
	{

		myCost.payCost ();


	//	myCost.payCost ();

		GameObject proj = null;

		Vector3 loc1 = location;
		loc1.x += 9;
		loc1.y += 150;
		proj = (GameObject)Instantiate (prefab, loc1, Quaternion.identity);

		UnitManager tempMan = proj.GetComponent<UnitManager> ();

		tempMan.setInteractor();
		tempMan.interactor.initialize ();
		racer.applyUpgrade (tempMan);
		proj.AddComponent<SpaceDrop> ();
		proj.GetComponent<SpaceDrop> ().speed = 3;
		loc1.y -= 150;
		proj.GetComponent<SpaceDrop> ().setLocation (loc1);

		Vector3 loc2 = location;
		loc2.x -= 9;
		loc2.y += 150;
		proj = (GameObject)Instantiate (prefab, loc2, Quaternion.identity);
		tempMan = proj.GetComponent<UnitManager> ();
		tempMan.setInteractor();
		tempMan.interactor.initialize ();
		racer.applyUpgrade (tempMan);
		proj.AddComponent<SpaceDrop> ();
		proj.GetComponent<SpaceDrop> ().speed = 3;
		loc2.y -= 150;
		proj.GetComponent<SpaceDrop> ().setLocation (loc2);

		Vector3 loc3 = location;
		loc3.z += 9;
		loc3.y += 150;
		proj = (GameObject)Instantiate (prefab, loc3, Quaternion.identity);
		tempMan = proj.GetComponent<UnitManager> ();
		tempMan.setInteractor();
		tempMan.interactor.initialize ();
		racer.applyUpgrade (tempMan);
		proj.AddComponent<SpaceDrop> ();
		proj.GetComponent<SpaceDrop> ().speed = 3;
		loc3.y -= 150;
		proj.GetComponent<SpaceDrop> ().setLocation (loc3);

		Vector3 loc4 = location;
		loc4.z -= 9;
		loc4.y += 150;
		proj = (GameObject)Instantiate (prefab, loc4, Quaternion.identity);
		tempMan = proj.GetComponent<UnitManager> ();
		tempMan.setInteractor();
		tempMan.interactor.initialize ();
		racer.applyUpgrade (tempMan);
		proj.AddComponent<SpaceDrop> ();
		proj.GetComponent<SpaceDrop> ().speed = 3;
		loc4.y -= 150;
		proj.GetComponent<SpaceDrop> ().setLocation (loc4);


		return false;

	}





	override
	public void Cast(){



		//myCost.payCost ();

		//GameObject proj = null;

		//Vector3 pos = this.gameObject.transform.position;
	
		//proj = (GameObject)
		Instantiate (prefab,  this.gameObject.transform.position, Quaternion.identity);
	



	}


}