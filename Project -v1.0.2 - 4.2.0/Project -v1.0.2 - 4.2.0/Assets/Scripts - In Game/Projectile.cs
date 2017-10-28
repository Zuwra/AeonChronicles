using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.SoundManagerNamespace;
public  class Projectile : MonoBehaviour {



	public UnitManager target;
 	protected UnitManager SourceMan;
	public float damage;
	public float speed;

	public bool trackTarget;

	protected float distance;
	protected float currentDistance;

	//public ProjectileMover mover;
	public GameObject Source;
	public int sourceInt =1;

	public DamageTypes.DamageType damageType = DamageTypes.DamageType.Regular;
	public AudioClip mySound;
	protected AudioSource AudSrc;
	//private bool selfDest = false;
	protected CharacterController control;

	//If you are using an explosion , you should set the variables in the explosion prefab itself.
	public GameObject explosionO;
	public bool SepDamWithExplos;
	public GameObject SpecialEffect;

	public List<Notify> triggers = new List<Notify> ();

	protected Vector3 lastLocation;

	public float FriendlyFire;
	protected Vector3 randomOffset;

	public GameObject myEffect;
	MultiShotParticle multiParticle;

	Lean.LeanPool myBulletPool;
	// Use this for initialization
	public void Start () {	
		if (!myBulletPool) {
			myBulletPool = Lean.LeanPool.getSpawnPool (this.gameObject);
		}

		AudSrc= GetComponent<AudioSource> ();
		if (AudSrc && mySound) {
			AudSrc.priority += Random.Range (-60, 0);
			AudSrc.volume = ((float)Random.Range (1, 5)) / 10;
			AudSrc.pitch +=((float)Random.Range (-3, 3)) / 10;
		}

		control = GetComponent<CharacterController> ();
	}

	public void OnSpawn()
	{	currentDistance = 0;
		
		if (AudSrc && mySound) {
			AudSrc.pitch +=((float)Random.Range (-3, 3)) / 10;
			SoundManager.PlayOneShotSound (AudSrc, mySound);
		}
	}

	public void Initialize(UnitManager targ, float dam, UnitManager src)
	{
		target = targ;
		damage = dam;
		Source = src.gameObject;
		SourceMan = src;
		sourceInt = src.PlayerOwner;
	}

	public virtual void setup()
	{
		if (target) {

			CharacterController cont = target.GetComponent<CharacterController> ();

			randomOffset = UnityEngine.Random.onUnitSphere * cont.radius;
			if (randomOffset.y < -cont.radius * .333f) {
				randomOffset.y = Mathf.Abs (randomOffset.y);
			}
			randomOffset *= .9f;
			randomOffset += Vector3.up;

			lastLocation = target.transform.position + randomOffset;
			distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);

			InvokeRepeating ("lookAtTarget", .05f, .05f);
		}
		lookAtTarget ();
			
	}

	protected virtual void onHit()
	{}
		

	protected void lookAtTarget()
	{
		if (target != null) {
			lastLocation = target.transform.position + randomOffset;
		}

		gameObject.transform.LookAt (lastLocation);
	}

	public void setLocation(Vector3 loc)
	{

		lastLocation = loc + randomOffset;

		gameObject.transform.LookAt (lastLocation);
		distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);
	}

	protected float yAmount;

	// Update is called once per frame
	protected virtual void Update () {

		if (distance - currentDistance < 1.5f) {
			if (target && trackTarget) {
				float trueDist = Vector3.Distance (transform.position, target.transform.position + randomOffset);
				if (trueDist > 2) {
					distance = trueDist;
					currentDistance = 0;
				} else {
					Terminate (target);
				}
			} else {
				Terminate (target);
			}
		}
	

	
		gameObject.transform.Translate (Vector3.forward* speed * Time.deltaTime *40);

		currentDistance += speed * Time.deltaTime * 40;

	}

	protected virtual void OnControllerColliderHit(ControllerColliderHit other)
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
	
	public virtual void OnTriggerEnter(Collider other)
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
	{//Debug.Log ("Finished");

		if (!gameObject.activeSelf) {
		
			return;
		}

		if (explosionO) {

			GameObject explode = (GameObject)Instantiate (explosionO, lastLocation + randomOffset, Quaternion.identity);

			explosion Escript = explode.GetComponent<explosion> ();
			if (Escript) {
				Escript.setSource (Source);
				Escript.damageAmount = this.damage;
				Escript.friendlyFireRatio = FriendlyFire;
			}
		}

		if (explosionO && SepDamWithExplos && target || !explosionO && target) {
			
			foreach (Notify not in triggers) {
				not.trigger (this.gameObject, this.gameObject, target, damage);
			}
			if (target != null && target.myStats != null) {

				//Debug.Log ("Giveing damage");
				float total =  target.myStats.TakeDamage (damage, Source,damageType);
				if (SourceMan)
					{
					SourceMan.myStats.veteranDamage (total);

				}
			}
			if (target == null) {
				{
					SourceMan.cleanEnemy ();}
			}
		} 
	

		if (SpecialEffect) {

			if (!myEffect) {
				myEffect = Instantiate (SpecialEffect, (transform.position + lastLocation + randomOffset) * .5f, transform.rotation);
				multiParticle = myEffect.GetComponent<MultiShotParticle> ();

			}
			else {
				myEffect.transform.position = (transform.position + lastLocation + randomOffset) * .5f;
				myEffect.transform.rotation = transform.rotation;

			}

			if (multiParticle) {
				multiParticle.playEffect ();
			}

		} 

		onHit ();
		myBulletPool.FastDespawn (this.gameObject, 0);
	
		//Destroy (this.gameObject);

	}



	public void Despawn()
	{//Debug.Log ("Despawning " + this.gameObject);
		myBulletPool.FastDespawn (this.gameObject, 0);
	}


	public void setSource(GameObject so)
	{
		
		Source = so;
		SourceMan = so.GetComponent<UnitManager> ();
		if (SourceMan) {
			sourceInt = SourceMan.PlayerOwner;
		} else {
			sourceInt = 1;
		}

	}
	
	


	public void selfDestruct()
	{target = null;
		//selfDest = true;
		if (explosionO) {
			Instantiate (explosionO, lastLocation + randomOffset, Quaternion.identity);
			Debug.Log ("Self destruct");
		}

		onHit ();
		GameObject.Destroy (this.gameObject);
	}




}