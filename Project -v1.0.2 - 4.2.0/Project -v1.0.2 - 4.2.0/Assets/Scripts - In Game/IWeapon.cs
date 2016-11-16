using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


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

	private float myRadius;

	public float baseDamage;
	private float InitialBaseDamage;
	//[Tooltip("this is multiplied by the enemy mass and added or subtracted from the damage")]
	//public float massBonus;

	[Tooltip("Having arange that is longer than the vision range is not supported yet")]
	public float range =5;
	public float minimumRange;


	public GameObject turret;
	public Vector3 originPoint;
	private float nextActionTime;
	public float attackArc;
	private UnitManager enemy;
	public float damagePoint;

	public float AttackDelay;

	private bool onDamagePoint;
	private float PointEnd;
	private UnitManager PointSource;


	private bool offCooldown = true;

	public List<Notify> triggers = new List<Notify> ();

	public List<Validator> validators = new List<Validator>();


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
		baseAttackPeriod = attackPeriod;
		InitialBaseDamage = baseDamage;
	}
	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		audioSrc.priority += Random.Range (-60, 0);
		myManager = this.gameObject.GetComponent<UnitManager> ();

		myRadius = GetComponent<CharacterController> ().radius;
	}



	
	// Update is called once per frame
	protected void Update () {


		if (Time.time > nextActionTime) {
			offCooldown = true;
	
		}
		if (onDamagePoint && PointEnd < Time.time) {
			onDamagePoint = false;
			PointSource.cMover.removeSpeedBuff (this);

		}
			

	}



	public bool isOffCooldown()
		{return offCooldown;
		}
	


	public bool canAttack(UnitManager target)
	{
		if (!offCooldown) {
	
			return false;}
		if (!target) {
			return false;}
	


		foreach (Validator val in validators) {
			if(val.validate(this.gameObject,target.gameObject) == false)
			{
				//Debug.Log ("Not valid");
				return false;}
		}


		float distance = Vector3.Distance (this.gameObject.transform.position, target.transform.position) - target.GetComponent<CharacterController>().radius -myRadius;
		float verticalDistance = this.gameObject.transform.position.y - target.transform.position.y;
		if (distance > (range + (verticalDistance)) || distance < minimumRange) {
			//Debug.Log ("Not in range");

			return false;}

		UnitStats targetStats= target.GetComponent<UnitStats>();
		foreach (UnitTypes.UnitTypeTag tag in cantAttackTypes) {
			if (targetStats.isUnitType (tag))
			{		//Debug.Log ("cant attack");
				return false;	}
		}

		offCooldown = false;
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
			if (distance > (range + (verticalDistance ))) {
			
		
				return false;
			}
		} else {
			return false;}
		return true;

	}


	public void attack(UnitManager target, UnitManager toStun)
	{nextActionTime = Time.time + attackPeriod;
		if (toStun && damagePoint > 0) {
			toStun.cMover.changeSpeed (-1, 0, false, this);
			PointEnd = Time.time + damagePoint;
			onDamagePoint = true;
			PointSource = toStun;
		}
		for (int i = 0; i < numOfAttacks; i++) {
			StartCoroutine( Fire ((i * .05f + AttackDelay), target));
		
		}


	}


	IEnumerator Fire (float time, UnitManager target)
	{if (myAnimator) {
			//Debug.Log ("Settign state to one");
			myAnimator.SetInteger ("State", 1);
	
		}

		if (myManager) {
			myManager.animAttack ();
		}
		yield return new WaitForSeconds(time);


		enemy = target;
		if (target) {
			Vector3 spotter = enemy.transform.position;
			spotter.y = this.transform.position.y;
			this.gameObject.transform.LookAt(spotter);

			float damage = baseDamage;


			foreach (bonusDamage tag in extraDamage) {
				if (target.myStats.isUnitType (tag.type)) {
					damage += tag.bonus;
				}
			}


			GameObject proj = null;
			if (projectile != null) {
				
				proj = (GameObject)Instantiate (projectile,transform.rotation *  originPoint + this.gameObject.transform.position, Quaternion.identity);
			
				Projectile script = proj.GetComponent<Projectile> ();
				proj.SendMessage ("setSource", this.gameObject);
				proj.SendMessage ("setTarget", target);
				proj.SendMessage ("setDamage", damage);
				script.damage = damage;

				script.target = target;
				script.Source = this.gameObject;

			} else {

				//OnAttacking();
				damage =target.myStats.TakeDamage (damage, this.gameObject, DamageTypes.DamageType.Regular);
				myManager.myStats.veteranDamage (damage);

			}
			if (target == null) {
				myManager.cleanEnemy ();
			}
			if (attackSoundEffect) {
				audioSrc.pitch = ((float)Random.Range (6, 14) / 10);
				audioSrc.PlayOneShot (attackSoundEffect, ((float)Random.Range(4,10)/10));
			}
			if (fireEffect) {
				fireEffect.playEffect ();
			}
			fireTriggers (this.gameObject, proj, target, damage);

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

	public void fireTriggers(GameObject source, GameObject proj, UnitManager target, float damage)
	{	triggers.RemoveAll (item => item == null);
		foreach (Notify obj in triggers) {
		//	Debug.Log (obj);
			obj.trigger(source,proj,target, damage);
		}
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
	{if (perm) {
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


	public void changeAttack(float perc, float flat, bool perm, Object obj )
	{//Debug.Log ("initials is " + InitialBaseDamage);
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

		Gizmos.DrawSphere ((transform.rotation)*originPoint +this.gameObject.transform.position, .5f);

	}


}
