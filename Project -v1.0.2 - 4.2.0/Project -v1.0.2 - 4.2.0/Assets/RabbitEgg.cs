using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEgg : MonoBehaviour,Modifier  {

	public GameObject ToSpawn;
	public bool hatchOnSight;
	public bool hatchOnDamage;
	public float hatchTime;
	public GameObject hatchEffect;
	public Selected mySelect;

	public bool hatching;
	// Use this for initialization
	void Start () {

		if (hatchOnDamage) {
			GetComponent<UnitStats>().addModifier (this);
		}
		if (!hatchOnSight) {
			GetComponent<SphereCollider> ().enabled = false;
		}
	}
	
	public float modify(float amount, GameObject src, DamageTypes.DamageType theType)
	{
		if (!hatching) {
			startHatch ();
		}
		return amount;
	}

	void OnTriggerEnter(Collider col)
	{
		if (hatchOnSight && !hatching) {
			UnitManager manage = col.GetComponent<UnitManager> ();
			if (manage &&  manage.PlayerOwner == 1) {
				startHatch ();
			
			}

		}
	
	}

	public void startHatch()
	{
		StartCoroutine (gestate());
	}


	IEnumerator gestate()
	{
		
		hatching = true;
		float totalTime = 0;

		while (totalTime < hatchTime) {
			totalTime += .15f;
			mySelect.updateCoolDown (totalTime / hatchTime);
			yield return new WaitForSeconds (.15f);
		
		}
		//UpdateCooldown bar
		BurstForth ();

	}






	void BurstForth()
	{
		if (ToSpawn) {
			Instantiate (ToSpawn, transform.position+ Vector3.up*2 , Quaternion.identity);
		}
		if (hatchEffect) {
			Instantiate (hatchEffect, transform.position + Vector3.up *.5f, Quaternion.identity);
		}

		GetComponent<UnitManager> ().myStats.kill (null);
	}


}
