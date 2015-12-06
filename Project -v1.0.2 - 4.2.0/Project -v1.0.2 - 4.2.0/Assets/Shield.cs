using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {



	
	public float Maxhealth;
	public float health;
	public float startingHealth;
	public float HealthRegenPerSec;
	public float HealthRegenPecentPerSec;

	private float nextActionTime;

	// Use this for initialization
	void Start () {
		nextActionTime = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {


	
			if (Time.time > nextActionTime ) {
				nextActionTime += 1;
				
				if (health > Maxhealth) {
				health += HealthRegenPerSec;
				}

			if (health < Maxhealth) {

				health += Maxhealth * HealthRegenPecentPerSec * .01f;
			}

			if(health > Maxhealth)
				{health = Maxhealth;}

			}
	

	
	}


	public float TakeDamage(float amount, GameObject source)
	{


		//foreach (Method modifier in DamageModifiers) {
		//	call modifier(amount, source);

	//	}


		health -= amount;

		if(health <= 0)
		{return -1*health;}
		else
		{return 0;}





	}


}
