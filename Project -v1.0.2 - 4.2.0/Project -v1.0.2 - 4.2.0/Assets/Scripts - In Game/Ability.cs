using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {

	public string Name;
	[TextArea(2,10)]
	public string Descripton;

	public Sprite iconPic;
	public AbstractCost myCost;
	public enum type{passive, target, activated, building}
	public type myType;
	public bool continueMoving;
	//These are seperate because Unit inspector wont show dictionaries
	public List<string> RequiredUnit = new List<string>();
	private Dictionary<string, bool> requirementList = new Dictionary<string, bool> ();


	//public GameObject UIButton;
	protected string description;
	public bool autocast;
	[Tooltip("Check this if this ability should break normal activities.")]
	public bool active = false;

	//if -1, then it is infinite
	public int chargeCount = -1;


	public abstract continueOrder canActivate();
	public abstract void Activate();  // returns whether or not the next unit in the same group should also cast it
	public abstract void setAutoCast();



	private bool initialized;

	private void initialize()
		{
		initialized = true;
		foreach (string s in RequiredUnit) {

			requirementList.Add (s, false);

		}

		if (requirementList.Count  > 0 && requirementList.ContainsValue (false)) {

		//	Debug.Log (this.gameObject.name + "  was created " + (requirementList.Count == 0) + "   " + (!requirementList.ContainsValue (false)));
			active = false;
		} 

	}

	// Use this for initialization
	void Start () {


		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void newUnitCreated(string newUnit)
	{if (!initialized) {
			initialize ();
		}
		if (requirementList.Count == 0) {
			return;
		}

		if (RequiredUnit.Contains(newUnit)) {
			requirementList [newUnit] = true;
		}

		if (!requirementList.ContainsValue (false)) {

			active = true;
		} 
			

	}


	public void UnitDied(string unitname)
	{
		if (requirementList.Count == 0) {
			return;
		}

		if (RequiredUnit.Contains (unitname)) {
			requirementList [unitname] = false;
		}

		if (requirementList.ContainsValue (false)) {
			active = false;
		}

	}





}
