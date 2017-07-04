using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System;



public class UnitStats : MonoBehaviour {

	[TextArea(2,10)]
	public string UnitDescription = "dude";
	public bool isHero;
	public float Maxhealth;
	public float health;
	public float HealthRegenPerSec;




	public float MaxEnergy;
	public float currentEnergy;

	public float EnergyRegenPerSec;
	public Sprite Icon;

	private int kills;
	public float supply;    //positive gives supply, negative uses it
	public float attackPriority =1 ;


	public float armor;
	public float spellResist;
	private UnitManager myManager;

	private List<Modifier> damageModifiers = new List<Modifier>();

	//BE Careful this can pass in both negative and positive numbers!
	private List<Modifier> EnergyModifiers = new List<Modifier>();
	public List<UnitTypes.UnitTypeTag> otherTags = new  List<UnitTypes.UnitTypeTag> ();
	private List<UnitTypes.UnitTypeTag> TotalTags = new  List<UnitTypes.UnitTypeTag> ();

	public UnitTypes.UnitArmorTag armorType;
	public UnitTypes.SizeTag sizeType;
	public UnitTypes.HeightType myHeight;
	private List<Modifier> deathTriggers = new List<Modifier> ();
	public List<KillModifier> killMods = new List<KillModifier> ();

	//Tags the units can have
	private Selected mySelection;

	public SphereCollider visionRange;


	//private float nextActionTime;
	public VeteranStats veternStat;


	public GameObject deathCorpse;
	public GameObject deathEffect;
	public GameObject takeDamageEffect;
	public Sprite UnitPortrait;
	bool tagSet = false;

	public List<Buff> goodBuffs = new List<Buff>();
	public 	List<Buff> badBuffs = new List<Buff>();





	void Awake()
	{

		Initialize ();

	}

	public void SetTags()
	{
		tagSet = true;
		TotalTags.Clear ();
		foreach (UnitTypes.UnitTypeTag t in otherTags) {

			TotalTags.Add ((UnitTypes.UnitTypeTag)Enum.Parse(typeof(UnitTypes.UnitTypeTag) ,t.ToString()));
		}
		TotalTags.Add ((UnitTypes.UnitTypeTag)Enum.Parse(typeof(UnitTypes.UnitTypeTag) ,armorType.ToString()));
		TotalTags.Add ((UnitTypes.UnitTypeTag)Enum.Parse(typeof(UnitTypes.UnitTypeTag) ,myHeight.ToString()));
		TotalTags.Add ((UnitTypes.UnitTypeTag)Enum.Parse(typeof(UnitTypes.UnitTypeTag) ,sizeType.ToString()));

	}

	public void Initialize()
	{
		if (!tagSet) {
			SetTags ();
		}
		if (!myManager) {
			myManager = this.gameObject.GetComponent<UnitManager> ();
			myManager.myStats = this;
		}

		if (isHero) {
			bool HasAName = !isUnitType (UnitTypes.UnitTypeTag.Turret) && !isUnitType (UnitTypes.UnitTypeTag.Structure);
			veternStat= new VeteranStats(HasAName, GetComponent<UnitManager>().UnitName,
				!(isUnitType(UnitTypes.UnitTypeTag.Turret))&&!(isUnitType(UnitTypes.UnitTypeTag.Worker)) && !(isUnitType(UnitTypes.UnitTypeTag.Structure)),GetComponent<UnitManager>().UnitName 
				, myManager.PlayerOwner, myManager);
		}
		else{
		veternStat= new VeteranStats(!isUnitType(UnitTypes.UnitTypeTag.Turret)&& !isUnitType(UnitTypes.UnitTypeTag.Structure), GetComponent<UnitManager>().UnitName,
				!(isUnitType(UnitTypes.UnitTypeTag.Turret))&&!(isUnitType(UnitTypes.UnitTypeTag.Worker)) && !(isUnitType(UnitTypes.UnitTypeTag.Structure)), "", myManager.PlayerOwner,myManager);}

		if (!mySelection) {
			mySelection = this.gameObject.GetComponent<Selected>();
		}
	}



	// Use this for initialization
	void Start () {
		if (!myManager) {
			myManager = GetComponent<UnitManager> ();
			myManager.myStats = this;
		}
	
		if (Clock.main.getTotalSecond()< 1 && myManager.PlayerOwner == 1) {
			
			GameManager.main.playerList[myManager.PlayerOwner-1].UnitCreated (supply);		
		}

		GameManager.main.playerList [myManager.PlayerOwner - 1].addVeteranStat (veternStat);
		if (isHero) {
			veternStat.UnitName = myManager.UnitName;
		}

		// FIX THIS REPEATING INEUMERNATER
		if (EnergyRegenPerSec > 0) {

			 setEnergyRegen (EnergyRegenPerSec);
		//	StartCoroutine (HealthEnergy());
		}
		if (HealthRegenPerSec > 0) {
			StartCoroutine (regenHealth());
		}
	
	}



