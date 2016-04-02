using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class UnitStats : MonoBehaviour {

	[TextArea(2,10)]
	public string UnitDescription;

	public float Maxhealth;
	public float health;
	public float HealthRegenPerSec;



	public float MaxEnergy;
	public float currentEnergy;
	public float StartingEnergy;
	public float EnergyRegenPerSec;
	public Sprite Icon;

	public float kills;
	public float supply;    //positive gives supply, negative uses it
	public float attackPriority =1 ;

	[Tooltip("This will affect things such as specialized damage and impact effects, should be in the range of 1-12, 12 being buildings")]
	public float mass;
	public float armor;
	private UnitManager myManager;

	private List<Modifier> damageModifiers = new List<Modifier>();
	public List<UnitTypes.UnitTypeTag> unitTags = new  List<UnitTypes.UnitTypeTag> ();
	private List<Modifier> deathTriggers = new List<Modifier> ();
	public List<KillModifier> killMods = new List<KillModifier> ();
	//Tags the units can have
	private Selected mySelection;

	public SphereCollider visionRange;


	private float nextActionTime;


	public GameObject deathCorpse;
	public GameObject deathEffect;


	// Use this for initialization
	void Start () {

		if (!mySelection) {
			mySelection = this.gameObject.GetComponent<Selected>();
		}
		myManager = this.gameObject.GetComponent<UnitManager> ();
	

		nextActionTime = Time.time;
		if (isUnitType (UnitTypes.UnitTypeTag.Structure)) {
	
		}

	}


	public bool isUnitType(UnitTypes.UnitTypeTag type){
		return unitTags.Contains (type);
	}



	// Update is called once per frame
	void Update () {




		if (Time.time > nextActionTime ) {
			nextActionTime += 1;

			//Regenerate Health
			if (health < Maxhealth) {
				health += HealthRegenPerSec;

				updateHealthBar ();
			}

			if(health > Maxhealth)
				{health = Maxhealth;}


			//Regenerate Energy
			if (currentEnergy < MaxEnergy) {
				currentEnergy += EnergyRegenPerSec;
			
			
				if(currentEnergy > MaxEnergy)
				{currentEnergy = MaxEnergy;}


				mySelection.updateEnergyBar(currentEnergy/MaxEnergy);
			
			}

			
		}

	}


	public float TakeDamage(float amount, GameObject source, DamageTypes.DamageType type)
	{
		if (!unitTags.Contains (UnitTypes.UnitTypeTag.Invulnerable)) {
			
			bool setToZero = false;
			if (type == DamageTypes.DamageType.Penetrating || type == DamageTypes.DamageType.Regular) {
				foreach (Modifier mod in damageModifiers) {

					amount = mod.modify (amount, source);
					if (amount <= 0) {
						setToZero = true;
						break;
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

				updateHealthBar ();

					if (health <= 0) {
						kill (source);

				}
				if(type != DamageTypes.DamageType.True)
				myManager.Attacked (source);
			}
			return amount;
		}
		return 0;
	}

	private void updateHealthBar()
	{

			mySelection.updateHealthBar (health / Maxhealth);
	
	}



	public void kill(GameObject deathSource)
	{bool FinishDeath= true;
		if (!unitTags.Contains(UnitTypes.UnitTypeTag.Invulnerable)) {
			//foreach (Method effect in lethalDamage) {

			//if(call deathTrigger != true)
			//{FinishDeath = false;}


			//}
			if (myManager.PlayerOwner == 1) {
				FinishDeath = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager> ().UnitDying (this.gameObject, deathSource);
			}

			if (FinishDeath) {

				foreach (Modifier effect in deathTriggers) {
					effect.modify (0, deathSource);
				}


				if (deathCorpse != null) {
					GameObject.Instantiate (deathCorpse, this.gameObject.transform.position, new Quaternion ());
				}

				if (deathSource) {
					if (deathSource.GetComponent<UnitStats> ()) {
						deathSource.GetComponent<UnitStats> ().upKills ();
					}
				}
				//fix this when we have multiplayer games
				if (myManager.PlayerOwner == 1) {
					
					GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<GameManager> ().activePlayer.UnitDied (supply);
				}

				if (mySelection.IsSelected) {

					RaceManager.removeUnitSelect(myManager);
				}
				if (deathEffect) {
					Instantiate (deathEffect, this.gameObject.transform.position, Quaternion.identity);
				}
					Destroy (this.gameObject);
				
			}
		}

	}


	public void upKills()
	{
		kills++;
		foreach (KillModifier km in killMods) {
			km.incKill ();
		}
	}

	public void addLethalTrigger()//Method method)
	{
		//lethalDamage.Add (method);
		
	}

	public void removeDeathTrigger (Modifier mod)
	{deathTriggers.Remove (mod);
	}
	public void addDeathTrigger( Modifier mod)//Method method)
	{deathTriggers.Add(mod);
		//deathTriggers.Add (method);

	}


	public void addModifier(Modifier mod)
	{damageModifiers.Add (mod);

	}

	public void removeModifier(Modifier mod)
	{damageModifiers.Remove(mod);

	}



	public bool atFullHealth()
	{
		if (health >= Maxhealth) {
			return true;
		} else {
			return false;
		}
	}


	public void heal(float n)
	{
		health += n;
		if (health > Maxhealth) {
			health = Maxhealth;
		}

		updateHealthBar ();
	}

	public bool atFullEnergy()
	{	
		if (currentEnergy >= MaxEnergy) {
		
			return true;
		} else {
			return false;
		}
	}

	public void changeArmor(float amount)
	{armor += amount;}

}
