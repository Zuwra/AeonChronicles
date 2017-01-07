using UnityEngine;
using System.Collections;

public class LandMineActivate : MonoBehaviour {

	public IWeapon explosion;

	public int activationsRemaining = 5;// implement this later for multi-use mines

	public float baseDamage;
	public float chargeUpTime;
	public float damagePerSec;


	public float currentDamage;

	public GameObject ChargeEffect;
	public GameObject FullChargeEffect;

	public GameObject explosionEffect;

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

	void OnTriggerEnter(Collider other) {

		UnitManager otherManager = other.gameObject.GetComponent<UnitManager> ();
		if (otherManager && !otherManager.PlayerOwner.Equals (1) && otherManager.getUnitStats()) {

			float amount = otherManager.getUnitStats ().TakeDamage (currentDamage,myVet.gameObject,DamageTypes.DamageType.True);
			if (myVet) {
				myVet.veteranDamage (amount);
			}

			Instantiate (explosionEffect, this.gameObject.transform.position, Quaternion.identity);
			Destroy (this.gameObject);

		}

	}

	public void setSource(GameObject obj)
	{
		myVet = obj.GetComponent<UnitStats> ();

	}

}
