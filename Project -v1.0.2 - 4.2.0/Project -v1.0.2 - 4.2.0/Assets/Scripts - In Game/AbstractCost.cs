using UnityEngine;
using System.Collections;

public class AbstractCost : MonoBehaviour {
		
		
	public enum CostType
	{	unit, building, ability, upkeep}

	public CostType MyType;
		//These are global resources held by any player, even though their names might change (ore, gold ,wood etc)
		public float ResourceOne;
		public float ResourceTwo;
		

		public float minimumHealth;
		public float health;
		
		public float energy;

		public float cooldown;
		public float cooldownTimer;
		public bool StartsRefreshed = true;
		
		
		private UnitStats stats;

		private RaceManager myGame;
		public bool allowedToActivate;

		private Selected selectMan;



		public string UsedFor;
		
		
		// Use this for initialization
		void Start () {
		selectMan = this.gameObject.GetComponent<Selected> ();
			
			if (!StartsRefreshed) {
				cooldownTimer = cooldown;
			}
			
			stats = this.gameObject.GetComponent<UnitStats> ();
			myGame = GameObject.FindGameObjectWithTag("GameRaceManager").GetComponent<RaceManager>();
			
		}
		
		// Update is called once per frame
		void Update () {
			if (cooldownTimer > 0) {
				cooldownTimer -= Time.deltaTime;
			//selectMan.updateCoolDown (cooldownTimer / cooldown);
			}
			
			
			
		}
		
		
	public bool canActivate(Ability ab, continueOrder order)
		{
		bool result = true;
		if (!ab.active) {
			result =  false;}
			
		if (myGame.ResourceOne < this.ResourceOne || myGame.ResourceTwo < this.ResourceTwo) {

			if (myGame.ResourceOne < this.ResourceOne) {
				order.reasonList.Add (continueOrder.reason.resourceOne);
			}

			if (myGame.ResourceTwo < this.ResourceTwo) {
				order.reasonList.Add (continueOrder.reason.resourceTwo);
			}
			order.nextUnitCast = false;
			//GameObject.FindGameObjectWithTag ("Error").GetComponent<ErrorPrompt> ().showError ("Not Enough Resources");
			result =  false;
			}
			

			if (stats.health < health || stats.health < minimumHealth) {
			order.reasonList.Add (continueOrder.reason.health);
			result =  false;
			}
			
			if (stats.currentEnergy < energy) {
			order.reasonList.Add (continueOrder.reason.energy);
			result = false;}

	
			if (cooldownTimer > 0) {
			order.reasonList.Add (continueOrder.reason.cooldown);
			result = false;}
			
			return result;
			
		}


	public bool canActivate(Ability ab)
	{
		if (!ab.active) {
			return  false;}

		if (myGame.ResourceOne < this.ResourceOne || myGame.ResourceTwo < this.ResourceTwo) {

		
			GameObject.FindGameObjectWithTag ("Error").GetComponent<ErrorPrompt> ().showError ("Not Enough Resources");
			return false;
		}


		if (stats.health < health || stats.health < minimumHealth) {
			return false;
		}

		if (stats.currentEnergy < energy) {

			return false;}


		if (cooldownTimer > 0) {
			return false;}

		return true;

	}



	public void resetCoolDown()
	{cooldownTimer = 0;
	}
		

	public float cooldownProgress()
	{
		return (1 - cooldownTimer / cooldown);
		}
	
	public void refundCost()
	{
		myGame.updateResources(ResourceOne, ResourceTwo);
		cooldownTimer = 0;
	}


		public void payCost()
	{


			myGame.updateResources (-ResourceOne, -ResourceTwo);
				
				
			stats.TakeDamage (health, this.gameObject, DamageTypes.DamageType.True);

				
			stats.currentEnergy -= energy; 
			cooldownTimer = cooldown;

	}
		
	}
