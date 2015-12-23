using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class IWeapon : MonoBehaviour {


	public UnitManager myManager;


	public float attackPeriod;
	public enum type{melee, ranged, magic}

	public type weaponType;

	public float baseDamage;

	public float range =5;
	public float minimumRange;
	public AbstractCost myCost;
	public float DamagePoint;

	public GameObject turret;
	private float nextActionTime;
	public float attackArc;
	private GameObject enemy;


	private bool offCooldown = true;

	public List<Notify> triggers = new List<Notify> ();

	public List<Validator> validators = new List<Validator>();






	public List<UnitTypes.UnitTypeTag> cantAttackTypes = new List<UnitTypes.UnitTypeTag> ();

	[System.Serializable]
	public struct bonusDamage {


		public UnitTypes.UnitTypeTag type;
		public float bonus;
	}
	public bonusDamage[] extraDamage;


	//public IList<method> weaponModifiers



	public GameObject projectile;
	//public Effect spawnEffect;


	private float attackTimer;

	// Use this for initialization
	void Start () {

		myManager = this.gameObject.GetComponent<UnitManager> ();

	}



	
	// Update is called once per frame
	void Update () {


		if (Time.time > nextActionTime) {
			offCooldown = true;
	
		}
		if (enemy) {
		if(inRange(enemy))
			{
				if (turret != null) {
					turret.GetComponent<turret> ().Target (enemy);
				}
				else
					{
				
					Vector3 spotter = enemy.transform.position;
					spotter.y = this.transform.position.y;
					this.gameObject.transform.LookAt(spotter);

						

				
				}
			}
		}

	}


	


	public bool canAttack(GameObject target)
	{


		if (!offCooldown) {
			
			return false;}
		if (!target) {
			return false;}
	


		foreach (Validator val in validators) {
		if(val.validate(this.gameObject,target) == false)
			{

				return false;}
		}


		float distance = Vector3.Distance (this.gameObject.transform.position, target.transform.position) - target.GetComponent<CharacterController>().radius;
		if (distance > range || distance < minimumRange) {


			return false;}

		UnitStats targetStats= target.GetComponent<UnitStats>();
		foreach (UnitTypes.UnitTypeTag tag in cantAttackTypes) {
			if (targetStats.isUnitType (tag))
			{	
				return false;	}
		}

		offCooldown = false;
		return true;
	

	}

	public bool inRange(GameObject target)
	{
		float distance = Vector3.Distance (this.gameObject.transform.position, target.transform.position)- target.GetComponent<CharacterController>().radius;
		if (distance > range) {
			
		
			return false;}
		return true;

	}


	public void attack(GameObject target)
		{
	
		enemy = target;

		nextActionTime = Time.time + attackPeriod;
			



			float damage = baseDamage;
			UnitStats targetStats= target.GetComponent<UnitStats>();

		
			foreach (bonusDamage tag in extraDamage) {
				if (targetStats.isUnitType (tag.type))
				{damage +=  tag.bonus;}
			}


		GameObject proj = null;
		if (projectile != null) {
			Vector3 pos = this.gameObject.transform.position;
			pos.y +=this.gameObject.GetComponent<CharacterController>().radius;
			proj = (GameObject)Instantiate (projectile,pos, Quaternion.identity);

			Projectile script = proj.GetComponent<Projectile> ();
			proj.SendMessage("setSource",this.gameObject);
			proj.SendMessage("setTarget",target);
			proj.SendMessage("setDamage",damage);
			script.damage = damage;

			script.target = target;
			script.Source = this.gameObject;

		} else {



		
			//OnAttacking();
			target.GetComponent<UnitStats> ().TakeDamage (damage, this.gameObject, DamageTypes.DamageType.Regular);

		}
		if (target == null) {
			myManager.cleanEnemy();
		}

		fireTriggers (this.gameObject,proj, target);
		attackTimer = attackPeriod;
			//ATTACK!

	}



	public void fireTriggers(GameObject source, GameObject proj, GameObject target)
	{	foreach (Notify obj in triggers) {
			obj.trigger(source,proj,target);
		}
	}



	public bool isValidTarget(GameObject target)
	{
		if (range < 4) {
			if (target.GetComponent<airmover> ()) {
				return false;
			}

		
		}

		UnitStats stats = target.GetComponent<UnitStats> ();
		foreach (UnitTypes.UnitTypeTag ty in cantAttackTypes) {
			if (stats.isUnitType (ty))
				return false;
		}
		return true;

	}





}
