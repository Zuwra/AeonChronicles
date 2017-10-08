using UnityEngine;
using System.Collections;

public class LandMineActivate : VisionTrigger {

	public IWeapon explosion;

	public int activationsRemaining = 5;// implement this later for multi-use mines

	public float baseDamage;
	public float chargeUpTime;
	public float damagePerSec;

	public float currentDamage;

	public GameObject ChargeEffect;
	public GameObject FullChargeEffect;

	public GameObject explosionEffect;
	[Tooltip("If null, it will use clip already on explosion object")]
	public AudioClip explosionSound;
	UnitStats myVet;

	// Use this for initialization
	void Start () {
		currentDamage = baseDamage;
		StartCoroutine (chargeUp ());
	}

	IEnumerator chargeUp()
	{
		float elapsedTime = .5f;
		yield return new WaitForSeconds (.5f);
		while (elapsedTime < chargeUpTime) {

			currentDamage += damagePerSec / 2;

			yield return new WaitForSeconds (.5f);
			elapsedTime += .5f;
		}

		FullChargeEffect.SetActive (true);
		Destroy (ChargeEffect);
	}
	public override void UnitExitTrigger(UnitManager manager)
	{
	}

	public override void UnitEnterTrigger(UnitManager manager)
	{
		if (manager.getUnitStats ()) {

			float amount;
			if (myVet) {
				amount = manager.getUnitStats ().TakeDamage (currentDamage, myVet.gameObject, DamageTypes.DamageType.True);
				myVet.veteranDamage (amount);
			} else {
				amount = manager.getUnitStats ().TakeDamage (currentDamage, null, DamageTypes.DamageType.True);
			}
			if (PlayerNumber == 1) {
				PlayerPrefs.SetInt ("TotalPlasmaMineDamage", PlayerPrefs.GetInt ("TotalPlasmaMineDamage") + (int)amount);
			}
			GameObject obj = Instantiate (explosionEffect, this.gameObject.transform.position, Quaternion.identity);
			if (explosionSound) {
				obj.GetComponentInChildren<AudioPlayer> ().myClip = explosionSound;
			}
			Destroy (this.gameObject);	
		}
	}

	/*

	void OnTriggerEnter(Collider other) {

		UnitManager otherManager = other.gameObject.GetComponent<UnitManager> ();
		if (otherManager && !otherManager.PlayerOwner.Equals (playerOwner) && otherManager.getUnitStats()) {

			float amount;
			if (myVet) {
				amount = otherManager.getUnitStats ().TakeDamage (currentDamage,myVet.gameObject,DamageTypes.DamageType.True);
				myVet.veteranDamage (amount);
			}
			else
			{amount = otherManager.getUnitStats ().TakeDamage (currentDamage,null,DamageTypes.DamageType.True);}
			PlayerPrefs.SetInt ("TotalPlasmaMineDamage", PlayerPrefs.GetInt("TotalPlasmaMineDamage") +  (int)amount);
			Instantiate (explosionEffect, this.gameObject.transform.position, Quaternion.identity);
			Destroy (this.gameObject);

		}

	}
*/
	public void setSource(GameObject obj)
	{
		myVet = obj.GetComponent<UnitStats> ();

	}

}
