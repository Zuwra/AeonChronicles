using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OreDispenser : MonoBehaviour {


	public float OreRemaining;
	public float OreRegenRate;
	private bool inUse;
	
	private Queue<GameObject> workers =  new Queue<GameObject >();
	private float nextActionTime;
	// Use this for initialization
	void Start () {nextActionTime = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (workers.Count > 0) {

		if(nextActionTime > Time.time)
			{
				while(workers.Count >0)
				{
				


						ResourceCarry carry = workers.Dequeue().GetComponent<ResourceCarry>();
						if(carry.myState!= ResourceCarry.workerState.Waiting)
						{OreRemaining -= carry.ResourceOneAmount;
							carry.loadResource(true);
							break;
						}
					
						


				}
			}
		
		}

	
	}




	public void getOre(GameObject client, float amount)
	{

		workers.Enqueue(client);
	}

	public void removeWorker(GameObject client)
	{
		workers.Enqueue(client);
	}

}
