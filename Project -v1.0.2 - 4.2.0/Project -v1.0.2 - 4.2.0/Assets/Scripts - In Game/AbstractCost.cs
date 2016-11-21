using UnityEngine;
using System.Collections;

public class AbstractCost : MonoBehaviour {
		
		
	public enum CostType
	{	unit, building, ability, upkeep}

	//public CostType MyType;
		//These are global resources held by any player, even though their names might change (ore, gold ,wood etc)
		public float ResourceOne;
		public float ResourceTwo;
		

		public float health;
		
		public float energy;

		public float cooldown;
		public float cooldownTimer;
		public bool StartsRefreshed = true;
		
		
		private UnitStats stats;

		private RaceManager myGame;
		public bool allowedToActivate;

		//private Selected selectMan;



		public string UsedFor;
		
		
		// Use this for initialization
		void Start () {
		//selectMan = this.gameObject.GetComponent<Selected> ();
			
			if (!StartsRefreshed) {
				cooldownTimer = cooldown;
			StartCoroutine (onCooldown());
			}
			
			stats = this.gameObject.GetComponent<UnitStats> ();
		myGame = GameManager.main.getActivePlayer ();			
		}
		/*
		// Update is called once per frame
		void Update () {
			if (cooldownTimer > 0) {
				cooldownTimer -= Time.deltaTime;
			//selectMan.updateCoolDown (cooldownTimer / cooldown);
			}
			
			
			
		}
	*/

	IEnumerator onCooldown()
	{	cooldownTimer = cooldown;

		while (true){
			yield return 0;
		if (cooldownTimer > 0) {
			cooldownTimer -= Time.deltaTime;
			//selectMan.updateCoolDown (cooldownTimer / cooldown);
		}
		else
		{
			break;
		}
	}

	}
		
		
	public bool canActivate(Ability ab, continueOrder order, bool showError)
		{
		bool result = true;
		if (!ab.active) {
		//	Debug.Log ("Not active");
			result =  false;}

		if (myGame == null) {
		//	Debug.Log ("Totally null");
		}
		if (myGame.ResourceOne < this.ResourceOne || myGame.ResourceTwo < this.ResourceTwo) {

		
			if (showError) {
				ErrorPrompt.instance.notEnoughResource ();

			}

			if (myGame.ResourceOne < this.ResourceOne) {
				order.reasonList.Add (continueOrder.reason.resourceOne);
			}

			if (myGame.ResourceTwo < this.ResourceTwo) {
				order.reasonList.Add (continueOrder.reason.resourceTwo);
			}
			order.nextUnitCast = false;
		//	Debug.Log ("Not enough money");
			//GameObject.FindGameObjectWithTag ("Error").GetComponent<ErrorPrompt> ().showError ("Not Enough Resources");
			result =  false;
			}
			

			if (stats.health < health ) {
			order.reasonList.Add (continueOrder.reason.health);
			result =  false;
			}
			
			if (stats.currentEnergy < energy) {
			order.reasonList.Add (continueOrder.reason.energy);
			result = false;
			if (showError) {
				ErrorPrompt.instance.notEnoughEnergy ();
			}
		}

	
		if (cooldown > 0 && cooldownTimer > 0) {
		//	Debug.Log ("Cooldown is wrong");
			order.reasonList.Add (continueOrder.reason.cooldown);
			result = false;
			if (showError) {
				ErrorPrompt.instance.onCooldown ();
			}
		}
			
			return result;
			
		}


	public bool canActivate(Ability ab)
	{
		if (!ab.active) {
			return  false;}

		if (myGame.ResourceOne < this.ResourceOne || myGame.ResourceTwo < this.ResourceTwo) {
			//ErrorPrompt.instance.showError ("Not Enough Resources");
		

			return false;
		}

		if (stats) {
			if (stats.health < health) {

				return false;
			}

			if (stats.currentEnergy < energy) {

				return false;
			}
		}

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
		myGame.updateResources(ResourceOne, ResourceTwo, false);
		Debug.Log ("Refunding " + this.gameObject);
		cooldownTimer = 0;
	}


	public void payCost ()
	{
		myGame.updateResources (-ResourceOne, -ResourceTwo, false);
		if (stats) {

			if (health > 0) {

				stats.TakeDamage (health, this.gameObject, DamageTypes.DamageType.True);
			}
			if (energy > 0) {
				stats.currentEnergy -= energy;
			}


		}

		StartCoroutine (onCooldown());

		
	}
}
