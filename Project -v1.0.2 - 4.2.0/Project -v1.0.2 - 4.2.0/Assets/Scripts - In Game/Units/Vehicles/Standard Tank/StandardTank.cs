using UnityEngine;
using System.Collections;

public class StandardTank : Vehicle {

	// Use this for initialization
	protected new void Start () 
	{
		//Assign variables for health/movement and so on..
		AssignDetails (ItemDB.GRIStandardTank);
		GetComponent<Movement>().AssignDetails (ItemDB.GRIStandardTank);
		
		//Call base class start
		base.Start ();
	}
	
	// Update is called once per frame
	protected new void Update () 
	{
		base.Update ();
	}
}