	Coroutine EnergyUpdater;

	public void setEnergyRegen(float amount)
	{
		EnergyRegenPerSec = amount;
		if (amount == 0) {
			if (EnergyUpdater != null) {
				StopCoroutine (EnergyUpdater);
			}
		}
		else if (EnergyUpdater == null) {
			//Debug.Log ("Starting energy regen " + Time.time);
			EnergyUpdater = StartCoroutine (EnergyReg ());
		}
	}


	IEnumerator EnergyReg()
	{
		float regenPerHalfSecond = EnergyRegenPerSec / 2;

		while(true){
			yield return new WaitForSeconds (.5f);

			//Regenerate Energy
			if (currentEnergy < MaxEnergy ) { //&& EnergyRegenPerSec >0
				
				float actual = changeEnergy (regenPerHalfSecond);
				veternStat.UpEnergy (actual);
			}
		}
	}


	IEnumerator regenHealth(){

		float regenPerHalfSecond = HealthRegenPerSec;

		float waitTime = 1;
		if (HealthRegenPerSec > 2) {
			regenPerHalfSecond = HealthRegenPerSec / 2;
			waitTime = .5f;
		}

		while(true){
			yield return new WaitForSeconds (waitTime);

		if (health < Maxhealth) { //&& HealthRegenPerSec > 0
			float actual = heal (regenPerHalfSecond);;
			veternStat.UpHealing (actual);
			}
		}
	}


	public bool isUnitType(UnitTypes.UnitTypeTag type){
		return TotalTags.Contains (type);
	}

	public void addBuff(Buff input, bool stack)
	{
		if (stack || !goodBuffs.Contains (input)) {
			goodBuffs.Add (input);
		}
		if (mySelection.IsSelected) {
			RaceManager.upDateSingleCard();
		}
	}

	public void addDebuff(Buff input, bool stack)
	{
		if (stack || !badBuffs.Contains (input)) {
			badBuffs.Add (input);
		}
		if (mySelection.IsSelected) {
			RaceManager.upDateSingleCard();
		}
	}

	public void removeBuff(Buff input)
	{
		if (goodBuffs.Contains (input)) {
			goodBuffs.Remove(input);
		}
		if (mySelection.IsSelected) {
			
			RaceManager.upDateSingleCard();
		}

	}

	public void removeDebuff(Buff input, bool stack)
	{
		if (badBuffs.Contains (input)) {
			badBuffs.Remove(input);
		}
		if (mySelection.IsSelected) {
			RaceManager.upDateSingleCard();
		}
	}





	public float TakeDamage(float amount, GameObject source, DamageTypes.DamageType type)
	{

		//Debug.Log ("Damage source " + amount +"    "+ source + "   " + type);
		if (!otherTags.Contains (UnitTypes.UnitTypeTag.Invulnerable)) {
			
			bool setToZero = false;
			if (type != DamageTypes.DamageType.True) {
				foreach (Modifier mod in damageModifiers) {

					amount = mod.modify (amount, source, type);
					if (amount <= 0) {
						setToZero = true;
						break;
					}
				}
			}
			if (!setToZero) {
				if (type == DamageTypes.DamageType.Regular || type == DamageTypes.DamageType.Wound ) {

					amount = Mathf.Max (amount - armor, 1);
						
				}
				if (myManager.PlayerOwner == 1 && source != this.gameObject) {
					if (isUnitType (UnitTypes.UnitTypeTag.Structure)) {
					} else {
						ErrorPrompt.instance.underAttack (this.transform.position);
					}
				}

				if (takeDamageEffect) {
					//Debug.Log ("Taking damage " + this.gameObject);
					Instantiate (takeDamageEffect, this.gameObject.transform.position, new Quaternion ());
				}
				if (veternStat != null) {
					veternStat.UpMitigated(armor);
					veternStat.UpdamTaken (amount);
				}

			//	Debug.Log ("Actual " + amount);
				health -= amount;
			
				if ((int)health <= 0) {
					kill (source);
				} else {
					updateHealthBar ();

			

					if (source) {
						myManager.Attacked (source.GetComponent<UnitManager> ());
					} 
				}
			}
			return amount;
		}
		return 0;
	}


	public void SetHealth (float percent)
	{
		health = Maxhealth * percent;
		updateHealthBar ();
	}

	private void updateHealthBar()
	{
			mySelection.updateHealthBar (health / Maxhealth);
	
	}

	private void updateEnergyBar()
	{

		mySelection.updateEnergyBar(currentEnergy / MaxEnergy);

	}

	private bool dead = false;

