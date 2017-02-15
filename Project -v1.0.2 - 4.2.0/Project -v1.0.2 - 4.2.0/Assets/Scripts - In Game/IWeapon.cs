using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using DigitalRuby.SoundManagerNamespace;
public class IWeapon : MonoBehaviour {

	public string Title;
	public Sprite myIcon;
	public UnitManager myManager;
	public MultiShotParticle fireEffect;
	public AudioClip attackSoundEffect;
	protected AudioSource audioSrc;
	public Animator myAnimator;

	public float attackPeriod;
	private float baseAttackPeriod;
	public int numOfAttacks = 1;

	int upgradeLevel = 0;

	private float myRadius;

	public float baseDamage;
	private float InitialBaseDamage ;
	bool initialSpeedSet;
	bool initalDamageSet;


	[Tooltip("Having arange that is longer than the vision range is not supported yet")]
	public float range =5;
	public float minimumRange;


	public GameObject turret;
	turret turretClass;
	public List<Vector3> originPoint;
	protected int originIndex = 0;
	//private float nextActionTime;
	public float attackArc;
	private UnitManager enemy;
	public float damagePoint;

	public float AttackDelay;

	//private bool onDamagePoint;
	//private float PointEnd;
	private UnitManager PointSource;


	private bool offCooldown = true;

	public List<Notify> triggers = new List<Notify> ();

	public List<Validator> validators = new List<Validator>();

	Lean.LeanPool myBulletPool;

	public void setBulletPool(Lean.LeanPool pool)
	{
		myBulletPool = pool;
	}

	private struct attackSpeedMod{
		public float perc;
		public float flat;
		public Object source;
	}

	private List<attackSpeedMod> ASMod = new List<attackSpeedMod>();
	private List<attackSpeedMod> DamageMod = new List<attackSpeedMod>();

	public List<UnitTypes.UnitTypeTag> cantAttackTypes = new List<UnitTypes.UnitTypeTag> ();

	[System.Serializable]
	public struct bonusDamage {


		public UnitTypes.UnitTypeTag type;
		public float bonus;
	}
	//public bonusDamage[] extraDamage;
	public List<bonusDamage> extraDamage;

	//public IList<method> weaponModifiers



	public GameObject projectile;
	//public Effect spawnEffect;


	void Awake()
	{
		if (projectile) {
			myBulletPool = Lean.LeanPool.getSpawnPool (projectile);
		}
		baseAttackPeriod = attackPeriod;
		InitialBaseDamage = baseDamage;
		initalDamageSet = true;
		initialSpeedSet = true;
		if (originPoint.Count == 0) {
			originPoint.Add (Vector3.zero);
		}
	}
	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		if (audioSrc) {
			audioSrc.priority += Random.Range (-60, 0);
		}
		myManager = this.gameObject.GetComponent<UnitManager> ();

