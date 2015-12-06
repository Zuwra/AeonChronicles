using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStats : MonoBehaviour {



	public float Maxhealth;
	public float health;
	public float startingHealth;
	public float HealthRegenPerSec;
	public float HealthRegenPercentPerSec;


	public float MaxEnergy;
	public float currentEnergy;
	public float StartingEnergy;
	public float EnergyRegenPerSec;
	public float EnergyRegenPrecentPerSec;

	public float kills;
	public float supply;    //positive gives supply, negative uses it
	public float cargoSpace;
	public float attackPriority =1 ;

	public float armor;


	private List<Modifier> damageModifiers = new List<Modifier>();
	public List<UnitTypes.UnitTypeTag> unitTags = new  List<UnitTypes.UnitTypeTag> ();
	//Tags the units can have
	private Selected mySelection;

	//public float mass;

	public SphereCollider visionRange;

	private Shield myShield;
	private float nextActionTime;

	//public List<Methods>  lethalDamage = new List<Methods>();
	//public List<Methods>  deathTriggers = new List<Methods>();

	//public List<Method>  DamageModifiers = new List<Methods>();

	public GameObject deathCorpse;
	//private List<Action> deathTriggers;
	//Enum race


	// Use this for initialization
	void Start () {

		if (!mySelection) {
			mySelection = this.gameObject.GetComponent<Selected>();
		}

		//this stuff is taken care of through the thing that builds it
		//if(supply > 0)
			//{GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().currentSupply += supply;}
		//else if (supply< 0)
			//{GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().supplyMax +=supply;}


		myShield = this.gameObject.GetComponent<Shield> ();
		nextActionTime = Time.time;
	
	}


	public bool isUnitType(UnitTypes.UnitTypeTag type){
		return unitTags.Contains (type);
	}


	void OnDestroy() {
	
	}

	// Update is called once per frame
	void Update () {




		if (Time.time > nextActionTime ) {
			nextActionTime += 1;

			//Regenerate Health
			if (health < Maxhealth) {
				health += HealthRegenPerSec;
				health += Maxhealth * HealthRegenPercentPerSec * .01f;
			}

			if(health > Maxhealth)
				{health = Maxhealth;}


			//Regenerate Energy
			if (currentEnergy < MaxEnergy) {
				currentEnergy += EnergyRegenPerSec;
				currentEnergy += MaxEnergy * EnergyRegenPrecentPerSec * .01f;
			
				if(currentEnergy > MaxEnergy)
				{currentEnergy = MaxEnergy;}


				mySelection.updateEnergyBar(currentEnergy/MaxEnergy);
				//mySelection.updateHealthBar (health / Maxhealth);
			}
			




			
		}

	
	
	}


	public void TakeDamage(float amount, GameObject source, DamageTypes.DamageType type)
	{
		if (!unitTags.Contains (UnitTypes.UnitTypeTag.invulnerable)) {
			if (myShield != null) {
				//remaining = myShield.takeDamage(amount, source);
			}
			bool setToZero = false;
			if (type == DamageTypes.DamageType.Penetrating || type == DamageTypes.DamageType.Regular) {
				foreach (Modifier mod in damageModifiers) {

					amount = mod.modify (amount, source);
					if (amount == 0) {
						setToZero = true;
					}
				}
			}
			if (!setToZero) {
				if (type == DamageTypes.DamageType.Regular || type == DamageTypes.DamageType.Wound) {

						if (amount - armor > 1) {
							amount = (amount - armor);
						} else {
							amount = 1;
						}
				}
				health-= amount;
				mySelection.updateHealthBar (health / Maxhealth);

					if (health <= 0) {
						kill (source);

				}
		
			}
		}
	}





	public void kill(GameObject deathSource)
	{bool FinishDeath= true;
		if (!unitTags.Contains(UnitTypes.UnitTypeTag.invulnerable)) {
			//foreach (Method effect in lethalDamage) {

			//if(call deathTrigger != true)
			//{FinishDeath = false;}


			//}

			FinishDeath = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager>().UnitDying(this.gameObject, deathSource);

			Debug.Log("I should die " + FinishDeath);
			if (FinishDeath) {

				//foreach (Method effect in deathTriggers) {
				//call deathTrigger;


				//}

				if (deathCorpse != null) {
					GameObject.Instantiate (deathCorpse, this.gameObject.transform.position, new Quaternion ());
				}

				if (deathSource) {
					if (deathSource.GetComponent<UnitStats> ()) {
						deathSource.GetComponent<UnitStats> ().kills += 1;
					}
				}
				if(supply > 0)
					{GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().currentSupply -= supply;}
				else if (supply< 0)
					{GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>().supplyMax -=supply;}
				GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitDied (supply);
				Destroy (this.gameObject);

			}
		}

	}


	public void addLethalTrigger()//Method method)
	{
		//lethalDamage.Add (method);
		
	}


	public void addDeathTrigger()//Method method)
		{
		//deathTriggers.Add (method);

	}


	public void addModifier(Modifier mod)
	{damageModifiers.Add (mod);

	}


	public bool atFullHealth()
	{
		if (health >= Maxhealth) {
			return true;
		} else {
			return false;
		}
	}


	public bool atFullEnergy()
	{	
		if (currentEnergy >= MaxEnergy) {
		
			return true;
		} else {
			return false;
		}
	}



}
