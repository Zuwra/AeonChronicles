using UnityEngine;
using System.Collections;

public sealed class MCV : Vehicle, IDeployable {
	
	public bool Deploying
	{
		get;
		set;
	}



	// Use this for initialization
	new void Start () 
	{
		//Assign variables for health/movement and so on..
		AssignDetails (ItemDB.GRIMCV);
		GetComponent<Movement>().AssignDetails (ItemDB.GRIMCV);
		
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () 
	{
		base.Update ();
		
		if (Deploying)
		{
			//Are we stopped?
			if (GetComponent<VehicleMovement>().CurrentSpeed < 0.1f)
			{
				//Rotate towards target
				if (transform.rotation.eulerAngles.y < 178)
				{
					transform.Rotate(0, GetComponent<VehicleMovement>().RotationalSpeed*Time.deltaTime, 0);
				}
				else if (transform.rotation.eulerAngles.y > 182)
				{
					transform.Rotate(0, -GetComponent<VehicleMovement>().RotationalSpeed*Time.deltaTime, 0);
				}
				else
				{
					//Deploy
					Instantiate (ItemDB.GRIConstructionYard.Prefab, transform.position, ItemDB.GRIConstructionYard.Prefab.transform.rotation);
					
					//Destroy the unit
					Destroy (this.gameObject);
				}
			}
		}
	}

	public void Deploy ()
	{
		Deploying = true;
	}
	
	public void StopDeploy()
	{
		Deploying = false;
	}
}
