using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class IWeapon : MonoBehaviour {


	public UnitManager myManager;


	public float attackPeriod;
	public int numOfAttacks = 1;


	public float baseDamage;

	[Tooltip("this is multiplied by the enemy mass and added or subtracted from the damage")]
	public float massBonus;

	[Tooltip("Having arange that is longer than the vision range is not supported yet")]
	public float range =5;
	public float minimumRange;


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
				
					//Vector3 spotter = enemy.transform.position;
					//spotter.y = this.transform.position.y;
					//this.gameObject.transform.LookAt(spotter);

						

				
				}
			}
		}

	}



	public bool isOffCooldown()
		{return offCooldown;
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
		float verticalDistance = this.gameObject.transform.position.y - target.transform.position.y;
		if (distance > (range + (verticalDistance*1.2)) || distance < minimumRange) {


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
		float verticalDistance = this.gameObject.transform.position.y - target.transform.position.y;
		if (distance > (range + (verticalDistance*1.2))) {
			
		
			return false;}
		return true;

	}


	public void attack(GameObject target)
	{nextActionTime = Time.time + attackPeriod;
		for (int i = 0; i < numOfAttacks; i++) {
			StartCoroutine( Fire ((i * .1f), target));
		
		}


	}


	IEnumerator Fire (float time, GameObject target)
	{
		yield return new WaitForSeconds(time);

		enemy = target;
		if (target) {
			Vector3 spotter = enemy.transform.position;
			spotter.y = this.transform.position.y;
			this.gameObject.transform.LookAt(spotter);

			float damage = baseDamage;
			UnitStats targetStats = target.GetComponent<UnitStats> ();


			foreach (bonusDamage tag in extraDamage) {
				if (targetStats.isUnitType (tag.type)) {
					damage += tag.bonus;
				}
			}
			damage += massBonus * targetStats.mass;

			GameObject proj = null;
			if (projectile != null) {
				Vector3 pos = this.gameObject.transform.position;
				pos.y += this.gameObject.GetComponent<CharacterController> ().radius;
				proj = (GameObject)Instantiate (projectile, pos, Quaternion.identity);
			
				Projectile script = proj.GetComponent<Projectile> ();
				proj.SendMessage ("setSource", this.gameObject);
				proj.SendMessage ("setTarget", target);
				proj.SendMessage ("setDamage", damage);
				script.damage = damage;

				script.target = target;
				script.Source = this.gameObject;

			} else {

				//OnAttacking();
				damage = target.GetComponent<UnitStats> ().TakeDamage (damage, this.gameObject, DamageTypes.DamageType.Regular);

			}
			if (target == null) {
				myManager.cleanEnemy ();
			}

			fireTriggers (this.gameObject, proj, target, damage);

		}
	}
		


	public void fireTriggers(GameObject source, GameObject proj, GameObject target, float damage)
	{	foreach (Notify obj in triggers) {
			obj.trigger(source,proj,target, damage);
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



	public void changeDamage(float dm)
	{
		baseDamage += dm;
	}

	public void changeAttackSpeed(float am)
	{
		attackPeriod -= am;
	}



}
