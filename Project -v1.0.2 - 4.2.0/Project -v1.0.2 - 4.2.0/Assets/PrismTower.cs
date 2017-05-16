using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismTower : MonoBehaviour , Notify{

	public List<PrismTower> pt;
	public float lendDamage;
	private float attackPeriod;
	private float timeOfLastAttack = -100;
	private float range;

	LineRenderer lineRender;
	public GameObject ParticleEffect;
	void Awake() {
		attackPeriod = GetComponent<IWeapon> ().attackPeriod - .05f;
		range = GetComponent<IWeapon> ().range;
		GetComponent<IWeapon> ().addNotifyTrigger (this);
		lineRender = ParticleEffect.GetComponent<LineRenderer> ();

	}

	public float trigger(GameObject source, GameObject proj, UnitManager target, float damage)
	{
		float toAdd = sendForAid (this, this);
		StartCoroutine( DisplayLine (target.gameObject,15,1));
		return toAdd + damage;
	}

	public float sendForAid(PrismTower origin, PrismTower Asker) {
		float toReturn = 0;

		if (Time.time - timeOfLastAttack > attackPeriod) {

			timeOfLastAttack = Time.time;

			if (origin != this) {
				if (Asker.gameObject) {
					StartCoroutine (DisplayLine (Asker.gameObject, 15, 15));
				}
				toReturn = lendDamage;
			}
			foreach (PrismTower p in pt) {
				if (p) {
					toReturn += p.sendForAid (origin, this);
				}
			}
		}

		return toReturn;
	}

	IEnumerator DisplayLine(GameObject target, float StartUp, float endUp)
	{

		ParticleEffect.SetActive (true);

		Vector3[] lines = new Vector3 [2];
		lines [0] = this.transform.position + StartUp*Vector3.up;
		lines [1] = new Vector3 (target.transform.position.x, target.transform.position.y + endUp, target.transform.position.z);
		ParticleEffect.transform.LookAt (lines[1]);
		lineRender.SetPositions (lines);
		yield return new WaitForSeconds(.45f);

	
		ParticleEffect.SetActive (false);
	}


	void OnTriggerEnter(Collider col)
	{

		if (!col.isTrigger) {

			if (col.gameObject == this.gameObject) {
				return;}

			PrismTower prism = col.gameObject.GetComponent<PrismTower> ();
			if (!prism) {
				return;}


			UnitManager manage = col.gameObject.GetComponent<UnitManager>();


			if(manage){
				if (manage.PlayerOwner ==  GetComponent<UnitManager>().PlayerOwner && Vector3.Distance(this.transform.position, col.transform.position) <= range) {
					pt.Add (prism);

				}
			}

		}

	}

}
