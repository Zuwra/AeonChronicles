using UnityEngine;
using System.Collections;

public class AbstractCost : MonoBehaviour {
		
		
	public enum CostType
	{	unit, building, ability, upkeep}

	public CostType MyType;
		//These are global resources held by any player, even though their names might change (ore, gold ,wood etc)
		public float ResourceOne;
		public float ResourceTwo;
		
		public float healthPercent;
		public float minimumHealth;
		public float health;
		
		public float energyPecent;
		public float energy;
		
		public float shield;
		
		public float cooldown;
		private float cooldownTimer;
		public bool StartsRefreshed = true;
		
		
		private UnitStats stats;
		private Shield myShield;
		private RaceManager myGame;
		public bool allowedToActivate;



		public string UsedFor;
		
		
		// Use this for initialization
		void Start () {
			
			if (!StartsRefreshed) {
				cooldownTimer = cooldown;
			}
			myShield = this.gameObject.GetComponent<Shield> ();
			stats = this.gameObject.GetComponent<UnitStats> ();
			myGame = GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>();
			
		}
		
		// Update is called once per frame
		void Update () {
			if (cooldownTimer > 0) {
				cooldownTimer -= Time.deltaTime;
			}
			
			
			
		}
		
		
		public bool canActivate()
		{
			
			if (myGame.ResourceOne < this.ResourceOne || myGame.ResourceTwo < this.ResourceTwo) {
			Debug.Log("not enough resources");
			return false;
			}
			
			if (stats.health < health || stats.health < minimumHealth) {
			Debug.Log("not enough health");
				return false;
			}
			
			if (stats.currentEnergy < energy) {
			Debug.Log("not enough energy");
			return false;}

		if(myShield){
			if(myShield.health < shield) {

				return false;}
			}
			if (cooldownTimer > 0) {
				return false;}
			
			return true;
			
		}
		
	public void refundCost()
	{
		myGame.buildUnit (-ResourceOne, -ResourceTwo);
		cooldownTimer = 0;
	}


		public void payCost()
		{if (canActivate()) {

			myGame.buildUnit(ResourceOne, ResourceTwo);
				
				
			stats.TakeDamage(health,this.gameObject, DamageTypes.DamageType.True);

				
				stats.currentEnergy -= energy; 
			if(myShield){
				myShield.health -=shield;}
				
				cooldownTimer = cooldown;
				
			} else {
				//throw exception
			}
		}
		
		
	}
