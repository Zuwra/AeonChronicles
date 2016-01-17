using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {

	public string Name;
	[TextArea(2,10)]
	public string Descripton;

	public Material iconPic;
	public AbstractCost myCost;
	public enum type{passive, active, triggered}

	//public GameObject UIButton;
	protected string description;
	public bool autocast;

	//if -1, then it is infinite
	public int chargeCount = -1;


	public abstract bool canActivate();
	public abstract bool Activate();  // returns whether or not the next unit in the same group should also cast it
	public abstract void setAutoCast();

	// Use this for initialization
	void Start () {


		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
