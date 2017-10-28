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
	protected type myType;
	[Tooltip("Check this if this ability should not interrupt the units movement")]
	public bool continueMoving;
	//These are seperate because Unit inspector wont show dictionaries
	public List<string> RequiredUnit = new List<string>();
	private Dictionary<string, bool> requirementList = new Dictionary<string, bool> ();


	//public GameObject UIButton;
	protected string description;
	[Tooltip("Check this if you have programmed something for autocasting")]
	public bool canAutoCast;
	public bool autocast;
	[Tooltip("Check this if this ability should show in UI but be grayed out")]
	public bool active = false;

	//if -1, then it is infinite
	public int chargeCount = -1;


	public abstract continueOrder canActivate(bool error);
	public abstract void Activate();  // returns whether or not the next unit in the same group should also cast it
	public abstract void setAutoCast(bool offOn);
	public AudioClip soundEffect;
	protected AudioSource audioSrc;

	private bool initialized;

	private void initialize()
		{
		initialized = true;

		foreach (string s in RequiredUnit) {

			requirementList.Add (s, false);
			GameManager.getInstance ().playerList [GetComponent<UnitManager> ().PlayerOwner - 1].addBuildTrigger (s, this);
		}

		if (requirementList.Count  > 0 && requirementList.ContainsValue (false)) {

		//	Debug.Log (this.gameObject.name + "  was created " + (requirementList.Count == 0) + "   " + (!requirementList.ContainsValue (false)));
			active = false;
		} 



	}


	void Awake()
	{
		audioSrc = GetComponent<AudioSource> ();


	}


	public type getMyType()
	{return myType;
	}


	public void newUnitCreated(string newUnit)
	{

		//Debug.Log ("I am " + this.gameObject + "   " + newUnit);
		if (!initialized) {
			initialize ();
		}
		if (requirementList.Count == 0) {
			return;
		}

		if (RequiredUnit.Contains(newUnit)) {

			//Debug.Log ("I have a " + newUnit);
			requirementList [newUnit] = true;
		}

		if (!requirementList.ContainsValue (false)) {

			active = true;
			if (GetComponent<Selected> ().IsSelected) {
				RaceManager.updateActivity ();
			}
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
			if (GetComponent<Selected> ().IsSelected) {
				RaceManager.updateActivity ();
			}
		}

	}

}
