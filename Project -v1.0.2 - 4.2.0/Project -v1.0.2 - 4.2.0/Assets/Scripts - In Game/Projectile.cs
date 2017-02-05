﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.SoundManagerNamespace;
public  class Projectile : MonoBehaviour {



	public UnitManager target;
	public float damage;
	public float speed;
	public float arcAngle;
	public bool trackTarget;

	private float distance;
	private float currentDistance;

	//public ProjectileMover mover;
	public GameObject Source;
	public int sourceInt;

	public DamageTypes.DamageType damageType = DamageTypes.DamageType.Regular;
	public AudioClip mySound;
	AudioSource AudSrc;
	public float inaccuracy;
	//private bool selfDest = false;
	private CharacterController control;

	//If you are using an explosion , you should set the variables in the explosion prefab itself.
	public GameObject explosionO;
	public bool SepDamWithExplos;
	public GameObject SpecialEffect;

	[Tooltip("Put any objects in here if you want the nose of the projectile to track up and down with the arc")]
	public GameObject myModel;


	public List<Notify> triggers = new List<Notify> ();

	protected Vector3 lastLocation;

	public GameObject TargetIndicator;
	public Texture indicatorPic;
	public float indicatorSize;
	private GameObject myIndiactor;

	protected Vector3 randomOffset;

	// Use this for initialization
	public void Start () {	

		AudSrc= GetComponent<AudioSource> ();
		if (AudSrc) {
			AudSrc.priority += Random.Range (-60, 0);
			AudSrc.volume = ((float)Random.Range (1, 5)) / 10;
			AudSrc.pitch +=((float)Random.Range (-3, 3)) / 10;
		}

			if (AudSrc && mySound) {
				SoundManager.PlayOneShotSound (AudSrc, mySound);
		}

		if (target) {
			if (inaccuracy > 0) {
				Vector3 hitzone = target.transform.position;
				float radius = Random.Range (0, inaccuracy);
				float angle = Random.Range (0, 360);
				
				hitzone.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
				hitzone.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;
				
				lastLocation = hitzone;
			


			
			} else {
				lastLocation = target.transform.position + randomOffset;
			}
		
			randomOffset = UnityEngine.Random.insideUnitSphere * target.GetComponent<CharacterController> ().radius * .9f;
		} 

		control = GetComponent<CharacterController> ();
		distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);

