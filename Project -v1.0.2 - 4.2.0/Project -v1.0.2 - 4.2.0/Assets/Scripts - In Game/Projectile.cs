using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class Projectile : MonoBehaviour {



	public GameObject target;
	public float damage;
	public float speed;

	//public ProjectileMover mover;
	public GameObject Source;



	public List<Notify> triggers = new List<Notify> ();

	private Vector3 lastLocation;


	// Use this for initialization
	void Start () {	lastLocation = target.transform.position;
	
	}



	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			lastLocation = target.transform.position;
			//Debug.Log("attacking on " +Vector3.Distance(lastLocation,this.gameObject.transform.position));


			if(Vector3.Distance(lastLocation,this.gameObject.transform.position) <1.3)
			{

				//Terminate(target);
			}
		//	Debug.Log("moveing towards " + target.name);

		} else {
			//if(Vector3.Distance(lastLocation,this.gameObject.transform.position) <.5)
				{Terminate(null);

				return;}
		}

		gameObject.transform.LookAt (lastLocation);
		gameObject.transform.Translate (Vector3.forward* speed);



	
	}


	
	void OnTriggerEnter(Collider other)
	{if (!other.isTrigger) {
			if (other.gameObject == target) {
				Terminate (other.gameObject);
			}
		}
	}



	public virtual void Terminate(GameObject target)
		{
		if (target != null) {

			foreach(Notify not in triggers)
			{not.trigger(this.gameObject,this.gameObject, target);}
			target.GetComponent<UnitStats>().TakeDamage(damage,Source, DamageTypes.DamageType.Regular);
			if(target == null)
			{{Source.GetComponent<UnitManager>().cleanEnemy();}}
		}
		Destroy (this.gameObject);

	}





	public void setSource(GameObject so)
	{
		
		Source = so;
	}
	
	
	public void setTarget(GameObject so)
	{

		target = so;
	}
	
	public void setDamage(float so)
	{
		
		damage = so;
	}


}
