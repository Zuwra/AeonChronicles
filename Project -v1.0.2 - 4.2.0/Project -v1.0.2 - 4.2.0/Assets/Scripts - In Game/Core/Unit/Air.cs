using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AirMovement))]
public abstract class Air : Unit {
	
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