		if (TargetIndicator != null) {

			myIndiactor = (GameObject)Instantiate (TargetIndicator, lastLocation, Quaternion.identity);

			myIndiactor.GetComponentInChildren<Light>().cookie = indicatorPic;
			myIndiactor.GetComponentInChildren<Light> ().spotAngle = indicatorSize;
		}

	
	
	}

	public void OnSpawn()
	{currentDistance = 0;
		if (AudSrc && mySound) {
			SoundManager.PlayOneShotSound (AudSrc, mySound);
		}

		if (target) {
			if (inaccuracy > 0) {
				Vector3 hitzone = target.transform.position;
				float radius = Random.Range (0, inaccuracy);
				float angle = Random.Range (0, 360);

				hitzone.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
				hitzone.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

				lastLocation = hitzone;




			} else {
				lastLocation = target.transform.position + randomOffset;
			}

			randomOffset = UnityEngine.Random.insideUnitSphere * target.GetComponent<CharacterController> ().radius * .9f;
		} 


		distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);

		if (TargetIndicator != null) {

			myIndiactor = (GameObject)Instantiate (TargetIndicator, lastLocation, Quaternion.identity);

			myIndiactor.GetComponentInChildren<Light>().cookie = indicatorPic;
			myIndiactor.GetComponentInChildren<Light> ().spotAngle = indicatorSize;
		}

	}


	public void setLocation(Vector3 loc)
	{

		if (inaccuracy > 0) {
			Vector3 hitzone = loc;
			float radius = Random.Range (0, inaccuracy);
			float angle = Random.Range (0, 360);

			hitzone.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			hitzone.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

			lastLocation = hitzone;




		} else {
			lastLocation = loc + randomOffset;
		}


		gameObject.transform.LookAt (lastLocation);
		distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);
	}
	
	// Update is called once per frame
	void Update () {

	
		if (target != null) {
			lastLocation = target.transform.position + randomOffset;
			//Debug.Log("attacking on " +Vector3.Distance(lastLocation,this.gameObject.transform.position));

		} 

		if(distance - currentDistance <1.5)
		{
			Terminate(target);
		}

		if (trackTarget) {
			gameObject.transform.LookAt (lastLocation);
		}

	
		gameObject.transform.Translate (Vector3.forward* speed * Time.deltaTime *40);

		currentDistance += speed * Time.deltaTime * 40;

		if (arcAngle > 0) {

			float yAmount;

			yAmount = (( (distance/2) - currentDistance )/distance) * arcAngle *3* Time.deltaTime;
			control.Move (Vector3.up * yAmount);

			if (myModel) {
				myModel.transform.LookAt (this.transform.position + transform.forward* -1 + Vector3.up * -yAmount);
			}
			//gameObject.transform.Translate (Vector3.up * yAmount );
	
		}

	
	}

	void OnControllerColliderHit(ControllerColliderHit other)
	{
		if (!target) {
			return;}
		if (other.gameObject == target || other.gameObject.transform.IsChildOf(target.transform)) {
			
			Terminate (other.gameObject.GetComponent<UnitManager>());
		}

		if (currentDistance / distance < .5) {
			return;
		}

		if(!trackTarget && (other.gameObject!= Source || !other.gameObject.transform.IsChildOf(Source.transform) ))
		{

			Terminate(null);}
	}
	
	void OnTriggerEnter(Collider other)
	{if (!target) {
			return;}
		
		if (other.isTrigger) {
			
			return;}
	

		if (other.gameObject == target || other.gameObject.transform.IsChildOf(target.transform)) {
				
			Terminate (other.gameObject.GetComponent<UnitManager> ());
			return;
			}

			if (currentDistance / distance < .5) {
				return;
			}

			if(!trackTarget && (other.gameObject!= Source || !other.gameObject.transform.IsChildOf(Source.transform) ))
				{
				
				Terminate(null);}
		
	}



	public virtual void Terminate(UnitManager target)
	{
		if (explosionO) {
			GameObject explode = (GameObject)Instantiate (explosionO, this.gameObject.transform.position, Quaternion.identity);
			//Debug.Log ("INstantiating explosion");

			explosion Escript = explode.GetComponent<explosion> ();
			if (Escript) {
				Escript.setSource (Source);
				Escript.damageAmount = this.damage;
			}
		}

		if (explosionO && SepDamWithExplos && target || !explosionO && target) {
			
			foreach (Notify not in triggers) {
				not.trigger (this.gameObject, this.gameObject, target, damage);
			}
			if (target != null && target.myStats != null) {

				float total =  target.myStats.TakeDamage (damage, Source,damageType);
				if (Source) {
					UnitManager man = Source.GetComponent<UnitManager> ();
					if (man) {
						man.myStats.veteranDamage (total);
					}
				}
			}
			if (target == null) {
				{
					Source.GetComponent<UnitManager> ().cleanEnemy ();}
			}
		} 
	

		if (SpecialEffect) {
			Instantiate (SpecialEffect,this.gameObject.transform.position, Quaternion.identity);
		
		}

		if (myIndiactor != null) {
			
			Destroy (myIndiactor);
		}

		Lean.LeanPool.Despawn (this.gameObject, 0);
		//Destroy (this.gameObject);

	}





	public void setSource(GameObject so)
	{
		
		Source = so;
		if (so.GetComponent<UnitManager> ()) {
			sourceInt = so.GetComponent<UnitManager> ().PlayerOwner;
		} else {
			sourceInt = 1;
		}
		if (TargetIndicator != null && Source.GetComponent<UnitManager> ().PlayerOwner != 1 ) {
			TargetIndicator.GetComponentInChildren<Light> ().color = Color.red;
		}
	}
	
	
	public void setTarget(UnitManager so)
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
				lastLocation = target.transform.position + randomOffset;
			}
			
			distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);
		}




		distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);


		gameObject.transform.LookAt (lastLocation);

	}

	public void selfDestruct()
	{target = null;
		//selfDest = true;
		if (explosionO) {
			Instantiate (explosionO, this.gameObject.transform.position, Quaternion.identity);
		}

		Destroy (myIndiactor);
		GameObject.Destroy (this.gameObject);
	}
	
	public void setDamage(float so)
	{
		
		damage = so;
	}



}