		myRadius = GetComponent<CharacterController> ().radius;
		if (turret) {
			turretClass = turret.GetComponent<turret> ();
		}
	}




	IEnumerator ComeOffCooldown( float length)
	{
		yield return new WaitForSeconds (length);
		offCooldown = true;

	}

	IEnumerator ComeOffDamagePoint(float length)
	{
		yield return new WaitForSeconds (length);
	
			//onDamagePoint = false;
			PointSource.cMover.removeSpeedBuff (this);
	

	}



	public bool isOffCooldown()
		{return offCooldown;
		}
	

	// Does not check for range
	public bool simpleCanAttack(UnitManager target)
	{
		if (!offCooldown) {
			//Debug.Log (Title + " On cooldown");
			return false;}
		if (!target) {
			//	Debug.Log (Title + " No title");
			return false;}



		foreach (Validator val in validators) {
			if(val.validate(this.gameObject,target.gameObject) == false)
			{
				//Debug.Log ("Not valid");
				return false;}
		}

		UnitStats targetStats= target.GetComponent<UnitStats>();
		foreach (UnitTypes.UnitTypeTag tag in cantAttackTypes) {
			if (targetStats.isUnitType (tag))
			{	//	Debug.Log (Title + "cant attack");
				return false;	}
		}

		return true;
	}

	public bool canAttack(UnitManager target)
	{
		if (!offCooldown) {
			//Debug.Log (Title + " On cooldown");
			return false;}
		if (!target) {
			//	Debug.Log (Title + " No title");
			return false;}
	


		foreach (Validator val in validators) {
			if(val.validate(this.gameObject,target.gameObject) == false)
			{
				//Debug.Log ("Not valid");
				return false;}
		}

			// Account for height advantage
		float distance = Vector3.Distance (this.gameObject.transform.position, target.transform.position) - target.GetComponent<CharacterController>().radius -myRadius;
		float verticalDistance = this.gameObject.transform.position.y - target.transform.position.y;
		if (distance > (range + (verticalDistance/2)) || distance < minimumRange) {
		//	Debug.Log ("Not in range");
			return false;}

		UnitStats targetStats= target.GetComponent<UnitStats>();
		foreach (UnitTypes.UnitTypeTag tag in cantAttackTypes) {
			if (targetStats.isUnitType (tag))
			{	//	Debug.Log (Title + "cant attack");
				return false;	}
		}

		//offCooldown = false;
		return true;
	

	}

	public bool inRange(UnitManager target)
	{

		if (this && target) {

			foreach (UnitTypes.UnitTypeTag tag in cantAttackTypes) {
				if (target.myStats.isUnitType (tag))
				{	
					return false;	}
			}




			float distance = Vector3.Distance (this.gameObject.transform.position, target.transform.position) - target.GetComponent<CharacterController> ().radius - myRadius;
			float verticalDistance = this.gameObject.transform.position.y - target.transform.position.y;

			//Debug.Log (this.gameObject +  "  Distance " + distance + "   range " + range + "  vert  " + (verticalDistance *1.2));
			if (distance > (range + (verticalDistance/2 ))) {
			
		
				return false;
			}
		} else {
			return false;}
		return true;

	}


	public void attack(UnitManager target, UnitManager toStun)
	{
		offCooldown = false;
		StartCoroutine (ComeOffCooldown (attackPeriod));

		if (toStun && damagePoint > 0) {
			toStun.cMover.changeSpeed (-1, 0, false, this);

				StartCoroutine (ComeOffDamagePoint (damagePoint));

			PointSource = toStun;
		}
		for (int i = 0; i < numOfAttacks; i++) {
			StartCoroutine( Fire ((i * .05f + AttackDelay), target));
		
		}
	}


	IEnumerator Fire (float time, UnitManager target)
	{if (myAnimator) {
			//Debug.Log ("Settign state to one");
			myAnimator.Play ("Attack");
			myAnimator.SetInteger ("State", 1);

	
		}

		if (myManager) {
			myManager.animAttack ();
		}
		yield return new WaitForSeconds(time);


		enemy = target;
		if (target) {
			if (turretClass) {
				turretClass.Target (enemy.gameObject);
			}
			else{
			Vector3 spotter = enemy.transform.position;
			spotter.y = this.transform.position.y;
			this.gameObject.transform.LookAt(spotter);
			}
			float damage = baseDamage;


			foreach (bonusDamage tag in extraDamage) {
				if (target.myStats.isUnitType (tag.type)) {
					damage += tag.bonus;
				}
			}


			GameObject proj = null;
			if (projectile != null) {

				if (turret) {
					
					proj = myBulletPool.FastSpawn(turret.transform.rotation * originPoint [originIndex] + this.gameObject.transform.position, Quaternion.identity);
					//Debug.Log ("Projectile is " + proj);
					//proj = (GameObject)Instantiate (projectile, turret.transform.rotation * originPoint [originIndex] + this.gameObject.transform.position, Quaternion.identity);
				} else {
					proj =  myBulletPool.FastSpawn(transform.rotation * originPoint [originIndex] + this.gameObject.transform.position, Quaternion.identity);
					//proj = (GameObject)Instantiate (projectile, transform.rotation * originPoint [originIndex] + this.gameObject.transform.position, Quaternion.identity);
				}
				originIndex++;
				if (originIndex == originPoint.Count) {
					originIndex = 0;}
				Projectile script = proj.GetComponent<Projectile> ();

				damage = fireTriggers (this.gameObject, proj, target, damage);
				proj.SendMessage ("setSource", this.gameObject, SendMessageOptions.DontRequireReceiver);
				proj.SendMessage ("setTarget", target,SendMessageOptions.DontRequireReceiver);
				proj.SendMessage ("setDamage", damage,SendMessageOptions.DontRequireReceiver);

				if (script) {
					script.damage = damage;

					script.target = target;
					script.Source = this.gameObject;
				}


			} else {

				//OnAttacking();
				//Debug.Log("Dealing direct Damage");
				damage = fireTriggers (this.gameObject, proj, target, damage); 
				if (damage > 0) {
					damage = target.myStats.TakeDamage (damage, this.gameObject, DamageTypes.DamageType.Regular);
					myManager.myStats.veteranDamage (damage);
				}

			}
			if (target == null) {
				myManager.cleanEnemy ();
			}
			if (attackSoundEffect) {
				audioSrc.pitch = ((float)Random.Range (6, 14) / 10);
					SoundManager.PlayOneShotSound(audioSrc, attackSoundEffect);
				//audioSrc.PlayOneShot (attackSoundEffect, Random.value/3 + .15f);
			}
			if (fireEffect) {
				fireEffect.playEffect ();
			}
		

		}

		if (myAnimator) {
		yield return new WaitForSeconds(.1f);

			myAnimator.SetInteger ("State", 0);
		}
	}
		
	public void addNotifyTrigger(Notify not)
	{
		triggers.Add (not);
	}

	public void removeNotifyTrigger(Notify not)
	{
		if (triggers.Contains (not)) {
			triggers.Remove (not);
		}
	}

	public float fireTriggers(GameObject source, GameObject proj, UnitManager target, float damage)
	{	triggers.RemoveAll (item => item == null);
		foreach (Notify obj in triggers) {
		//	Debug.Log (obj);
			damage =  obj.trigger(source,proj,target, damage);
		}
		return damage;
	}

	public int getUpgradeLevel ()
	{
		return  upgradeLevel;
	}

	public void incrementUpgrade()
	{
		upgradeLevel++;
	}

	public bool isValidTarget(UnitManager target)
	{
		if (range < 4) {
			if (target.GetComponent<airmover> ()) {
				return false;
			}

		
		}

		foreach (UnitTypes.UnitTypeTag ty in cantAttackTypes) {
			if (target.myStats.isUnitType (ty))
				return false;
		}
		return true;

	}




	public void removeAttackSpeedBuff(Object obj)
	{
		for (int i = 0; i < ASMod.Count; i++) {
			if (ASMod [i].source ==obj) {
				ASMod.RemoveAt (i);
				break;
			}
		}
		adjustAttackSpeed ();

	}


	public void changeAttackSpeed(float perc, float flat, bool perm, Object obj )
	{
		if (!initialSpeedSet) {
			baseAttackPeriod =attackPeriod;
			initialSpeedSet = true;
		}

		if (perm) {
			baseAttackPeriod += flat;
			if (perc > 0) {
				baseAttackPeriod *= perc;
			}
		} else {
			attackSpeedMod temp = new attackSpeedMod ();
			temp.flat = flat;
			temp.perc = perc;
			temp.source = obj;
			ASMod.Add (temp);
		}

		adjustAttackSpeed ();
	
	}

	private void adjustAttackSpeed()
	{//Debug.Log ("Adjusting");


		float speed = baseAttackPeriod;
		foreach (attackSpeedMod a in ASMod) {
			speed += a.flat;
		}

		float percent = 1;
		foreach (attackSpeedMod a in ASMod) {
			percent += a.perc;
		}
		//Debug.Log ("Setting to " + percent);
		speed *= percent;
		if (speed < .05f) {
			speed = .05f;}
		
		attackPeriod = speed;
		//Debug.Log ("Attack PEriod " + attackPeriod + "  " + this.gameObject.name);
	}


	// CHANGE ATTACK DAMAGE

	public void removeAttackBuff(Object obj)
	{
		for (int i = 0; i < DamageMod.Count; i++) {
			if (DamageMod [i].source ==obj) {
				DamageMod.RemoveAt (i);
				break;

			}
		}
		adjustAttack();

	}


	public void AdjustAttack(float perc, float flat, bool perm, Object obj )
	{//Debug.Log ("initials is " + InitialBaseDamage);

		for (int i = 0; i < DamageMod.Count; i++) {
			if (DamageMod [i].source ==obj) {
				DamageMod.RemoveAt (i);
				break;

			}
		}

		changeAttack (perc, flat, perm, obj);
	}


	public void changeAttack(float perc, float flat, bool perm, Object obj )
	{//Debug.Log ("initials is " + InitialBaseDamage);
		if (!initalDamageSet) {
			InitialBaseDamage = baseDamage;
			initalDamageSet = true;
		}

		if (perm) {
			InitialBaseDamage += flat;
			if (perc > 0) {
				InitialBaseDamage *= 1 + perc;
			}
	} else {
		attackSpeedMod temp = new attackSpeedMod ();
		temp.flat = flat;
		temp.perc = perc;
		temp.source = obj;
		DamageMod.Add (temp);
	}

	adjustAttack();
		//Debug.Log ("after is " + InitialBaseDamage);
}

	private void adjustAttack()
	{
		float myDamage = InitialBaseDamage;
		foreach (attackSpeedMod a in DamageMod) {
			myDamage += a.flat;
		}

		float percent = 1;
		foreach (attackSpeedMod a in DamageMod) {
			percent += a.perc;
		}
	//	Debug.Log ("Before multiple " + InitialBaseDamage);
		myDamage *= percent;
		if (myDamage < .05f) {
			myDamage = .05f;}

		baseDamage = myDamage;
	}




	public void OnDrawGizmos()
	{
		if (originPoint != null) {
			foreach (Vector3 vec in originPoint) {
				if (turret) {
					Gizmos.DrawSphere ((turret.transform.rotation) * vec + this.gameObject.transform.position, .5f);
				} else {
					Gizmos.DrawSphere ((transform.rotation) * vec + this.gameObject.transform.position, .5f);
				}
			}
		}
	}


}