	public void kill(GameObject deathSource)
	{
		if (dead)
			return;
		bool FinishDeath= true;
		if (!otherTags.Contains(UnitTypes.UnitTypeTag.Invulnerable)) {
			//foreach (Method effect in lethalDamage) {

			//if(call deathTrigger != true)
			//{FinishDeath = false;}


			//}

			FinishDeath = false;
			if (this) {
				FinishDeath = GameManager.main.playerList [myManager.PlayerOwner - 1].UnitDying (myManager, deathSource, true);
			}

			if (FinishDeath) {
				dead = true;
				deathTriggers.RemoveAll (item => item == null);
				for(int i = deathTriggers.Count -1; i > -1; i --)
				{
					if (deathTriggers[i]!= null) {
						deathTriggers[i].modify (0, deathSource, DamageTypes.DamageType.Regular);
					}
				}


				if (deathCorpse != null) {

					Vector3 spawnLoc = this.gameObject.transform.position;
					if (!isUnitType (UnitTypes.UnitTypeTag.Air)) {
						RaycastHit objecthit;

						Physics.Raycast (this.gameObject.transform.position, Vector3.down, out objecthit, 1000, 1 << 8);
						spawnLoc = objecthit.point;
					}

					GameObject.Instantiate (deathCorpse,spawnLoc, this.gameObject.transform.rotation);
				}

				if (deathSource) {
					if (deathSource.GetComponent<UnitStats> ()) {
						deathSource.GetComponent<UnitStats> ().upKills ();
					}
				}
				//fix this when we have multiplayer games
				if (myManager.PlayerOwner == 1) {
					
					GameManager.main.playerList[myManager.PlayerOwner-1].UnitDied (supply, myManager);
				}

				if (mySelection.IsSelected) {

					RaceManager.removeUnitSelect(myManager);
				}
				if (deathEffect) {
					Instantiate (deathEffect, this.gameObject.transform.position, Quaternion.identity);
				}

				//foreach (UnitManager man in GetComponentsInChildren<UnitManager>()) {
					//Debug.Log ("Triggering children");
					//man.myStats.kill (deathSource);
				//}
	
				SelectedManager.main.updateControlGroups (myManager);
				this.gameObject.SendMessage ("Dying",SendMessageOptions.DontRequireReceiver);
				veternStat.Died = true;
				veternStat.DeathTime = Time.timeSinceLevelLoad;
				Destroy (this.gameObject);
			
				
			}
		}

	}

	public void veteranDamage(float amount)
	{
		if (otherTags.Contains (UnitTypes.UnitTypeTag.Turret)) {
			try{
			transform.root.GetComponent<UnitManager> ().myStats.veternStat.damageDone += amount;
			}
			catch{
			}
		}
		veternStat.damageDone += amount;
	}

	public void upKills()
	{
		kills++;
		veternStat.kills++;
		if (otherTags.Contains (UnitTypes.UnitTypeTag.Turret)) {
			try{
			transform.root.GetComponent<UnitManager> ().myStats.upKills ();
			}
			catch{
			}
		}
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
	{
		if (!damageModifiers.Contains (mod)) {
			damageModifiers.Add (mod);
		}
	}

	public void addHighPriModifier(Modifier mod)
	{
		if (!damageModifiers.Contains (mod)) {
			damageModifiers.Insert(0,mod);
		}
	}

	public void removeModifier(Modifier mod)
	{if (damageModifiers.Contains (mod)) {
			damageModifiers.Remove (mod);
		}
	}


	public void addEnergyModifier(Modifier mod)
	{
		if (!EnergyModifiers.Contains (mod)) {
			EnergyModifiers.Add (mod);
		}
	}

	public void removeEnergyModifier(Modifier mod)
	{if (EnergyModifiers.Contains (mod)) {
			EnergyModifiers.Remove (mod);
		}
	}


	public bool atFullHealth()
	{
		if (health >= Maxhealth) {
			return true;
		} else {
			return false;
		}
	}

	public float changeEnergy(float n)
	{//Debug.Log ("Recharging " + n);
		if (MaxEnergy == 0) {
		
			return 0;}
		float amount = 0;

	
		foreach (Modifier mod in EnergyModifiers) {
			n = mod.modify (n, this.gameObject, DamageTypes.DamageType.Regular);
		}

		if (n > 0 ) {

			amount = Math.Min (n, MaxEnergy - currentEnergy);
			currentEnergy += amount;
			updateEnergyBar ();
		} 
		else if(n < 0){
			currentEnergy += n;
			if (currentEnergy < 0) {
				currentEnergy = 0;
			}
			updateEnergyBar ();
		}
			
		return amount;
	}

	public float heal(float n)
	{
		
		float amount = Math.Min (n, Maxhealth - health);
		health += amount;


		updateHealthBar();
		return amount;
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

	public int getKills()
	{
		return kills;
	}

}
