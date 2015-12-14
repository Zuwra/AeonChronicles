using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class Projectile : MonoBehaviour {



	public GameObject target;
	public float damage;
	public float speed;
	public float arcAngle;
	public bool trackTarget;

	private float distance;
	private float currentDistance;

	//public ProjectileMover mover;
	public GameObject Source;

	public float inaccuracy;


	//If you are using an explosion , you should set the variables in the explosion prefab itself.
	public GameObject explosion;



	public List<Notify> triggers = new List<Notify> ();

	private Vector3 lastLocation;


	// Use this for initialization
	void Start () {	
		
		if (target) {
			if( inaccuracy > 0){
				Vector3 hitzone = target.transform.position;
				float radius = Random.Range(0, inaccuracy);
				float angle = Random.Range(0, 360);
				
				hitzone.x += Mathf.Sin(Mathf.Deg2Rad * angle);
				hitzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angle);
				
				lastLocation = hitzone;
			
				
			}
			else{
				lastLocation = target.transform.position;
			}
			
			distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);
		}
	}



	
	// Update is called once per frame
	void Update () {

		if (target != null) {
			lastLocation = target.transform.position;
			//Debug.Log("attacking on " +Vector3.Distance(lastLocation,this.gameObject.transform.position));


			if(currentDistance <1.3)
			{

				//Terminate(target);
			}
		

		} else {

				{Terminate(null);

				return;}
		}


		if (trackTarget) {
			gameObject.transform.LookAt (lastLocation);
		}

		gameObject.transform.Translate (Vector3.forward* speed * Time.deltaTime *40);

		currentDistance += speed * Time.deltaTime * 40;

		if (arcAngle > 0) {

			float yAmount;

				yAmount = (( (distance/2) - currentDistance )/distance) * arcAngle *3* Time.deltaTime;

			gameObject.transform.Translate (Vector3.up * yAmount );
	
		}




		//hack for hitting the ground
		if ( !trackTarget &&  this.gameObject.transform.position.y < lastLocation.y)
			Terminate (null);

	
	}


	
	void OnTriggerEnter(Collider other)
	{
	
		if (!other.isTrigger) {
			if (other.gameObject == target) {
				Terminate (other.gameObject);
			}


			if(!trackTarget && other.gameObject!= Source)
				{
			
				Terminate(null);}
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
		if (explosion) {

			GameObject explode = (GameObject)Instantiate (explosion,this.gameObject.transform.position, Quaternion.identity);
			explode.GetComponent<explosion>().source = Source;
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

		if (target) {
			if( inaccuracy > 0){
				Vector3 hitzone = target.transform.position;
				float radius = Random.Range(0, inaccuracy);
				float angle = Random.Range(0, 360);
				
				hitzone.x += Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
				hitzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
				
				lastLocation = hitzone;
			
			}
			else{
				lastLocation = target.transform.position;
			}
			
			distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);
		}




		distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);


		gameObject.transform.LookAt (lastLocation);

	}
	
	public void setDamage(float so)
	{
		
		damage = so;
	}


}
