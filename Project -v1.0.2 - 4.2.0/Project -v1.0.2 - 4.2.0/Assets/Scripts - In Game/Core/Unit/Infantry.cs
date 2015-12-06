using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InfantryMovement))]
public abstract class Infantry : Unit {

	// Use this for initialization
	protected new void Start () 
	{
		base.Start ();
	}
	
	// Update is called once per frame
	protected new void Update () 
	{
		base.Update ();
	}
}
